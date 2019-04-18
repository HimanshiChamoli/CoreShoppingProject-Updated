
using EcommerceUserPanel.Controllers;
using EcommerceUserPanel.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace XUnitTestProject1
{
    public class ProductCategoryTestController
    {
        private ShoppingDemoooo2Context context;

        public static DbContextOptions<ShoppingDemoooo2Context>
        dbContextOptions
        { get; set; }

        public static string connectionString =
          "Data Source=TRD-517;Initial Catalog=ShoppingDemoooo2;Integrated Security=true;";
        static ProductCategoryTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ShoppingDemoooo2Context>()
                .UseSqlServer(connectionString).Options;

        }

        public ProductCategoryTestController()
        {
            context = new ShoppingDemoooo2Context(dbContextOptions);

        }

        [Fact]

        public async void Task_GetProductCategoryByID_Return_OkResult()
        {
            var controller = new ProductCategoryController(context);
            var productcategoryId = 1;
            var Data = await controller.Get(productcategoryId);
            Assert.IsType<OkObjectResult>(Data);
        }
        [Fact]
        public async void Task_GetProductCategoryByID_Return_NotFoundResult()
        {
            var controller = new ProductCategoryController(context);
            var productcategoryId = 6;
            var Data = await controller.Get(productcategoryId);
            Assert.IsType<NotFoundResult>(Data);
        }
        [Fact]
        public async void Task_GetProductCategoryByID_MatchResult()
        {
            var controller = new ProductCategoryController(context);
            int id = 1;
            var data = await controller.Get(id);
            Assert.IsType<OkObjectResult>(data);
            var OkResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var pro = OkResult.Value.Should().BeAssignableTo<Categories>().Subject;
            Assert.Equal("Summer Collection", pro.CategoryName);
            Assert.Equal("Cotton Material", pro.CategoryDescription);

        }

    }
}
