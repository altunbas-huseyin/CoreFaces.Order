using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CoreFaces.Order.UnitTest
{
    [TestClass]
    public class OrderServiceTest : BaseTest
    {
        [TestMethod]
        public void OrderInsert()
        {
            CoreFaces.Order.Models.Domain.Order order = new Models.Domain.Order { StatusId = 2, UserId = Guid.NewGuid() };
            List<Models.Domain.OrderItem> orderItems = new List<Models.Domain.OrderItem>();

            _orderDatabaseContext.Database.BeginTransaction();
            try
            {
                Guid result = _orderService.Save(order);

                orderItems.Add(new Models.Domain.OrderItem { ProductId = Guid.NewGuid(), Price = 5, StatusId = 2, Quantity = 1, ProductName = "Ürün Adý", StockCode = "001", Vat = 18, Currency = Helper.Enums.Currency.TL, OrderId = order.Id });
                orderItems.Add(new Models.Domain.OrderItem { ProductId = Guid.NewGuid(), Price = 2, StatusId = 2, Quantity = 2, ProductName = "Ürün Adý 2", StockCode = "002", Vat = 18, Currency = Helper.Enums.Currency.TL, OrderId = order.Id });
                foreach (var item in orderItems)
                { _orderItemService.Save(item); }

                CoreFaces.Order.Models.Domain.OrderAddress orderAddress = new Models.Domain.OrderAddress { StatusId = 2, UserId = Guid.NewGuid(), Name = "Hüseyin", Surname = "altunbaþ", MobilePhone = "5335299862", OrderId = order.Id };
                _orderAddressService.Save(orderAddress);
                _orderDatabaseContext.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                _orderDatabaseContext.Database.RollbackTransaction();
                throw;
            }

           

        }
    }
}
