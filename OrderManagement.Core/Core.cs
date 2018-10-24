using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace OrderManagement.Core
{
    public class OrderManagementCore : IDisposable
    {
        #region Members

        private static Lazy<OrderManagementCore> _Instance = new Lazy<OrderManagementCore>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance of this class
        /// </summary>
        public static OrderManagementCore Instance
        {
            get
            {
                return _Instance.Value;
            }
        }

        /// <summary>
        /// Gets the container instance
        /// </summary>
        public IUnityContainer Container { get; }

        /// <summary>
        /// Gets assembly types
        /// </summary>
        public List<Type> AssemblyTypes { get; private set; }

        #endregion

        #region Initialization

        /// <summary>
        ///  Initialize a new instance of Core object
        /// </summary>
        public OrderManagementCore()
        {
            Container = new UnityContainer();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialize core services and classes
        /// </summary>
        public void Initialize()
        {
            Container.RegisterInstance(this, new SingletonLifetimeManager());

            var orderManagementAssamblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            AssemblyTypes = orderManagementAssamblies.SelectMany(x => x.GetTypes()).ToList();

            ResolveDependencyRegistrarAttributes();
        }

        /// <summary>
        /// Dispose unmanaged resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Resolves given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Disposes the unity container
        /// </summary>
        /// <param name="disposing"></param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                Container.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Resolves the given types which uses dependency registrar attributes by given lifetimemanager
        /// </summary>
        private void ResolveDependencyRegistrarAttributes()
        {
            var types = AssemblyTypes.Where(t => t.IsClass && !t.IsAbstract && t.IsDefined(typeof(DependencyRegisterarAttribute), false)).ToList();

            types.ForEach(t =>
            {
                object[] atts = t.GetCustomAttributes(typeof(DependencyRegisterarAttribute), false);
                foreach (var item in atts)
                {
                    var dependencyRegistrarAttr = item as DependencyRegisterarAttribute;
                    if (dependencyRegistrarAttr != null)
                    {
                        var instance = Activator.CreateInstance(dependencyRegistrarAttr.LifetimeManagerType);
                        Container.RegisterType(t, instance as LifetimeManager);
                    }
                }
            });
        }

        #endregion

    }
}
