using Microsoft.VisualStudio.TestTools.UnitTesting;
using News_portal.Controllers;
using Moq;
using News_portal.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using News_portal.BLL.DTO;
using System;
using Microsoft.AspNetCore.Identity;
using News_portal.DAL.Entities;
using AutoMapper;

namespace News_portal.TEST.ControllerTesting
{
    [TestClass]
    public class ViewNewsUnitTest
    {
        [TestMethod]
        public void ViewNewsTesting()
        {
            //Arrange
            var news = new NewsDTO
            {
                Id = 1,
                Caption = "test",
                Description = "test",
                Text = "test",
                ImageURL = "test",
                DateOfCreating = DateTime.Now,
                DateOfPublishing = DateTime.Now,
                IsPublished = true,
                NewsApplicationUsers = null
            };

            var mockNewsService = new Mock<INewsService>();
            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            var mockMapper = new Mock<IMapper>();

            mockNewsService.Setup(repo => repo.GetNewsByIdAsync(1)).ReturnsAsync(news);

            var newsController = new NewsController(mockNewsService.Object, mockMapper.Object, mockUserManager.Object);

            //Act
            var result = newsController.ViewNews(1).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IActionResult));
        }

    }
}
