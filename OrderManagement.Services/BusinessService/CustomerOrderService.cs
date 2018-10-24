using OrderManagement.Core;
using OrderManagement.Data;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Services
{
    [DependencyRegisterar(LifetimeManagerType = typeof(PerRequestLifetimeManager))]
    public class CustomerOrderService : OrderManagementService<CustomerOrder>
    {
        private OrderManagementRepository<CustomerOrder> _CustomerOrderRepository;

        public CustomerOrderService(OrderManagementRepository<CustomerOrder> customerOrderRepository) : base(customerOrderRepository)
        {
            _CustomerOrderRepository = customerOrderRepository;
        }

    }
}
