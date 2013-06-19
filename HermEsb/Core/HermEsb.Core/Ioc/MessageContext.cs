using System;

namespace HermEsb.Core.Ioc
{
    /// <summary>
    /// Message Context
    /// </summary>
    public class MessageContext : IMessageContext
    {
        private readonly IIoc _ioc;
        private IDisposable _containerScope;

        /// <summary>
        /// Gets the message info.
        /// </summary>
        /// <value>The message info.</value>
        public IMessageInfo MessageInfo { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageContext"/> class.
        /// </summary>
        /// <param name="ioc">The ioc.</param>
        public MessageContext(IIoc ioc)
        {
            _ioc = ioc;
            _containerScope = _ioc.CreateContext();
            MessageInfo = new MessageInfo();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; 
        /// <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _containerScope.Dispose();
            }
        }

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return _ioc.Resolve<T>();
        }

        /// <summary>
        /// Resolves the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public object Resolve(Type service)
        {
            return _ioc.Resolve(service);
        }
    }
}
 