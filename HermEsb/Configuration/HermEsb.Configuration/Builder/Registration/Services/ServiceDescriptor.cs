using System;

namespace HermEsb.Configuration.Builder.Registration.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceDescriptor
    {
        private IServiceStrategy _serviceStrategy;
        private readonly BasedOnDescriptor _baseOnDescritor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDescriptor"/> class.
        /// </summary>
        /// <param name="baseOnDescritor">The base on descritor.</param>
        public ServiceDescriptor(BasedOnDescriptor baseOnDescritor)
        {
            _baseOnDescritor = baseOnDescritor;
        }

        /// <summary>
        /// Bases this instance.
        /// </summary>
        public BasedOnDescriptor Base()
        {
            _serviceStrategy = new BaseServiceStrategy();
            return _baseOnDescritor;
        }

        /// <summary>
        /// Firsts the interface.
        /// </summary>
        public BasedOnDescriptor FirstInterface()
        {
            _serviceStrategy = new FirstServiceStrategy();
            return _baseOnDescritor;
        }

        /// <summary>
        /// Selfs this instance.
        /// </summary>
        public BasedOnDescriptor Self()
        {
            _serviceStrategy = new SelfServiceStrategy();
            return _baseOnDescritor;
        }

        /// <summary>
        /// Gets the interface.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public Type GetInterface(Type type)
        {
            return _serviceStrategy.GetInterface(type);
        }
    }
}