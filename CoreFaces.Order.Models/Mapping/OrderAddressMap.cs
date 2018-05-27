using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Order.Models.Mapping
{
    public class OrderAddressMap
    {
        public OrderAddressMap(EntityTypeBuilder<Domain.OrderAddress> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.UserId).IsRequired();
            entityBuilder.Property(t => t.MobilePhone).IsRequired();
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.Surname).IsRequired();
            entityBuilder.Property(t => t.OrderId).IsRequired();
            entityBuilder.Property(t => t.CreateDate).IsRequired();
            entityBuilder.Property(t => t.UpdateDate).IsRequired();
        }
    }
}
