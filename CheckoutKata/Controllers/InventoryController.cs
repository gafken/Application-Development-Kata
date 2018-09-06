﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CheckoutKata.Controllers
{
    public class InventoryController : ApiController
    {
        public InventoryController()
        {
            _cache = new Dictionary<string, decimal>();
        }

        internal Dictionary<string, decimal> _cache { get; set; }

        public void AddNewItem(string name, decimal price)
        {
            _cache.Add(name, price);
        }

        public void AddNewItem(int id, decimal price)
        {
            _cache.Add(id.ToString(), price);
        }

        internal void UpdatePrice(string name, int newPrice)
        {
            if (_cache.ContainsKey(name))
                _cache[name] = newPrice;
            else
                throw new Exception($"Item {name} does not exist in the inventory.");
        }
    }
}
