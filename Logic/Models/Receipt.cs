using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class Receipt
    {
        public List<Product> products;
        public double totalPrice;
        public int discount;
    }
}
