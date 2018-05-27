using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using CoreFaces.Order.Repositories;
using CoreFaces.Order.Services;
using CoreFaces.Order.Models.Models;

namespace CoreFaces.Order.UnitTest
{
    public class BaseTest
    {
        public OrderDatabaseContext _orderDatabaseContext;
        public readonly IOrderService _orderService;
        public readonly IOrderAddressService _orderAddressService;
        public readonly IOrderItemService _orderItemService;

        public readonly IOrderSchemaService orderSchemaService;
        public readonly HttpClient _client;
        public readonly IOptions<OrderSettings> _orderSettings;
        public readonly IHttpContextAccessor iHttpContextAccessor;
        public BaseTest()
        {
           
            DbContextOptionsBuilder<OrderDatabaseContext> orderBuilder = new DbContextOptionsBuilder<OrderDatabaseContext>();
            var connectionString = "server=localhost;userid=root;password=123456;database=Product;";
            orderBuilder.UseMySql(connectionString);
            //.UseInternalServiceProvider(serviceProvider); //burası postgress ile sıkıntı çıkartmıyor, fakat mysql'de çalışmıyor test esnasında hata veriyor.

            _orderDatabaseContext = new OrderDatabaseContext(orderBuilder.Options);
            //_context.Database.Migrate();

            //Configuration
            iHttpContextAccessor = new HttpContextAccessor { HttpContext = new DefaultHttpContext() };
            _orderSettings = Options.Create(new OrderSettings()
            {
                FileUploadFolderPath = @"C:\Users\haltunbas\Documents\GitHub\ProductV2\Product.Api\Product.Api\wwwroot\upload\"
            });

            _orderService = new OrderService(_orderDatabaseContext, _orderSettings, iHttpContextAccessor);
            _orderItemService = new OrderItemService(_orderDatabaseContext, _orderSettings, iHttpContextAccessor);
            orderSchemaService = new OrderSchemaService(_orderDatabaseContext, _orderSettings, iHttpContextAccessor);
            _orderAddressService = new OrderAddressService(_orderDatabaseContext, _orderSettings, iHttpContextAccessor);


        }
    }

}
