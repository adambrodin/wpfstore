using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public class CheckoutService
    {
        private Receipt _receipt = new Receipt();
        private void GenerateRecipt(List<Cart> items)
        {
            items.ForEach(item =>
            {
                this._receipt.totalPrice += item.price;
            });
            this._receipt.discount = 0;
            this._receipt.products = items;
        }

        public Receipt Checkout(List<Cart> items)
        {
            GenerateRecipt(items);
            return this._receipt;
        }

        public void ClearReceipt()
        {
            this._receipt = new Receipt();
        }
    }
}
