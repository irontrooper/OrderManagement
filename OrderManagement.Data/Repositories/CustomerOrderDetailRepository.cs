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
    /// Represets a class which defines CustomerOrderDetailRepository
    /// </summary>
    [DependencyRegisterar(LifetimeManagerType = typeof(PerRequestLifetimeManager))]
    public class CustomerOrderDetailRepository : OrderManagementRepository<CustomerOrderDetail>
    {
        #region Initialization

        /// <summary>
        /// Initializes a new instance of CustomerOrderDetailRepository
        /// </summary>
        /// <param name="context"></param>
        public CustomerOrderDetailRepository(OrderManagementDbContext context) : base(context)
        {
        }

        #endregion
    }
}
