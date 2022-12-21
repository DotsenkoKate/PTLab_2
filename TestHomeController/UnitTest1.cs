using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using PTLab_2;
using PTLab_2.Controllers;
using PTLab_2.Models;

namespace TestHomeController
{
    [TestClass]
    public class UnitTest1
    {
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
  
    }
}