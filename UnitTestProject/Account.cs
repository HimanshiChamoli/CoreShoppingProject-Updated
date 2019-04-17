//using CoreShoppingAdminPortal.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using CoreShoppingAdminPortal.Models;
//using System;

//namespace UnitTestProject
//{
//    [TestClass]
//    public class UnitTest1
//    {
//        //static AccountController controller;

//        [ClassInitialize]

//        //public static void ClassInit(TestContext context)
//        //{
//        //    controller = new AccountController();
//        //    context.WriteLine("AccountController Instance Created");

//        //}

//        [TestMethod]
//        //public void TestForIndexAction()
//        //{
//        //    //Arrange
//        //    AccountController controller = new AccountController();
//        //    //Act

//        //    IActionResult result = controller.Index();
//        //    ViewResult view = (ViewResult)result;
//        //    //Assert
//        //    Assert.IsNotNull(result);
//        //    Assert.IsInstanceOfType(result, typeof(ViewResult));
//        //}
       
//        [TestCategory("LoginTest")]
//        [ExpectedException(typeof(NullReferenceException))]
//        public void TestForIfLogin()
//        {    //Act
//            ViewResult result = (ViewResult)controller.Login("admin", "123456");
//            //ViewResult view = (ViewResult)result;
//            Assert.IsNotNull(result.Model);

//            Assert.IsInstanceOfType(result.Model, typeof(string));
//            //Account account = (Account)result.Model;
//            Assert.AreEqual(result.ViewName, "Home");
//        }
//        [TestMethod]
//        [TestCategory("LoginTest")]
//          public void TestForElseLogin()
//         {
//             ViewResult result = (ViewResult)controller.Login("Admin", "1234");

//            string teststring = "Invalid Credentials";
//             string data = (string)result.ViewData["Error"];
//            Assert.IsNotNull(result);
//            Assert.IsInstanceOfType(result.ViewName, typeof(string));
//            Assert.AreEqual(result.ViewName,"Index");
//            Assert.AreEqual(teststring,data);
//          }
//        [TestMethod]
//       [TestCategory("LogoutTest")]
//        [ExpectedException(typeof(NullReferenceException))]
//        public void TestForLogout()
//        {
//            ViewResult result = (ViewResult)controller.Logout();
//            Assert.IsNotNull(result);
//            //Assert.IsInstanceOfType(result.ViewName, typeof(string));
//            Assert.AreEqual(result.ViewName, "Index");
//        }

//    }

//    }

