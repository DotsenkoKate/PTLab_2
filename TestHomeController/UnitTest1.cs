using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PTLab_2.Controllers;
using PTLab_2.Models;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Web;

namespace TestHomeController
{
    [TestClass]
    public class UnitTest1
    {

        private readonly Mock<HomeController> _mock = new();

        [TestMethod]
        public void LoginTest_InvalidLogin()
        {
            string expected = "Пользователь не найден!";

            HomeController controller = new HomeController();
            ViewResult action = controller.Login("444", "41324") as ViewResult;

            Assert.AreEqual(expected, action.ViewData["Message"]);
        }
        [TestMethod]
        public void LoginTest_CorrectLogin()
        {
            string expected = "Index";

            HomeController controller = new HomeController();
            RedirectToActionResult view = controller.Login("123", "123") as RedirectToActionResult;

            Assert.AreEqual(expected, view.ActionName);
        }
        [TestMethod]
        public void GetDiscountTest_CorrectAmountOfPurchase()
        {
            int purchases = 1000;
            int expected = 10;

            int discount = HomeController.GetDiscount(purchases);

            Assert.AreEqual(expected, discount);
        }
        [TestMethod]
        public void GetDiscountTest_NegativeAmountOfPurchases()
        {
            int purchases = -20000;
            int expected = 15;

            int discount = HomeController.GetDiscount(purchases);

            Assert.AreEqual(expected, discount);
        }
        [TestMethod]
        public void IndexTest_GetCatalog()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var res = controller.Index();
            ViewResult view = res.Result as ViewResult;

            // Assert
            Assert.IsNotNull(view.Model);
        }
        [TestMethod]
        public void AddToCartTest()
        {

            var sessionMock = new Mock<ISession>();
            byte[]? value = Encoding.UTF8.GetBytes("1");
            sessionMock.Setup((s) => s.TryGetValue("id", out value)).Returns(true);
            HomeController controller = new HomeController();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.HttpContext.Session = sessionMock.Object;

            var result = controller.AddToCart(1,1) is RedirectToActionResult;

            Assert.IsTrue(result);

        }
        [TestMethod]
        public void BuyTest()
        {
            var expected = "Index";
            var sessionMock = new Mock<ISession>();
            byte[]? value = Encoding.UTF8.GetBytes("1");
            sessionMock.Setup((s) => s.TryGetValue("id", out value)).Returns(true);
            HomeController controller = new HomeController();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.HttpContext.Session = sessionMock.Object;

            var result = controller.Buy() as RedirectToActionResult;

            Assert.AreEqual(expected, result.ActionName);

        }
        public void CartTest()
        {

            var sessionMock = new Mock<ISession>();
            byte[]? value = Encoding.UTF8.GetBytes("1");
            sessionMock.Setup((s) => s.TryGetValue("id", out value)).Returns(true);
            HomeController controller = new HomeController();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.HttpContext.Session = sessionMock.Object;

            var result = controller.Cart() is ViewResult;

            Assert.IsTrue(result);

        }
    }
}