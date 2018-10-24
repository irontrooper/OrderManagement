using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    /// <summary>
    /// Represents a class which defines customer order detail object
    /// </summary>
    public class CustomerOrderDetail : EntityBaseInt
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets customer order id
        /// </summary>
        public int FkCustomerOrder { get; set; }

        /// <summary>
        /// Gets or sets product id
        /// </summary>
        public int FkProduct { get; set; }

        /// <summary>
        /// Gets or sets quantity
        /// </summary>
        public int Quantity { get; set; }

        #endregion
    }
}
