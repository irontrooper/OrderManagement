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
    /// Represets a class which defines CustomerRepository
    /// </summary>
    [DependencyRegisterar(LifetimeManagerType = typeof(PerRequestLifetimeManager))]
    public class UserRepository : OrderManagementRepository<User>
    {
        #region Initialization

        /// <summary>
        /// Initializes a new instance of CustomerRepository
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(OrderManagementDbContext context) : base(context)
        {
        }

        #endregion
    }
}
