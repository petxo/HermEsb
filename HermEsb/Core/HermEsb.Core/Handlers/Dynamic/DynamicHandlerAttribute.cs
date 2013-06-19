using System;

namespace HermEsb.Core.Handlers.Dynamic
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DynamicHandlerAttribute : Attribute
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicHandlerAttribute"/> class.
        /// </summary>
        public DynamicHandlerAttribute()
        {

        }

        /// <summary>
        /// Gets or sets the type of the base.
        /// </summary>
        /// <value>The type of the base.</value>
        public Type BaseType { get; set; }

    }
}