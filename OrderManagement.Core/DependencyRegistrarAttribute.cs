using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity.Lifetime;

namespace OrderManagement.Core
{
    /// <summary>
    /// Attribute that contains lifetime manager for given type
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class DependencyRegisterarAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// Gets or sets the lifetime of the dependency
        /// </summary>
        public Type LifetimeManagerType { get; set; }

        #endregion

        #region Initialization

        /// <summary>
        /// Initialize a new instance of DependencyRegisterarAttribute
        /// </summary>
        public DependencyRegisterarAttribute()
        {
            LifetimeManagerType = typeof(TransientLifetimeManager);
        }

        /// <summary>
        /// Initialize a new instance of DependencyRegisterarAttribute with lifetimemanagertype
        /// </summary>
        public DependencyRegisterarAttribute(Type lifetimeManagerType)
        {
            LifetimeManagerType = lifetimeManagerType;
        }
        #endregion
    }
}