using OrderManagement.Core;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data
{
    /// <summary>
    /// Represets a class which defines CustomerOrderRepository
    /// </summary>
    [DependencyRegisterar(LifetimeManagerType = typeof(PerRequestLifetimeManager))]
    public class CustomerOrderRepository : OrderManagementRepository<CustomerOrder>
    {
        #region Initialization

        /// <summary>
        /// Initializes a new instance of CustomerOrderRepository
        /// </summary>
        /// <param name="context"></param>
        public CustomerOrderRepository(OrderManagementDbContext context) : base(context)
        {
        }

        #endregion
    }
}
