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
    public class WriterTests
    {
        private string testfile = "product_test.csv";
        [TestMethod]
        public void WriteDataToCsvTest_ValidProductData_SucceedToWriteCsv()
        {
            

            var list = new List<Product>()
            {
               new Product("Bitcoin", "Krypto valuta",  "NULL", 492132),
               new Product("Etherium", "Krypto valuta",  "NULL", 4213),
               new Product("Kappacoin", "Krypto valuta",  "NULL", 4921),
            };

            Writer writer = new Writer();
            writer.WriteDataToCsv(list, testfile);

            Reader reader = new Reader();
            var output = reader.ReadDataFromCsv<Product>(testfile);

            Assert.AreEqual(list.Count, output.Count());
        }

        
    }
}