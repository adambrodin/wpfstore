using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic.IO;
using Logic.Models;

namespace Logic.Services
{
    
    public class CartService
    {
        private List<Product> _currentCart;
        private Reader _reader;
        private Writer _writer;
        public CartService()
        {
            this._reader = new Reader();
            this._writer = new Writer();
            this._currentCart = new List<Product>();
        }
        public void SaveCart()
        {
            this._writer.WriteDataToCsv(this._currentCart, "saved_cart.csv");
        }

        public List<Product> LoadCart()
        {
            this.ClearCart();
            this._currentCart = this._reader.ReadDataFromCsv<Product>("saved_cart.csv").ToList();
            if (!this._currentCart.Any())
            {
                throw new IndexOutOfRangeException("Could not load saved cart");
            }

            return this._currentCart;
        }

        public List<Product> GetCart() 
        {
            return this._currentCart;
        }

        public void ClearCart()
        {
            this._currentCart.Clear();
        }

        public void RemoveItemFromCart(int index)
        {
            this._currentCart.RemoveAt(index);
        }

        public List<Product> AddItemToCart(Product product)
        {
            this._currentCart.Add(product);
            return this.GetCart();
        }
    }
}
