using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using CoreFaces.Order.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using CoreFaces.Licensing;

namespace CoreFaces.Order.Repositories
{
    public interface IOrderSchemaRepository
    {
        bool DropTables();
        bool EnsureCreated();
    }

    public class OrderSchemaRepository : Licence, IOrderSchemaRepository
    {
        private readonly OrderDatabaseContext _orderDatabaseContext;

        public OrderSchemaRepository(OrderDatabaseContext orderDatabaseContext, IOptions<OrderSettings> productSettings, IHttpContextAccessor iHttpContextAccessor) : base("Order", iHttpContextAccessor, productSettings.Value.OrderLicenseDomain, productSettings.Value.OrderLicenseKey)
        {
            _orderDatabaseContext = orderDatabaseContext;
        }

        public bool DropTables()
        {
            int result = _orderDatabaseContext.Database.ExecuteSqlCommand("DROP TABLE `Order`; DROP TABLE OrderItem; DROP TABLE OrderAddress;");
            if (result == 0)
                return true;
            else
                return false;
        }

        public bool EnsureCreated()
        {
            RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)_orderDatabaseContext.Database.GetService<IDatabaseCreator>();
            databaseCreator.CreateTables();
            return true;
        }
    }

}
