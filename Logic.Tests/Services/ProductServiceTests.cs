using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Logic.IO;
using Logic.Models;

namespace Logic.Services.Tests
{
    [TestClass]
    public class ProductServiceTests
    {
        private ProductService _productService = new ProductService();
        private string _filename = "products.csv";
        public void Startup()
        {
            var list = new List<Product>()
            {
               new Product("Bitcoin", "Krypto valuta",  "NULL", 492132),
               new Product("Etherium", "Krypto valuta",  "NULL", 4213),
               new Product("Kappacoin", "Krypto valuta",  "NULL", 4921),
               new Product("Monkeycoin", "Krypto valuta",  "NULL", 48301),
            };

            Writer writer = new Writer();
            writer.WriteDataToCsvTemp(list, _filename);
        }

        [TestCleanup]
        public void Cleanup()
        {
            FileHelper.DeleteTempFile(_filename);
        }

        [TestMethod]
        public void FetchProductsTest_NoProductsInList_ThrowException()
        {
            try
            {
                this._productService.FetchProducts();
            }
            catch (IndexOutOfRangeException iore)
            {
                Assert.AreEqual("No products found", iore.Message);
            }
        }
    }
}