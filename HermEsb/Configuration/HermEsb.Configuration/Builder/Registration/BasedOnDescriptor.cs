using System;
using System.Collections.Generic;
using System.Linq;
using Bteam.Specifications;
using HermEsb.Configuration.Builder.Registration.Services;
using HermEsb.Configuration.Builder.Registration.Specifications;

namespace HermEsb.Configuration.Builder.Registration
{
    /// <summary>
    /// </summary>
    public class BasedOnDescriptor : IRegister
    {
        private readonly IEnumerable<Type> _types;
        private ISpecification<Type> _specification;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BasedOnDescriptor" /> class.
        /// </summary>
        /// <param name="types">The types.</param>
        internal BasedOnDescriptor(IEnumerable<Type> types)
        {
            _types = types;
        }

        /// <summary>
        ///     Gets or sets the service descriptor.
        /// </summary>
        /// <value>The service descriptor.</value>
        public ServiceDescriptor WithService { get; internal set; }

        /// <summary>
        ///     Alls the interface to class.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<Type, Type>> AllInterfaceToClass()
        {
            If(new IsConcreteClassSpec());

            return from type in _types
                   where _specification.IsSatisfiedBy(type)
                   let @interface = WithService.GetInterface(type)
                   select new KeyValuePair<Type, Type>(@interface, type);
        }

        /// <summary>
        ///     Alls the types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Type> AllTypes()
        {
            return _types.Where(type => _specification != null ? _specification.IsSatisfiedBy(type) : true);
        }

        /// <summary>
        ///     Ifs the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns></returns>
        public BasedOnDescriptor If(ISpecification<Type> specification)
        {
            _specification = _specification == null ? specification : _specification.And(specification);

            return this;
        }
    }
}