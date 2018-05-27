using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoreFaces.Order.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Order.Models.Mapping
{
    public class OrderItemMap
    {
        public OrderItemMap(EntityTypeBuilder<OrderItem> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.VendorId).IsRequired();
            entityBuilder.Property(t => t.OrderId).IsRequired();
            entityBuilder.Property(t => t.ProductId).IsRequired();
            entityBuilder.Property(t => t.ProductName).IsRequired();
            entityBuilder.Property(t => t.Description).IsRequired();
            entityBuilder.Property(t => t.StockCode).IsRequired();
            entityBuilder.Property(t => t.Quantity).IsRequired();
            entityBuilder.Property(t => t.Price).IsRequired();
            entityBuilder.Property(t => t.Currency).IsRequired();
            entityBuilder.Property(t => t.Vat).IsRequired();
            entityBuilder.Property(t => t.CreateDate).IsRequired();
            entityBuilder.Property(t => t.UpdateDate).IsRequired();
        }
    }
}
