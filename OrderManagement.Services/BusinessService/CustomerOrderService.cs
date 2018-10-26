using OrderManagement.Core;
using OrderManagement.Data;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OrderManagement.Services
{
    /// <summary>
    /// Represents a class which manages the customer order operations
    /// </summary>
    [DependencyRegisterar(LifetimeManagerType = typeof(PerRequestLifetimeManager))]
    public class CustomerOrderService : OrderManagementService<CustomerOrder>
    {
        #region Memebers 

        private CustomerOrderRepository _CustomerOrderRepository;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets instance of CustomerOrderDetailService
        /// </summary>
        public static CustomerOrderService Instance
        {
            get
            {
                return OrderManagementCore.Instance.Resolve<CustomerOrderService>();
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of CustomerOrderDetailService
        /// </summary>
        /// <param name="customerOrderRepository"></param>
        public CustomerOrderService(CustomerOrderRepository customerOrderRepository) : base(customerOrderRepository)
        {
            _CustomerOrderRepository = customerOrderRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds customer order with order details
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customerOrderDetails"></param>
        /// <returns></returns>
        public OperationResult<CustomerOrder> AddCustomerOrderWithCustomerOrderDetails(int customerId, List<CustomerOrderDetail> customerOrderDetails)
        {
            var result = new OperationResult<CustomerOrder>();
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                var customer = CustomerService.Instance.GetEntityById(customerId);
                if (customer == null)
                {
                    result.AddError("Customer could not found!");
                    return result;
                }

                var customerOrder = new CustomerOrder
                {
                    FkCustomer = customerId
                };

                result = Instance.Insert(customerOrder);
                if (!result.IsSucceed)
                {
                    result.AddError("Customer Order could not insert to database!");
                    return result;
                }
                if (customerOrderDetails != null)
                {
                    var customerOrderId = result.Item.Id;
                    foreach (var detail in customerOrderDetails)
                    {
                        detail.FkCustomerOrder = customerOrderId;
                    }

                    var resultCOD = CustomerOrderDetailService.Instance.InsertCustomerOrderDetails(customerOrderDetails);
                    if (!resultCOD.IsSucceed)
                    {
                        result.AddError(resultCOD.FormatErrors());
                    }
                }

                scope.Complete();
                return result;
            }
        }

        /// <summary>
        /// Deletes customer order with customer order details
        /// </summary>
        /// <returns></returns>
        public OperationResult DeleteCustomerOrderWithCustomerOrderDetails(int customerOrderId)
        {
            var result = new OperationResult();
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                result = CustomerOrderDetailService.Instance.DeleteCustomerOrderDetails(customerOrderId);
                if (!result.IsSucceed)
                {
                    return result;
                }

                result = Instance.DeleteEntityById(customerOrderId);
                if (!result.IsSucceed)
                {
                    return result;
                }

                scope.Complete();
                return result;
            }
        }

        #endregion
    }
}
