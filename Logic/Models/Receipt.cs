using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class Receipt
    {
        public List<Cart> products;
        public double totalPrice;
        public int discount;
    }
}
