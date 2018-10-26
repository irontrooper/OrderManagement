using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
    }
}
