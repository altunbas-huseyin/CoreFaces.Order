using CoreFaces.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreFaces.Order.Models.Domain
{
    public class OrderItem : EntityBase
    {
        public Guid VendorId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string StockCode { get; set; }
        public int Quantity { get; set; }
        [DataType("decimal(16,2")]
        public decimal Price { get; set; }
        public Enums.Currency Currency { get; set; }
        public decimal Vat { get; set; }
    }
}
