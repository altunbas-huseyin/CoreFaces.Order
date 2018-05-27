using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CoreFaces.Order.Models.Models;
using CoreFaces.Order.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Order.Services
{

    public interface IOrderSchemaService
    {
        bool DropTables();
        bool EnsureCreated();
    }

    public class OrderSchemaService : IOrderSchemaService
    {
        private readonly OrderDatabaseContext _orderDatabaseContext;
        private readonly IOrderSchemaRepository _schemaRepository;
        public OrderSchemaService(OrderDatabaseContext orderDatabaseContext, IOptions<OrderSettings> productSettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _orderDatabaseContext = orderDatabaseContext;
            _schemaRepository = new OrderSchemaRepository(_orderDatabaseContext, productSettings, iHttpContextAccessor);
        }

        public bool DropTables()
        {
            return _schemaRepository.DropTables();
        }

        public bool EnsureCreated()
        {
            return _schemaRepository.EnsureCreated();
        }
    }

}
