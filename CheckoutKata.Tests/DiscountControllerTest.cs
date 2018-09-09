using System;
using System.Collections.Generic;
using System.Linq;
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
        public static void LoadCache(TestContext context)
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
            CacheManager.MarkDownCache = new List<Markdown>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            controller = null;
        }

        [TestMethod]
        public void AddNewDiscountValueIsInCache()
        {
            controller.AddDiscount(new Markdown("jerky", 1.23m));
            Assert.IsNotNull(controller._cache.FirstOrDefault(x => x.Identifier == "jerky"));
            Assert.AreEqual(1.23m, controller._cache.FirstOrDefault(x => x.Identifier == "jerky").MarkdownPrice);
        }

        [TestMethod]
        public void AddDifferentDiscountValueIsInCache()
        {
            controller.AddDiscount(new Markdown("turkey", 2.75m));
            Assert.IsNotNull(controller._cache.FirstOrDefault(x => x.Identifier == "turkey"));
            Assert.AreEqual(2.75m, controller._cache.FirstOrDefault(x => x.Identifier == "turkey").MarkdownPrice);
        }
    }
}
