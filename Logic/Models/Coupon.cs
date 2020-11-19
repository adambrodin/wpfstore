using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class Coupon
    {
        public Coupon(string code, double discount)
        {
            this.code = code;
            this.discount = discount;
        }

        public string code { get; set; }
        public double discount { get; set; }
    }
}
