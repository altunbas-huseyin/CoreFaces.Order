using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Order.Models.Domain
{
    public class OrderAddress : EntityBase
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public Guid OrderId { get; set; }
        public string MobilePhone { get; set; } = "";
    }
}
