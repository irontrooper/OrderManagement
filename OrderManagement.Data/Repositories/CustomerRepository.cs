using OrderManagement.Core;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data
{
    [DependencyRegisterar(LifetimeManagerType = typeof(PerRequestLifetimeManager))]
    public class CustomerRepository : OrderManagementRepository<Customer>
    {
        public CustomerRepository(OrderManagementDbContext context) : base(context)
        {
        }
    }
}
