using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic.IO;
using Logic.Models;

namespace Logic.Services
{
    public class ProductService
    {
        private Reader _reader;
       
        public ProductService()
        {
            this._reader = new Reader();
        }

        public List<Product> FetchProducts()
        {
            var products = this._reader.ReadDataFromCsv<Product>("products.csv");
            if (products.Any())
            {
                throw new IndexOutOfRangeException("No products found");
            }

            return products.ToList();
        }
    }
}
