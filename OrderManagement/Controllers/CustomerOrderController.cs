using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OrderManagement.Domain;
using OrderManagement.Services;

namespace OrderManagement.Controllers
{
    /// <summary>
    /// Represents an api controllor class which manages CustomerOrder operations
    /// </summary>
    [RoutePrefix("customers/{customerid}/orders")]
    public class CustomerOrderController : ApiController
    {
        //POST: customers/{customerId}/orders
        /// <summary>
        /// Adds customer order with customer order details
        /// </summary>
        /// <param name="customerid">Customer id</param>
        /// <param name="customerOrderDetails">New Order Items</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult AddCustomerOrderWithCustomerOrderDetails(int customerid, [FromBody]List<CustomerOrderDetail> customerOrderDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = CustomerOrderService.Instance.AddCustomerOrderWithCustomerOrderDetails(customerid, customerOrderDetails);
            if (!result.IsSucceed)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(result.FormatErrors()),
                    ReasonPhrase = "Exception"
                });
            }

            return Ok(result.Item.Id);
        }

        //DELETE: customers/{customerId}/orders/{orderid}
        /// <summary>
        /// Deletes customer order
        /// </summary>
        /// <param name="customerid">Customer id</param>
        /// <param name="orderid">The id of the Order to be deleted</param>
        /// <returns></returns>
        [Route("{orderid}")]
        [HttpDelete]
        public IHttpActionResult DeleteCustomerOrderWithCustomerOrderDetails(int customerid, int orderid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CheckEntityExistingAndRelationsForCustomerAndCustomerOrder(customerid, orderid);

            var result = CustomerOrderService.Instance.DeleteCustomerOrderWithCustomerOrderDetails(orderid);
            if (!result.IsSucceed)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(result.FormatErrors()),
                    ReasonPhrase = "Exception"
                });
            }

            return Ok();
        }

        //PUT: customers/{customerId}/orders/{orderid}/orderitems/{orderitemid}
        /// <summary>
        /// Changes customer order product quantity
        /// </summary>
        /// <param name="customerid">Customer id</param>
        /// <param name="orderid">Order id</param>
        /// <param name="orderitemid">Order item id</param>
        /// <param name="orderProductQuantity">New value of order product quantity</param>
        /// <returns></returns>
        [Route("{orderid}/orderitems/{orderitemid}")]
        [HttpPut]
        public IHttpActionResult ChangeCustomerOrderDetailQuantity(int customerid, int orderid, int orderitemid, [FromBody] int orderProductQuantity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CheckEntityExistingAndRelationsForCustomerAndCustomerOrder(customerid, orderid);

            var orderDetail = CustomerOrderDetailService.Instance.GetEntityById(orderitemid);
            if (orderDetail == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Customer Order Detail could not found for given order detail id!"),
                    ReasonPhrase = "Exception"
                });
            }

            orderDetail.Quantity = orderProductQuantity;

            var result = CustomerOrderDetailService.Instance.Update(orderDetail);
            if (!result.IsSucceed)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(result.FormatErrors()),
                    ReasonPhrase = "Exception"
                });
            }

            return Ok();
        }

        //POST: customers/{customerId}/orders/{orderid}/orderitems
        /// <summary>
        /// Adds new product to customer order detail
        /// </summary>
        /// <param name="customerid">Customer id</param>
        /// <param name="orderid">Order id</param>
        /// <param name="customerOrderDetail">New order item which contains new product</param>
        /// <returns></returns>
        [Route("{orderid}/orderitems")]
        [HttpPost]
        public IHttpActionResult AddNewProductToCustomerOrderDetail(int customerid, int orderid, [FromBody] CustomerOrderDetail customerOrderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CheckEntityExistingAndRelationsForCustomerAndCustomerOrder(customerid, orderid);
            customerOrderDetail.FkCustomerOrder = orderid;

            var result = CustomerOrderDetailService.Instance.Insert(customerOrderDetail);
            if (!result.IsSucceed)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(result.FormatErrors()),
                    ReasonPhrase = "Exception"
                });
            }

            return Ok();
        }

        //POST: customers/{customerId}/orders/{orderid}/orderitems/{productid}
        /// <summary>
        /// Deletes customer order item by given product id 
        /// </summary>
        /// <param name="customerid">Customer id</param>
        /// <param name="orderid">Order id</param>
        /// <param name="productid">The product id of the order item to be deleted</param>
        /// <returns></returns>
        [Route("{orderid}/orderitems/{productid}")]
        [HttpDelete]
        public IHttpActionResult DeleteCustomerOrderDetailByProductId(int customerid, int orderid, int productid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CheckEntityExistingAndRelationsForCustomerAndCustomerOrder(customerid, orderid);

            var dbCustomerOrderDetail = CustomerOrderDetailService.Instance.QuerableSearch().FirstOrDefault(x => x.FkCustomerOrder == orderid && x.FkProduct == productid);
            if (dbCustomerOrderDetail == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Customer order product could not found with given by orderid and product id"),
                    ReasonPhrase = "Exception"
                });
            }

            var result = CustomerOrderDetailService.Instance.Delete(dbCustomerOrderDetail);
            if (!result.IsSucceed)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(result.FormatErrors()),
                    ReasonPhrase = "Exception"
                });
            }

            return Ok();
        }

        /// <summary>
        /// Checks existence of Customer and CustomerOrder object and relations of its
        /// </summary>
        /// <param name="customerid"></param>
        /// <param name="orderid"></param>
        private static void CheckEntityExistingAndRelationsForCustomerAndCustomerOrder(int customerid, int orderid)
        {
            var customer = CustomerService.Instance.GetEntityById(customerid);
            if (customer == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Customer could not found!"),
                    ReasonPhrase = "Exception"
                });
            }

            var customerOrder = CustomerOrderService.Instance.GetEntityById(orderid);
            if (customerOrder == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Customer Order could not found!"),
                    ReasonPhrase = "Exception"
                });
            }

            if (customerid != customerOrder.FkCustomer)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Customer does not have Customer Order with given customer order id!"),
                    ReasonPhrase = "Exception"
                });
            }
        }
    }
}