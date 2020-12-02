using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Logic.Models;

namespace Logic.Services.Tests
{
    [TestClass()]
    public class CartServiceTests
    {
        private List<Product> products;
        private CartService testCartService;

        [TestInitialize]
        public void Startup()
        {
            testCartService = new CartService();
            products = new List<Product>()
            {
                new Product("item 1", "an item", "undefined.png", 5000),
                new Product("item 2", "an item", "undefined.png", 5000),
                new Product("item 3", "an item", "undefined.png", 5000),
                new Product("item 4", "an item", "undefined.png", 5000),
            };
        }

        [TestMethod()]
        public void LoadCartWithValidData()
        {
            products.ForEach(p => testCartService.AddItemToCart(p));
            var result = testCartService.GetCart();
            Assert.AreEqual(products.Count, result.Count);
        }

        [TestMethod()]
        public void RemoveItemFromCartTest()
        {
            products.ForEach(p => testCartService.AddItemToCart(p));
            int testIndex = 1;
            testCartService.RemoveItemFromCart(testIndex);

            var result = testCartService.GetCart();
            Assert.AreEqual(products.Count - 1, result.Count);
        }
    }
}