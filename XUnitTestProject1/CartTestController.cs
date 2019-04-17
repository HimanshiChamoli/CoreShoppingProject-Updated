//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace XUnitTestProject1
//{
//    class CartTestController
//    {
//        private ShoppingDemoooo2Context context;

//        public static DbContextOptions<ShoppingDemoooo2Context>
//        dbContextOptions
//        { get; set; }

//        public static string connectionString =
//          "Data Source=TRD-517;Initial Catalog=ShoppingDemoooo2;Integrated Security=true;";
//        static CartTestController()
//        {
//            dbContextOptions = new DbContextOptionsBuilder<ShoppingDemoooo2Context>()
//                .UseSqlServer(connectionString).Options;

//        }

//        public CartTestController()
//        {
//            context = new ShoppingDemoooo2Context(dbContextOptions);

//        }
//        [Fact]
//        public void Task_Checkout_Return_View()
//        {
//            Assert.Throws<NullReferenceException>(() =>
//            {
//                var controller = new CartController(context);
//                //var CustomerId = 1;
//                var customer = new Customers()
//                {
//                    CustomerId = 1,
//                    FirstName = "Namarata",
//                    LastName = "Sharma",
//                    Username = "Namarat",
//                    Password = "123456",
//                    EmailId = "Namarata@user.com",
//                    Address = "Sarojini",

//                    State = "Delhi",

//                    Country = "India",

//                    PhoneNo = 6890851362,

//                    Zip = 110056,

//                    Gender = "Female",


//                };

//                var data = controller.Checkout(customer);
//                Assert.IsType<ViewResult>(data);
//            });
//        }

//        [Fact]
//        public void Task_remove_Return_View()
//        {
//            Assert.Throws<NullReferenceException>(() =>
//            {
//                var controller = new CartController(context);
//                var Id = 1;
//                var data = controller.Remove(Id);
//                Assert.IsType<ViewResult>(data);
//            });
//        }

//        [Fact]
//        public void Task_Invoice_Return_View()
//        {
//            Assert.Throws<NullReferenceException>(() =>
//            {
//                var controller = new CartController(context);
//                var data = controller.Invoice();
//                Assert.IsType<ViewResult>(data);
//            });
//            //}
//            //[Fact]
//            //public async void Task_Return_GetAllDetails()
//            //{
//            //    var controller = new CustomerController(context);
//            //    var data = await controller.Details();
//            //    Assert.IsType<OkObjectResult>(data);
//            //}
//        }
//    }
//}
