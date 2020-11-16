using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public class CouponService
    {
        public void GenerateCoupon()
        {
            throw new Exception("Not implemented");
        }

        public Coupon ValidateCoupon(String coupon)
        {
            return new Coupon { code = coupon, discount = 0.5 };
        }
    }
}
