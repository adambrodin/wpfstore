using Logic.Models;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public class CheckoutService
    {
        private CouponService _couponService;
        private Receipt _receipt;
        public CheckoutService()
        {
            this._couponService = new CouponService();
            this._receipt = new Receipt();
        }
        private void GenerateRecipt(List<Cart> items, string coupon)
        {
            items.ForEach(item =>
            {
                this._receipt.totalPrice += item.price;
            });

            var couponInformation = this._couponService.ValidateCoupon(coupon);
            
            double discount = couponInformation.discount * 10;
            // this._receipt.totalPrice = (int)(this._receipt.totalPrice - (this._receipt.totalPrice * discount));
            this._receipt.totalPrice *= discount;
            this._receipt.products = items;
        }

        public Receipt Checkout(List<Cart> items, String coupon)
        {
            GenerateRecipt(items, coupon);
            return this._receipt;
        }

        public void ClearReceipt()
        {
            this._receipt = new Receipt();
        }
    }
}
