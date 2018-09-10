using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckoutKata.Models
{
    public class CartItem
    {
        public CartItem(string identifier)
        {
            Identifier = identifier;
            Quantity = 1;
        }

        public string Identifier { get; set; }

        public decimal Quantity { get; set; }
    }
}