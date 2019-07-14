using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Order.Models.Mapping
{
    public class OrderMap
    {
        public OrderMap(EntityTypeBuilder<Domain.Order> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.UserId).IsRequired();
            entityBuilder.Property(t => t.OrderNumber).IsRequired();
            entityBuilder.Property(t => t.Extra1).IsRequired();
            entityBuilder.Property(t => t.Extra2).IsRequired();
            entityBuilder.Property(t => t.CreateDate).IsRequired();
            entityBuilder.Property(t => t.UpdateDate).IsRequired();

            entityBuilder.HasIndex(t => new { t.OrderNumber }).IsUnique();
        }
    }
}
