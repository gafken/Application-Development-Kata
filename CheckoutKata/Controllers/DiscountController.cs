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

        public void AddDiscount(Markdown discount) //This would be a nice place to use an Abstarct Base Class
        {
            _cache.Add(discount);
        }
    }
}
