using Microsoft.VisualStudio.TestTools.UnitTesting;
using News_portal.Controllers;
using Moq;
using News_portal.BLL.Interfaces;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            NewsController newsController = new NewsController(newsService: null, userManager: null, mapper: null);

            //var result = await newsController.ViewNews(id: null);

            //Assert.
  

        }

        [TestMethod]
        public void TestMethod2()
        {
            var mockNewsRepository = new Mock<INewsService>();


        }
    }
}
