using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using News_portal.Controllers;

namespace News_portal.TEST.ControllerTesting
{
    [TestClass]
    public class CreateNewsUnitTest
    {
        [TestMethod]
        public void CreateNewsIsNotNullTest()
        {
            //Arrange
            var newsController = new NewsController(newsService: null, userManager: null, mapper: null);

            //Act
            var result = newsController.CreateNews() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateNewsViewDataIsNotNullTest()
        {
            //Arrange
            var newsController = new NewsController(newsService: null, userManager: null, mapper: null);

            //Act
            var result = newsController.CreateNews() as ViewResult;

            //Assert
            Assert.IsNotNull(result?.ViewData);
        }

    }
}
