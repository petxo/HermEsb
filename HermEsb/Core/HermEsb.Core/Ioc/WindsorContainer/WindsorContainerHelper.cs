using System;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using Castle.MicroKernel.Registration;

namespace HermEsb.Core.Ioc.WindsorContainer
{
    /// <summary>
    /// 
    /// </summary>
    public class WindsorContainerHelper : IIoc
    {
        /// <summary>
        /// Gets the windsor container.
        /// </summary>
        /// <value>The windsor container.</value>
        public IWindsorContainer WindsorContainer
        {
            get { return _windsorContainer; }
        }

        private readonly IWindsorContainer _windsorContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorContainerHelper"/> class.
        /// </summary>
        /// <param name="windsorContainer">The windsor container.</param>
        public WindsorContainerHelper(IWindsorContainer windsorContainer)
        {
            _windsorContainer = windsorContainer;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            WindsorContainer.Dispose();
        }

        /// <summary>
        /// Releases the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Release(object instance)
        {
            WindsorContainer.Release(instance);
        }

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return WindsorContainer.Resolve<T>();
        }


        /// <summary>
        /// Resolves the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public object Resolve(Type service)
        {
            return WindsorContainer.Resolve(service);
        }

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IIoc AddComponent<T>() where T : class
        {
            WindsorContainer.Register(Component.For<T>().LifestyleScoped().Named(typeof(T).FullName));
            return this;
        }

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public IIoc AddComponent(Type type)
        {
            WindsorContainer.Register(Component.For(type).LifestyleScoped().Named(type.FullName));
            return this;
        }

        /// <summary>
        /// News the context.
        /// </summary>
        public IDisposable CreateContext()
        {
            return _windsorContainer.BeginScope();
        }

        /// <summary>
        /// Disposes the context.
        /// </summary>
        public void DisposeContext()
        {
        }
    }
}