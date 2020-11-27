using Logic.Models;
using System;
using System.Collections.Generic;

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
        private void GenerateReceipt(List<Product> items, string coupon)
        {
            items.ForEach(item =>
            {
                this._receipt.totalPrice += item.price;
            });

            var couponInformation = this._couponService.ValidateCoupon(coupon);
            if (couponInformation != null)
            {
                this._receipt.discount = couponInformation.discount * 100;
            }
            this._receipt.products = items;
        }

        public Receipt Checkout(List<Product> items, String coupon = "")
        {
            GenerateReceipt(items, coupon);
            return this._receipt;
        }

        public void ClearReceipt()
        {
            this._receipt = new Receipt();
        }
    }
}
