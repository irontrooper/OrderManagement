using OrderManagement.Core;
using OrderManagement.Data;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OrderManagement.Services
{
    /// <summary>
    /// Represents a class which manages the customer order detail operations
    /// </summary>
    [DependencyRegisterarAttribute(LifetimeManagerType = typeof(PerRequestLifetimeManager))]
    public class CustomerOrderDetailService : OrderManagementService<CustomerOrderDetail>
    {
        #region Memebers 

        private CustomerOrderDetailRepository _CustomerOrderDetailRepository;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets instance of CustomerOrderDetailService
        /// </summary>
        public static CustomerOrderDetailService Instance
        {
            get
            {
                return OrderManagementCore.Instance.Resolve<CustomerOrderDetailService>();
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of CustomerOrderDetailService
        /// </summary>
        /// <param name="customerOrderDetailRepository"></param>
        public CustomerOrderDetailService(CustomerOrderDetailRepository customerOrderDetailRepository) : base(customerOrderDetailRepository)
        {
            _CustomerOrderDetailRepository = customerOrderDetailRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Inserts customer order details
        /// </summary>
        /// <param name="customerOrderDetails"></param>
        /// <returns></returns>
        public OperationResult InsertCustomerOrderDetails(List<CustomerOrderDetail> customerOrderDetails)
        {
            var result = new OperationResult();

            //This list holds the product ids in the order details that cannot be deleted.
            var notInsertedDetailProductNames = new List<int>();
            foreach (var orderDetail in customerOrderDetails)
            {
                var resultOD = Instance.Insert(orderDetail);
                if (!resultOD.IsSucceed)
                {
                    notInsertedDetailProductNames.Add(orderDetail.FkProduct);
                }
            }

            if (notInsertedDetailProductNames.Count > 0)
            {
                var idsString = string.Join(", ", notInsertedDetailProductNames);
                var message = $"Could not added Products with ids '{idsString}'";
                result.AddError(message);
            }

            return result;
        }

        /// <summary>
        /// Deletes customer order details by given customer order id
        /// </summary>
        /// <param name="customerOrderId"></param>
        /// <returns></returns>
        public OperationResult DeleteCustomerOrderDetails(int customerOrderId)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                var result = new OperationResult();
                var customerOrderDetails = Instance.QuerableSearch().Where(x => x.FkCustomerOrder == customerOrderId).ToList();

                foreach (var detail in customerOrderDetails)
                {
                    result = Instance.Delete(detail);
                    if (!result.IsSucceed)
                    {
                        return result;
                    }
                }

                scope.Complete();
                return result;
            }
        }
        #endregion
    }
}
