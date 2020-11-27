using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.IO;
using System;
using System.Collections.Generic;
using System.Text;
using Logic.Models;
using System.Linq;

namespace Logic.IO.Tests
{
  
    [TestClass]
    public class ReaderTests
    {
        private string _filename = "product_test.csv";
        [TestInitialize]
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
        public void ReadDataFromCsv_ProductObject_ValidProductObject()
        {
            Reader reader = new Reader();
            var data = reader.ReadDataFromCsvTemp<Product>(_filename);

            Assert.AreEqual(4, data.Count());
        }

        
    }
}