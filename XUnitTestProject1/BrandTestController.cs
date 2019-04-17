﻿using EcommerceUserPanel.Controllers;
using EcommerceUserPanel.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject1
{
    class BrandTestController
    {
        private ShoppingDemoooo2Context context;

        public static DbContextOptions<ShoppingDemoooo2Context>
        dbContextOptions
        { get; set; }

        public static string connectionString =
          "Data Source=TRD-517;Initial Catalog=ShoppingDemoooo2;Integrated Security=true;";
        static BrandTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ShoppingDemoooo2Context>()
                .UseSqlServer(connectionString).Options;

        }

        public BrandTestController()
        {
            context = new ShoppingDemoooo2Context(dbContextOptions);

        }

        [Fact]
        public async void Task_GetBrandBy_Id_Return_OkResult()
        {
            var controller = new BrandController(context);
            var ProductCategoryId = 1;
            var data = await controller.Get(ProductCategoryId);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetBrandBy_Id_Return_NoResult()
        {
            var controller = new BrandController(context);
            var ProductCategoryId = 6;
            var data = await controller.Get(ProductCategoryId);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_GetBrandById_MatchResult()
        {
            var controller = new BrandController(context);
            int id = 1;
            var data = await controller.Get(id);
            Assert.IsType<OkObjectResult>(data);
            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var brnd = okResult.Value.Should().BeAssignableTo<Brands>().Subject;
            Assert.Equal("Nike", brnd.BrandName);
            Assert.Equal("Good", brnd.BrandDescription);

        }
        [Fact]
        public async void Task_GetBrandById_BadResult()
        {
            var controller = new BrandController(context);
            int? id = null;
            var data = await controller.Get(id);
            Assert.IsType<BadRequestResult>(data);

        }
    }
}
