using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    /// <summary>
    /// Represents a class which defines product object
    /// </summary>
    public class Product : EntityBaseInt
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets product barcode
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        /// Gets or sets product description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets product quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets price amount of product
        /// </summary>
        public decimal Price { get; set; }

        #endregion
    }
}
