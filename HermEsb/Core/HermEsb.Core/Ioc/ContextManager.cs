using System;

namespace HermEsb.Core.Ioc
{
    /// <summary>
    /// 
    /// </summary>
    public class ContextManager : IContextManager
    {
#if __MonoCS__
		private const string ContextStorageName = "CurrentHermEsbStorage";
#endif

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static IContextManager Instance { get; private set; }

        /// <summary>
        /// Creates the specified ioc.
        /// </summary>
        /// <param name="ioc">The ioc.</param>
        public static void Create(IIoc ioc)
        {
            Instance = new ContextManager(ioc);
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly IIoc _ioc;

        [ThreadStatic]
        private static IMessageContext _currentContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextManager"/> class.
        /// </summary>
        /// <param name="ioc">The ioc.</param>
        private ContextManager(IIoc ioc)
        {
            _ioc = ioc;
        }

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void AddComponent<T>() where T : class
        {
            _ioc.AddComponent<T>();
        }

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="type">The type.</param>
        public void AddComponent(Type type)
        {
            _ioc.AddComponent(type);
        }

        /// <summary>
        /// Creates the new context.
        /// </summary>
        /// <returns></returns>
        public IMessageContext CreateNewContext()
        {
            var messageContext = new MessageContext(_ioc);
            _currentContext = messageContext;
            return messageContext;
        }

        /// <summary>
        /// Gets the current context.
        /// </summary>
        /// <value>The current context.</value>
        public IMessageContext CurrentContext
        {
            get
            {
                return _currentContext;
            }
        }

        /// <summary>
        /// Determines whether [has current context].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [has current context]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasCurrentContext()
        {
            return _currentContext != null;
        }
    }
}