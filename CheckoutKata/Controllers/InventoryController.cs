using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using CheckoutKata.Models;

namespace CheckoutKata.Controllers
{
    public class InventoryController : ApiController
    {
        internal Dictionary<string, InventoryItem> _cache => CacheManager.InventoryCache;

        #region Insert
        public void AddNewItem(InventoryItem item)
        {
            if (!_cache.ContainsKey(item.Identifier))
                _cache.Add(item.Identifier, item);
            else
                throw new ArgumentException("Item already added.  Recommend UpdatePrice() instead.");
        }

        public void AddNewItem(string name, decimal price)
        {
            if (!_cache.ContainsKey(name))
            {
                var item = new InventoryItem(name, price);
                _cache.Add(name, item);
            }
            else
                throw new ArgumentException("Item already added.  Recommend UpdatePrice() instead.");
        }

        public void AddNewItem(int id, decimal price)
        {
            AddNewItem(id.ToString(), price);
        }
        #endregion Insert

        #region Read
        public decimal GetPrice(string name)
        {
            if (_cache.ContainsKey(name))
                return _cache[name].Price;
            else
                throw new ArgumentException($"Item {name} does not exist in the cache and must be inserted."); //only other option here is to return -1
                                                                                                               //I don't want a -1 being used on the user side erroneously
                                                                                                               //so this seemed the most graceful way at first glance.
        }

        public decimal GetPrice(int id)
        {
            return GetPrice(id.ToString());
        }

        public IEnumerable<InventoryItem> SearchFor(string queryString)
        {
            return _cache.Where(x => x.Key.Contains(queryString)).Select(x => x.Value);
        }
        #endregion Read

        #region Update
        public void UpdatePrice(string name, decimal newPrice)
        {
            if (_cache.ContainsKey(name))
                _cache[name].Price = newPrice;
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
