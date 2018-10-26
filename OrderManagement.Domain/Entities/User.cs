using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    /// <summary>
    /// Represents a class which defines User
    /// </summary>
    public class User : EntityBaseInt
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets password
        /// </summary>
        public string Password{ get; set; }
        #endregion
    }
}
