using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using News_portal.BLL.Interfaces;
using News_portal.Controllers;
using News_portal.DAL.Entities;
using System;

namespace News_portal.TEST.ControllerTesting
{
    [TestClass]
    public class PublishNewsUnitTest
    {
        [TestMethod]
        public void PublishNewsIsNotNullTest()
        {
            //Arrange
            var newsController = new NewsController(newsService: null, userManager: null, mapper: null);

            //Act
            var result = newsController.PublishNews(1);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PublishNewsIsInstanceOfTypeTest()
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

            var mockNewsService = new Mock<INewsService>();
            mockNewsService.Setup(repo => repo.GetNewsByIdAsync(1)).ReturnsAsync(news);

            var mockMapper = new Mock<IMapper>();

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            var newsController = new NewsController(mockNewsService.Object, mockMapper.Object, mockUserManager.Object);

            //Act
            var result = newsController.PublishNews(1).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
    }
}