using Microsoft.VisualStudio.TestTools.UnitTesting;
using News_portal.Controllers;
using Moq;
using News_portal.BLL.Interfaces;
using System;
using Microsoft.AspNetCore.Identity;
using News_portal.DAL.Entities;
using AutoMapper;

namespace News_portal.TEST.ServiseTesting
{
    [TestClass]
    public class NewsServiseUnitTest
    {
        [TestMethod]
        public void NewsServiceTesting()
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

            //Act
            var result = mockNewsService.Object.GetNewsByIdAsync(1).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(News));
            Assert.AreEqual(news, result);
        }
    }
}
