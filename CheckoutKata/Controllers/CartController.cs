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

        internal void AddItem(string itemName)
        {
            _cache.Add(new CartItem(itemName));
        }
    }
}
