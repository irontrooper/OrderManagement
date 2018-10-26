using OrderManagement.Domain;
using OrderManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderManagement.Controllers
{
    /// <summary>
    /// Represents an api controllor class which manages Customer operations
    /// </summary>
    [RoutePrefix("customers/{customerid}")]
    [Authorize]
    public class CustomerController : ApiController
    {
        /// <summary>
        /// Changes customer address
        /// </summary>
        /// <param name="customerid">Customer id</param>
        /// <param name="customerAddress">New customer address</param>
        /// <returns></returns>
        [Route("")]
        [HttpPut]
        public IHttpActionResult ChangeCustomerAddress(int customerid, [FromBody] string customerAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbCustomer = CustomerService.Instance.GetEntityById(customerid);
            if (dbCustomer == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Customer could not found!"),
                    ReasonPhrase = "Exception"
                });
            }

            dbCustomer.Address = customerAddress;
            var result = CustomerService.Instance.Update(dbCustomer);
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
    }
}
