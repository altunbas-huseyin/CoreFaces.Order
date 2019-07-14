using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Order.Models.Domain
{
    public class Order : EntityBase
    {
        public Guid UserId { get; set; }
        public string OrderNumber { get; set; }
        public string Extra1 { get; set; }
        public string Extra2 { get; set; }
    }
}
