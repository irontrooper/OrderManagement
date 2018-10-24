using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    /// <summary>
    /// Represents a class which defines customer order object
    /// </summary>
    public class CustomerOrder : EntityBaseInt
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets customer id
        /// </summary>
        public int FkCustomer { get; set; }
        #endregion
    }
}
