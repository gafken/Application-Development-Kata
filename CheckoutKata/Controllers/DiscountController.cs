using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CheckoutKata.Models;

namespace CheckoutKata.Controllers
{
    public class DiscountController : ApiController
    {
        internal List<Markdown> _cache => CacheManager.MarkDownCache;
        private Dictionary<string, InventoryItem> inventoryCache => CacheManager.InventoryCache;

        public void AddDiscount(Markdown discount) //This would be a nice place to use an Abstarct Base Class
        {
            if (!inventoryCache.ContainsKey(discount.Identifier))
                throw new ArgumentException($"{discount.Identifier} is not in the inventory, so you cannot apply a markdown.");

            if (!_cache.Any(x => x.Identifier == discount.Identifier))
                _cache.Add(discount);
            else
                throw new ArgumentException($"Markdown already exists for {discount.Identifier}.  Recommend UpdateMarkdown()");
        }

        public decimal GetPrice(string discountName)
        {
            var discount = _cache.FirstOrDefault(x => x.Identifier == discountName);

            if (discount != null)
                return discount.MarkdownPrice;
            else
                throw new ArgumentException($"{discountName} markdown was not available in the inventory.");
        }

        public void UpdateDiscount(string discountName, decimal newPrice)
        {
            var discount = _cache.FirstOrDefault(x => x.Identifier == discountName);

            if (discount != null)
                discount.MarkdownPrice = newPrice;
            else
                throw new ArgumentException($"{discountName} markdown not available and must be added before using update.");
        }

        public bool DeleteDiscount(string discountName)
        {
            var discount = _cache.FirstOrDefault(x => x.Identifier == "jerky");

            if (discount != null)
            {
                _cache.Remove(discount);
                return true;
            }

            return false;
        }
    }
}
