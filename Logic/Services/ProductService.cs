using System;
using System.Collections.Generic;
using System.Linq;
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
            IEnumerable<Product> products;

            products = this._reader.ReadDataFromCsvTemp<Product>("products.csv");
            if (products.Any())
            {
                return products.ToList();
            }

            products = this._reader.ReadDataFromCsv<Product>("products.csv");
            if (products.Any())
            {
                return products.ToList();
            }

            throw new IndexOutOfRangeException("No products found");
        }
    }
}
