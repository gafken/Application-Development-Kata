using System;
using CheckoutKata.Controllers;
using CheckoutKata.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckoutKata.Tests
{
    [TestClass]
    public class DiscountControllerTest
    {
        private DiscountController controller { get; set; }

        [ClassInitialize]
        public void LoadCache()
        {
            CacheManager.InventoryCache.Add("UPC1", new InventoryItem("jerky", 1.25m));
            CacheManager.InventoryCache.Add("UPC2", new InventoryItem("turkey", 1.5m));
            CacheManager.InventoryCache.Add("UPC3", new InventoryItem("ham", 1.75m));
            CacheManager.InventoryCache.Add("UPC4", new InventoryItem("pepper", 2m));
            CacheManager.InventoryCache.Add("UPC5", new InventoryItem("pepperoni", 2.25m));
            CacheManager.InventoryCache.Add("UPC6", new InventoryItem("ground beef", 2.5m));
            CacheManager.InventoryCache.Add("UPC7", new InventoryItem("bacon", 2.75m));
            CacheManager.InventoryCache.Add("UPC8", new InventoryItem("salami", 3m));
            CacheManager.InventoryCache.Add("UPC9", new InventoryItem("veal", 3.25m));
        }

        [TestInitialize]
        public void Initialize()
        {
            controller = new DiscountController();
        }

        [TestCleanup]
        public void Cleanup()
        {
            controller = null;
        }
    }
}
