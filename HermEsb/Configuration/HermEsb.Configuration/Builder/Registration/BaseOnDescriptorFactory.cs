using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HermEsb.Configuration.Builder.Registration.Services;

namespace HermEsb.Configuration.Builder.Registration
{
    /// <summary>
    /// 
    /// </summary>
    public static class BaseOnDescriptorFactory
    {
        /// <summary>
        /// Creates the default.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <returns></returns>
        public static BasedOnDescriptor CreateDefault(IEnumerable<Assembly> assemblies)
        {
            var types = assemblies.SelectMany(assembly => assembly.GetTypes()).ToList();

            var basedOnDescriptor = new BasedOnDescriptor(types);
            var serviceDescriptor = new ServiceDescriptor(basedOnDescriptor);
            basedOnDescriptor.WithService = serviceDescriptor;

            return basedOnDescriptor;
        }
    }
}