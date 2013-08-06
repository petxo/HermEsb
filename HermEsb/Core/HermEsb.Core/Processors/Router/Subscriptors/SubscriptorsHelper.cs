using System;
using System.Linq;
using HermEsb.Core.Messages.Control;
using HermEsb.Core.Processors.Router.Subscriptors.Persisters;
using HermEsb.Logging;

namespace HermEsb.Core.Processors.Router.Subscriptors
{
    /// <summary>
    /// </summary>
    public class SubscriptorsHelper : ISubscriptorsHelper
    {
        private readonly ISubscriptorsPersister _subscriptorsPersister;
        private readonly ISubscriptorsRepository _subscriptorsRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SubscriptorsHelper" /> class.
        /// </summary>
        /// <param name="subscriptorsRepository">The subscriptors repository.</param>
        /// <param name="subscriptorsPersister">The subscriptors persiter.</param>
        public SubscriptorsHelper(ISubscriptorsRepository subscriptorsRepository,
                                  ISubscriptorsPersister subscriptorsPersister)
        {
            _subscriptorsRepository = subscriptorsRepository;
            _subscriptorsPersister = subscriptorsPersister;
        }

        /// <summary>
        ///     Gets or sets the controller.
        /// </summary>
        /// <value>The controller.</value>
        public IRouterController Controller { get; set; }

        /// <summary>
        ///     Loads the stored subscriptors.
        /// </summary>
        public void LoadStoredSubscriptors(Identification identification)
        {
            foreach (Subscriptor subscriptor in _subscriptorsPersister.GetSubscriptors(identification))
            {
                AddSubscriptor(subscriptor);
            }
        }

        /// <summary>
        ///     Subscribes the specified type.
        /// </summary>
        /// <param name="subscriptor">The subscriptor.</param>
        public void Add(Subscriptor subscriptor)
        {
            LoggerManager.Instance.Debug("Adding Subscriptor");
            AddSubscriptor(subscriptor);

            LoggerManager.Instance.Debug("Persisting Subscriptor");
            try
            {
                _subscriptorsPersister.Add(subscriptor);
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error("Error Persisting Subscriptor", ex);
                throw;
            }
        }

        /// <summary>
        ///     Unsubscribes the specified type.
        /// </summary>
        /// <param name="subscriptorIdentification">The subscriptor identification.</param>
        public void Remove(Identification subscriptorIdentification)
        {
            Subscriptor subscriptor = _subscriptorsRepository.Get(subscriptorIdentification);
            _subscriptorsRepository.Remove(subscriptor);
            foreach (SubscriptionKey type in subscriptor.SubcriptionTypes)
            {
                ((ISubscriber) Controller.Processor).Unsubscribe(type, subscriptorIdentification,
                                                                 subscriptor.ServiceInputGateway);
            }

            _subscriptorsPersister.Remove(subscriptor.Service);

            subscriptor.Dispose();
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            foreach (var subscriber in _subscriptorsRepository.GetAll().ToList())
            {
                _subscriptorsRepository.Remove(subscriber);
            }
        }

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public void Refresh()
        {
            Clear();
            LoadStoredSubscriptors(Controller.Processor.Identification);
        }

        /// <summary>
        /// Adds the service.
        /// </summary>
        /// <param name="serviceId">The service id.</param>
        public void AddService(Identification serviceId)
        {
            var subscriptor = _subscriptorsRepository.Get(serviceId);
            AddSubscriptor(subscriptor);
        }

        /// <summary>
        ///     Determines whether [contains] [the specified identification].
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <returns>
        ///     <c>true</c> if [contains] [the specified identification]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(Identification identification)
        {
            return _subscriptorsRepository.Contains(identification);
        }

        /// <summary>
        ///     Sends the specified identification.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="messageControl">The message control.</param>
        public void Send(Identification identification, IControlMessage messageControl)
        {
            Subscriptor subscriptor = _subscriptorsRepository.Get(identification);
            if (subscriptor != null)
            {
                subscriptor.ServiceInputControlGateway.Send(messageControl);
            }
        }


        /// <summary>
        ///     Sends broad cast the specified message bus.
        /// </summary>
        /// <param name="messageControl">The message control.</param>
        public void Send(IControlMessage messageControl)
        {
            foreach (Subscriptor subscriptor in _subscriptorsRepository.GetAll())
            {
                subscriptor.ServiceInputControlGateway.Send(messageControl);
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        ///     Adds the subscriptor.
        /// </summary>
        /// <param name="subscriptor">The subscriptor.</param>
        private void AddSubscriptor(Subscriptor subscriptor)
        {
            _subscriptorsRepository.Add(subscriptor);

            foreach (SubscriptionKey type in subscriptor.SubcriptionTypes)
            {
                if (type != null)
                {
                    LoggerManager.Instance.Debug(string.Format("Adding Subscriptors Types {0}", type.Key));
                    ((ISubscriber) Controller.Processor).Subscribe(type, subscriptor.Service,
                                                                   subscriptor.ServiceInputGateway);
                }
                else
                {
                    LoggerManager.Instance.Error(
                        string.Format("Type null in subscriptor {0}, check subscription message", subscriptor.Service.Id));
                }
            }
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _subscriptorsRepository.Dispose();
            }
        }
    }
}