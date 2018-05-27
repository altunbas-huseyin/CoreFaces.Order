using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Order.Models.Models
{
    public class OrderView : CoreFaces.Order.Models.Domain.Order
    {
        public List<Domain.OrderItem> orderItems { get; set; }
        public Domain.OrderAddress orderAddress { get; set; }
    }
}
