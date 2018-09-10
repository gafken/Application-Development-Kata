using CheckoutKata.Controllers;
using CheckoutKata.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.Tests
{
    [TestClass]
    public class CartControllerTest
    {
        private CartController controller { get; set; }

        [ClassInitialize]
        public static void LoadInventory(TestContext context)
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
            controller = new CartController();
            CacheManager.CartCache = new List<CartItem>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            controller = null;
        }

        [TestMethod]
        public void AddItemToCartAddsValueToCache()
        {
            controller.AddItem("jerky");
            Assert.IsTrue(controller._cache.Any(x => x.Identifier == "jerky"));
        }

        [TestMethod]
        public void AddDifferntItemToCartAddsValueToCache()
        {
            controller.AddItem("turkey");
            Assert.IsTrue(controller._cache.Any(x => x.Identifier == "turkey"));
        }

        [TestMethod]
        public void AddNewItemToCartAddsValueToCacheWithNumberOfItemAsOne()
        {
            controller.AddItem("jerky");
            Assert.IsTrue(controller._cache.Any(x => x.Identifier == "jerky"));
            Assert.AreEqual(1, controller._cache.First(x => x.Identifier == "jerky").NumberOfItems);
        }

        [TestMethod]
        public void AddSameItemToCartTwiceAddsValueToCacheWithNumberOfItemAsTwo()
        {
            controller.AddItem("jerky");
            controller.AddItem("jerky");
            Assert.IsTrue(controller._cache.Any(x => x.Identifier == "jerky"));
            Assert.AreEqual(2, controller._cache.First(x => x.Identifier == "jerky").NumberOfItems);
        }
    }
}
