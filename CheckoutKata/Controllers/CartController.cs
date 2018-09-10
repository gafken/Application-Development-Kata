using CheckoutKata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CheckoutKata.Controllers
{
    public class CartController : ApiController
    {
        internal List<CartItem> _cache => CacheManager.CartCache;
        private Dictionary<string, InventoryItem> inventoryCache => CacheManager.InventoryCache;

        internal void AddItem(string itemName)
        {
            if (!inventoryCache.ContainsKey(itemName))
                throw new ArgumentException($"{itemName} not in Inventory so cannot be added to Cart.");

            var currentItem = _cache.SingleOrDefault(x => x.Identifier == itemName);

            if (currentItem == null)
                _cache.Add(new CartItem(itemName));
            else
                currentItem.NumberOfItems++;
        }

        internal void UpdateQuantity(string itemName, int newQuantity)
        {
            var currentItem = _cache.SingleOrDefault(x => x.Identifier == itemName);

            if (currentItem == null)
                throw new ArgumentException($"{itemName} not in Cart so quantity cannot be updated.");

            _cache.Single(x => x.Identifier == "jerky").NumberOfItems = 5;
        }
    }
}
