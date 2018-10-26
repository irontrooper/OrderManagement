using OrderManagement.Core;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data
{
    /// <summary>
    /// Represents a class which defines order management db context
    /// </summary>
    [DependencyRegisterar(LifetimeManagerType = typeof(PerRequestLifetimeManager))]
    public class OrderManagementDbContext : DbContext
    {
        #region Members

        /// <summary>
        /// Default connection string name, which will be used if there is no specified connection string
        /// </summary>
        public static string DefaultConnectionName = "OrderManagementDataContext";

        #endregion

        #region Public Properties

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<CustomerOrder> CustomerOrder { get; set; }
        public DbSet<CustomerOrderDetail> CustomerOrderDetail { get; set; }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderManagementDbContext"/> class.
        /// </summary>
        public OrderManagementDbContext()
            : base("name=" + DefaultConnectionName)
        {
            this.Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<OrderManagementDbContext>(null);
        }

        #endregion

        #region Protected Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            Assembly.GetExecutingAssembly().GetTypes().Where(t => t.BaseType != null
                                                     && !string.IsNullOrEmpty(t.Namespace)
                                                     && t.BaseType.IsGenericType
                                                     && t.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)
                                                     && t.IsClass
                                                     && !t.IsAbstract)
                                      .ToList()
                                      .ForEach(t =>
                                      {
                                          dynamic instance = Activator.CreateInstance(t);
                                          modelBuilder.Configurations.Add(instance);
                                      });

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
