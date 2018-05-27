using Microsoft.EntityFrameworkCore;
using CoreFaces.Order.Models;
using CoreFaces.Order.Models.Domain;
using CoreFaces.Order.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreFaces.Order.Repositories
{
    public class OrderDatabaseContext : DbContext
    {
        public OrderDatabaseContext(DbContextOptions<OrderDatabaseContext> options) : base(options)
        {
            //bool status = this.Database.EnsureDeleted();
            //IExecutionStrategy dd = this.Database.CreateExecutionStrategy();
            //bool test = this.Database.EnsureCreated();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new OrderMap(modelBuilder.Entity<Models.Domain.Order>());
            new OrderItemMap(modelBuilder.Entity<OrderItem>());
            new OrderAddressMap(modelBuilder.Entity<OrderAddress>());

            foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal)))
                    {
                        property.Relational().ColumnType = "decimal(18, 2)";
                    }
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            var changeSet = ChangeTracker.Entries<EntityBase>();
            if (changeSet != null)
            {
                foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged))
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.UpdateDate = DateTime.Now;
                        entry.Entity.CreateDate = DateTime.Now;
                    }
                    entry.Entity.UpdateDate = DateTime.Now;
                }
            }
        }
    }

}
