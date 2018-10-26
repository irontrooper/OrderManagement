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
    /// <summary>
    /// Represents a class which manages the customer operations
    /// </summary>
    [DependencyRegisterar(LifetimeManagerType = typeof(PerRequestLifetimeManager))]
    public class CustomerService : OrderManagementService<Customer>
    {
        #region Members

        private CustomerRepository _CustomerRepository;

        #endregion

        /// <summary>
        /// Gets instance of service
        /// </summary>
        public static CustomerService Instance
        {
            get
            {
                return OrderManagementCore.Instance.Resolve<CustomerService>();
            }
        }


        #region Initialization

        /// <summary>
        /// Initializes a new instance of CustomerService
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="customerRepository"></param>
        public CustomerService(CustomerRepository customerRepository) : base(customerRepository)
        {
            _CustomerRepository = customerRepository;
        }

        #endregion
    }
}
