using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    /// <summary>
    /// Represents a class which defines customer object
    /// </summary>
    public class Customer : EntityBaseInt
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets customer name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets customer address
        /// </summary>
        public string Address { get; set; }
        #endregion
    }
}
