using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Logic.Models;

namespace Logic.Services.Tests
{
    [TestClass()]
    public class CheckoutServiceTests
    {
        private CheckoutService checkoutService;

        [TestInitialize]
        public void StartUp()
        {
            checkoutService = new CheckoutService();
        }

        [TestMethod()]
        public void CheckoutTest_NoItemsInCart()
        {
            try
            {
                checkoutService.Checkout(new List<Product>() { }, "");
            }
            catch (IndexOutOfRangeException iore)
            {
                Assert.AreEqual("You cannot check out without any items in cart!", iore.Message);
            }
        }

        [TestMethod()]
        public void CheckoutTest_WithValidItems()
        {
            var receipt = checkoutService.Checkout(new List<Product>()
            {
              new Product("sample item", "sample description", "image.png", 5000),
              new Product("sample item", "sample description", "image.png", 5000),
              new Product("sample item", "sample description", "image.png", 5000),
              new Product("sample item", "sample description", "image.png", 5000),
              new Product("sample item", "sample description", "image.png", 5000),
            });

            Assert.AreEqual("25000, 0", $"{receipt.totalPrice}, {receipt.discount}");
        }
    }
}