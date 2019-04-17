using CoreShoppingAdminPortal.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject
{
    [TestClass]
    public class Vendor
    {
        static VendorController controller;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            controller = new VendorController();
            context.WriteLine("Controller Instance Created");

        }
        [TestMethod]
        public void TestForIndex()
        {
            //Arrange
            VendorController controller = new VendorController();
            //Act

            IActionResult result = controller.Index();
            ViewResult view = (ViewResult)result;
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        [TestCategory("Create")]
        public void TestFoCreate()
        {
            IActionResult result = controller.Create();
            ViewResult view = (ViewResult)result;
        }
        [TestMethod]
        [TestCategory("Details")]
        [DataRow(3)]
        [DataRow(4)]

        public void TestForDetails(int id)
        {
            ViewResult result = controller.Details(id);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(Vendor));


        }
        [TestMethod]
        [TestCategory("DeleteForId")]
        [DataRow(3)]
        [DataRow(4)]
        [HttpGet]
        public void Delete(int Id)
          {
        ViewResult result = (ViewResult)controller.Delete(Id);
        Assert.IsNotNull(result.Model);
           
        Assert.IsInstanceOfType(result.Model,typeof(Vendor));

        }
       //[TestMethod]
       // [TestCategory("Delete")]
       // [DataRow(3)]
       // [DataRow(4)]
       // [HttpPost]
       // public void Delete(int Id, Vendor a1)
       // {
       //   ViewResult result = controller.Delete(Id);
       //   Assert.IsNotNull(result.Model);
       //    Assert.IsInstanceOfType(result.Model, typeof(Vendor));
       // }


    }
}
