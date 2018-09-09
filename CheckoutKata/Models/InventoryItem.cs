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

        //This should have a UPC property as well for scanning
        //For simplicity purposes, I will assume Identifier will act as
        //A name text field and the UPC

        public string Identifier { get; set; }

        public decimal Price { get; set; }
    }
}