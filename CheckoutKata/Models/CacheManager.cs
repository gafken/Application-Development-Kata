using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckoutKata.Models
{
    internal static class CacheManager //normally I woudn't make this class static
                                       //but this project lacks some underlying infrastructure
    {
        internal static Dictionary<string, InventoryItem> InventoryCache = new Dictionary<string, InventoryItem>();

        internal static List<CartItem> CartCache = new List<CartItem>();
    }
}