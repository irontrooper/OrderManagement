using OrderManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderManagement.Domain;
using OrderManagement.Core;

namespace OrderManagement.Controllers
{
    public class CustomersController : ApiController
    {
        //private CustomerService _CustomerService;

        //public CustomersController(CustomerService customerService)
        //{
        //    _CustomerService = customerService;
        //}

        // GET api/customers
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };

        }

        // GET api/customers/5
        public string Get(int id)
        {
            var item = CustomerService.Instance.GetEntityById(id);
            return "value";
        }

        // POST api/customers
        public void Post([FromBody]string value)
        {
        }

        // PUT api/customers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/customers/5
        public void Delete(int id)
        {
        }
    }
}
