using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreFaces.Order.Models
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
