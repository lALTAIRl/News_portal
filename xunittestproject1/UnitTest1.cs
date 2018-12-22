using Xunit;
using System;
using Microsoft.AspNetCore.Mvc;
using Task2.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Task2.Models;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            UserController controller = new UserController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            //Assert.Equal("Index", result?.ViewData["Title"]);
            Assert.NotNull(result);
            Assert.Equal("Index", result?.ViewName);
        }
    }
}
