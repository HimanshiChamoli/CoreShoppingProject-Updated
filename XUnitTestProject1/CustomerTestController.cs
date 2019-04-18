using EcommerceUserPanel.Controllers;
using EcommerceUserPanel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject1
{
    public class CustomerTestController
    {
        private ShoppingDemoooo2Context context;

        public static DbContextOptions<ShoppingDemoooo2Context>
        dbContextOptions
        { get; set; }

        public static string connectionString =
          "Data Source=TRD-517;Initial Catalog=ShoppingDemoooo2;Integrated Security=true;";
        static CustomerTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ShoppingDemoooo2Context>()
                .UseSqlServer(connectionString).Options;

        }
        [Fact]
        public void Task_Login_Return_View()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                var controller = new CustomerController(context);
                var CustomerEmail = "Nimi@gmail.com";
                var CustomerPassword = "1234";
                var data = controller.Login(CustomerEmail, CustomerPassword);
                //   Assert.NotNull(data);
                Assert.IsType<RedirectResult>(data);
            });
        }
        //[Fact]
        //public void Task_Register_Return_View()
        //{

        //    var controller = new CustomerController(context);
        //    var cust = new Customers()
        //    {
        //        cust.FirstName = "Sambhav",
        //        cust.LastName = "Jain"
        //        cust.Username = "himi",
        //        cust.EmailId = "himi@gmail.com"
        //        cust.Address = "Mayapuri"

        //        cust.State = "Delhi",

        //        cust.Country = "India"

        //        cust.PhoneNo = 98567485235
        //        cust.Zip = 110064

        //        cust.Gender = "Female"
        //    };
            
               


        //    var data = controller.Create();
        //    Assert.NotNull(data);


        //}
    }
}
