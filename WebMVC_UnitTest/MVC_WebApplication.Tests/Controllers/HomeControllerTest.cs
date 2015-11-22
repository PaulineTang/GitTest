using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC_WebApplication;
using MVC_WebApplication.Controllers;
using System.Web;

namespace MVC_WebApplication.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        #region 代码重构，避免写很多重复的代码
        private HomeController controller;
        private ViewResult result;

        [TestInitialize]
        public void SetupContext()
        {
            controller = new HomeController();
            result = controller.About();
        }
        #endregion


        [TestMethod]
        public void Index()
        {
            // Arrange
            //HomeController controller = new HomeController();

            // Act
            ViewResult result1 = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result1);
            Assert.AreEqual("",result.ViewName);
        }

        [TestMethod]
        public  void AboutShouldAskForDefaultView()
        {
            //Arrange
            //HomeController controller = new HomeController();

            //Act
            //ViewResult result = controller.About();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("",result.ViewName);

        }
        [TestMethod]
        public void AboutShouldSetMessageInViewBag()
        {
            // Arrange
            //HomeController controller = new HomeController();

            // Act
            //ViewResult result = controller.About() as ViewResult;
            ViewResult result = controller.About();

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            //HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            
        }
    }
}
