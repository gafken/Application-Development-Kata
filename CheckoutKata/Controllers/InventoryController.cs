using System;
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

        #region Insert
        public void AddNewItem(string name, decimal price)
        {
            if (!_cache.ContainsKey(name))
                _cache.Add(name, price);
            else
                throw new ArgumentException("Item already added.  Recommend UpdatePrice() instead.");
        }

        public void AddNewItem(int id, decimal price)
        {
            AddNewItem(id.ToString(), price);
        }
        #endregion Insert

        #region Update
        public void UpdatePrice(string name, decimal newPrice)
        {
            if (_cache.ContainsKey(name))
                _cache[name] = newPrice;
            else
                throw new Exception($"Item {name} does not exist in the inventory.");
        }

        public void UpdatePrice(int id, decimal price)
        {
            UpdatePrice(id.ToString(), price);
        }
        #endregion Update
    }
}
