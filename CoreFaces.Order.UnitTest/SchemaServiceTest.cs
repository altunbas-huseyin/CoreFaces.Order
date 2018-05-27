
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace CoreFaces.Order.UnitTest
{
    [TestClass]
    public class SchemaServiceTest : BaseTest
    {

        [TestMethod]
        public void DropTables()
        {
            bool result = orderSchemaService.DropTables();
            Assert.AreEqual(result, true);
        }


        [TestMethod]
        public void EnsureCreated()
        {
            bool result = orderSchemaService.EnsureCreated();
            Assert.AreEqual(result, true);
        }

    }

}
