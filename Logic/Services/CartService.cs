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
        private List<Cart> _currentCart;
        private Reader _reader;
        private Writer _writer;
        public CartService()
        {
            this._reader = new Reader();
            this._writer = new Writer();
            this._currentCart = new List<Cart>();
        }
        public void SaveCart()
        {
            this._writer.WriteDataToCsv(this._currentCart, "saved_cart.csv");
        }

        public List<Cart> LoadCart()
        {
            var cart = this._reader.ReadDataFromCsv<Cart>("saved_cart.csv");
            if (cart.Any())
            {
                throw new IndexOutOfRangeException("Could not load saved cart");
            }

            return this._currentCart;
        }

        public List<Cart> GetCart()
        {
            return this._currentCart;
        }

        public void ClearCart()
        {
            this._currentCart.Clear();
        }

        public void RemoveItemFromCart(Cart item)
        {
            this._currentCart.Remove(item);
        }

        public List<Cart> AddItemToCart(Product product)
        {
            var item = new Cart { productName = product.title, price = product.price };
            this._currentCart.Add(item);

            return this.GetCart();
        }
    }
}
