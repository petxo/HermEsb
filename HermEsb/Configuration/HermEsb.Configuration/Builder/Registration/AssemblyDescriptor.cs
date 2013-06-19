using System.Collections.Generic;
using System.Reflection;

namespace HermEsb.Configuration.Builder.Registration
{
    ///<summary>
    ///</summary>
    public class AssemblyDescriptor
    {
        private readonly IEnumerable<Assembly> _assemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyDescriptor"/> class.
        /// </summary>
        /// <param name="assemblies">The assembly.</param>
        internal AssemblyDescriptor(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        /// <summary>
        /// Picks this instance.
        /// </summary>
        /// <returns></returns>
        public BasedOnDescriptor Pick()
        {
            return BaseOnDescriptorFactory.CreateDefault(_assemblies);
        }
    }
}