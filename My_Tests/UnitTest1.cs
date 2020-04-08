using Asp_Net_MVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace My_Tests
{
    [TestClass]
    public class UnitTest1
    {
        //private Mock<ControllerContext> mockControllerContext;
        //private AccountController sut;

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    mockControllerContext = new Mock<ControllerContext>();
        //    sut = new HomeController();
        //}
        //[TestCleanup]
        //public void TestCleanup()
        //{
        //    sut.Dispose();
        //    mockControllerContext = null;
        //}
        //[TestMethod]
        //public void Index_Should_Return_Default_View()
        //{

        //    // Expectations
        //    mockControllerContext.SetupGet(x => x.HttpContext.Request.ApplicationPath)
        //        .Returns("/foo.com");
        //    sut.ControllerContext = mockControllerContext.Object;

        //    // Act
        //    var failure = sut.Index();

        //    // Assert
        //    Assert.IsInstanceOfType(failure, typeof(ViewResult), "Index() did not return expected ViewResult.");
        //}
    }
}
