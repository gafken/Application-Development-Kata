using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckoutKata.Models
{
    public class InventoryItem
    {
        public InventoryItem(string identifier, decimal price)
        {
            Identifier = identifier;
            Price = price;
        }

        public string Identifier { get; set; }

        public decimal Price { get; set; }
    }
}