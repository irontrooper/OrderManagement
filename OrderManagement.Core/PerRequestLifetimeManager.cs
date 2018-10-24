using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Unity.Lifetime;

namespace OrderManagement.Core
{
    public class PerRequestLifetimeManager : LifetimeManager
    {
        private readonly object key = new object();

        public override object GetValue(ILifetimeContainer container = null)
        {
            if (HttpContext.Current != null &&
                HttpContext.Current.Items.Contains(key))
                return HttpContext.Current.Items[key];
            else
                return null;
        }

        public override void RemoveValue(ILifetimeContainer container = null)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Items.Remove(key);
        }

        public override void SetValue(object newValue, ILifetimeContainer container = null)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Items[key] = newValue;
        }

        protected override LifetimeManager OnCreateLifetimeManager()
        {
            return new PerRequestLifetimeManager();
        }
    }
}
