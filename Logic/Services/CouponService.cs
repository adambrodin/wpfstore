using Logic.IO;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Services
{
    public class CouponService
    {
        private Reader reader;
        private List<Coupon> availableCoupons;
        public CouponService()
        {
            reader = new Reader();
            availableCoupons = reader.ReadDataFromCsv<Coupon>("coupons.csv").ToList();
        }
        public void GenerateCoupon()
        {
            throw new Exception("Not implemented");
        }

        public Coupon ValidateCoupon(String code)
        {
            return availableCoupons.Where(c => c.code == code).FirstOrDefault();
        }

        public List<Coupon> FetchCoupons()
        {
            var coupons = this.reader.ReadDataFromCsv<Coupon>("coupons.csv");
            if (!coupons.Any())
            {
                throw new IndexOutOfRangeException("No coupons found");
            }

            return coupons.ToList();
        }
    }
}
