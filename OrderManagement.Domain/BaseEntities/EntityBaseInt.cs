using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    /// <summary>
    /// Represents an entity which has an int identity Id column
    /// </summary>
    public class EntityBaseInt : EntityBase
    {
        /// <summary>
        /// Gets or sets id
        /// </summary>
        public virtual int Id { get; set; }
    }
}
