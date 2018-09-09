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
            CacheManager.InventoryCache.Add("jerky", new InventoryItem("jerky", 1.25m));
            CacheManager.InventoryCache.Add("turkey", new InventoryItem("turkey", 1.5m));
            CacheManager.InventoryCache.Add("ham", new InventoryItem("ham", 1.75m));
            CacheManager.InventoryCache.Add("pepper", new InventoryItem("pepper", 2m));
            CacheManager.InventoryCache.Add("pepperoni", new InventoryItem("pepperoni", 2.25m));
            CacheManager.InventoryCache.Add("ground beef", new InventoryItem("ground beef", 2.5m));
            CacheManager.InventoryCache.Add("bacon", new InventoryItem("bacon", 2.75m));
            CacheManager.InventoryCache.Add("salami", new InventoryItem("salami", 3m));
            CacheManager.InventoryCache.Add("veal", new InventoryItem("veal", 3.25m));
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
            Assert.AreEqual(1.23m, controller.GetPrice("jerky"));
        }

        [TestMethod]
        public void AddDifferentDiscountValueIsInCache()
        {
            controller.AddDiscount(new Markdown("turkey", 2.75m));
            Assert.IsNotNull(controller._cache.FirstOrDefault(x => x.Identifier == "turkey"));
            Assert.AreEqual(2.75m, controller.GetPrice("turkey"));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddMoreThanOneMarkdownForAnItemReturnsDetailedError()
        {
            controller.AddDiscount(new Markdown("turkey", 2.75m));

            try
            {
                controller.AddDiscount(new Markdown("turkey", 2.50m));
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Markdown already exists for turkey.  Recommend UpdateMarkdown()", e.Message);
                throw;
            }

            Assert.Fail();
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddMarkdownForItemNotInInventoryReturnsDetailedError()
            //Probably best to ensure other devs know they didn't load the item properly
        {
            try
            {
                controller.AddDiscount(new Markdown("whopper", 2.50m));
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("whopper is not in the inventory, so you cannot apply a markdown.", e.Message);
                throw;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void GetMarkdownPriceOfMarkDownInCache()
        {
            controller.AddDiscount(new Markdown("turkey", 2.50m));
            Assert.AreEqual(2.50m, controller.GetPrice("turkey"));
        }
    }
}
