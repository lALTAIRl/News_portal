using Microsoft.VisualStudio.TestTools.UnitTesting;
using News_portal.Controllers;
using Moq;
using News_portal.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        public void ViewNewsValidIdReturnsNewsDTO()
        {
            //Arrange
            var news = new News
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

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            var mockMapper = new Mock<IMapper>();

            var mockNewsService = new Mock<INewsService>();
            mockNewsService.Setup(repo => repo.GetNewsByIdAsync(It.IsInRange<int>(1,3, Range.Inclusive))).ReturnsAsync(news);

            var newsController = new NewsController(mockNewsService.Object, mockMapper.Object, mockUserManager.Object);

            //Act
            var result = newsController.ViewNews(1).Result as ViewResult;
            var model = result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(News));
        }

        [TestMethod]
        public void ViewNewsInvalidIdReturnsNewsDTO()
        {
            //Arrange
            var news = new News
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

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            var mockMapper = new Mock<IMapper>();

            var mockNewsService = new Mock<INewsService>();
            mockNewsService.Setup(repo => repo.GetNewsByIdAsync(It.Is<int>(id => id > 3))).ReturnsAsync((News)null);

            var newsController = new NewsController(mockNewsService.Object, mockMapper.Object, mockUserManager.Object);

            //Act
            var result = newsController.ViewNews(4).Result as ViewResult;
            var model = result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsNull(result.Model);
        }
    }
}
