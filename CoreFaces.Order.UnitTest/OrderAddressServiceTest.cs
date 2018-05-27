using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CoreFaces.Order.UnitTest
{
    [TestClass]
    public class OrderAddressServiceTestTest : BaseTest
    {
        [TestMethod]
        public void OrderAddressInsert()
        {
            CoreFaces.Order.Models.Domain.OrderAddress orderAddress = new Models.Domain.OrderAddress { StatusId = 2, UserId = Guid.NewGuid(), Name="Hüseyin", Surname="altunbaþ", MobilePhone="5335299862" };
            Guid result = _orderAddressService.Save(orderAddress);
        }
    }
}
