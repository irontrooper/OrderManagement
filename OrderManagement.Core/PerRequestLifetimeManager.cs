using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Unity.Lifetime;

namespace OrderManagement.Core
{
    /// <summary>
    /// Represents a class which defines per request lifetime manager
    /// </summary>
    public class PerRequestLifetimeManager : LifetimeManager
    {
        #region Members

        private readonly object key = new object();

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets value of HttpContext.Current.Items with key
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public override object GetValue(ILifetimeContainer container = null)
        {
            if (HttpContext.Current != null &&
                HttpContext.Current.Items.Contains(key))
                return HttpContext.Current.Items[key];
            else
                return null;
        }

        /// <summary>
        /// Removes value of HttpContext.Current.Items with key
        /// </summary>
        /// <param name="container"></param>
        public override void RemoveValue(ILifetimeContainer container = null)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Items.Remove(key);
        }

        /// <summary>
        /// Sets new value to HttpContext.Current.Items with key
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="container"></param>
        public override void SetValue(object newValue, ILifetimeContainer container = null)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Items[key] = newValue;
        }

        #endregion

        #region Protected Methods

        protected override LifetimeManager OnCreateLifetimeManager()
        {
            return new PerRequestLifetimeManager();
        }

        #endregion
    }
}
