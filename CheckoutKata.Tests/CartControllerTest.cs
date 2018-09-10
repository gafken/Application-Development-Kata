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
            Assert.AreEqual(1, controller._cache.First(x => x.Identifier == "jerky").Quantity);
        }

        [TestMethod]
        public void AddSameItemToCartTwiceAddsValueToCacheWithNumberOfItemAsTwo()
        {
            controller.AddItem("jerky");
            controller.AddItem("jerky");
            Assert.IsTrue(controller._cache.Any(x => x.Identifier == "jerky"));
            Assert.AreEqual(2, controller._cache.First(x => x.Identifier == "jerky").Quantity);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddItemNotInInventoryReturnsDetailedException()
        {
            try
            {
                controller.AddItem("NotInInventory");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("NotInInventory not in Inventory so cannot be added to Cart.", e.Message);
                throw;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void UpdateQuantityUpdatesValueInCache()
        {
            controller.AddItem("jerky");
            controller.UpdateQuantity("jerky", 5);
            Assert.IsTrue(controller._cache.Any(x => x.Identifier == "jerky"));
            Assert.AreEqual(5, controller._cache.First(x => x.Identifier == "jerky").Quantity);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void UpdateQuantityOfItemNotInCacheReturnDetailsException()
        {
            try
            {
                controller.UpdateQuantity("jerky", 5);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("jerky not in Cart so quantity cannot be updated.", e.Message);
                throw;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void UpdateQuantityForDifferentItemUpdatesValueInCache()
        {
            controller.AddItem("turkey");
            controller.UpdateQuantity("turkey", 7);
            Assert.IsTrue(controller._cache.Any(x => x.Identifier == "turkey"));
            Assert.AreEqual(7, controller._cache.First(x => x.Identifier == "turkey").Quantity);
        }

        [TestMethod]
        public void UpdateQuantityToDecimalValueItemUpdatesValueInCache()
        {
            controller.AddItem("turkey");
            controller.UpdateQuantity("turkey", 7.25m);
            Assert.IsTrue(controller._cache.Any(x => x.Identifier == "turkey"));
            Assert.AreEqual(7.25m, controller._cache.First(x => x.Identifier == "turkey").Quantity);
        }

        [TestMethod]
        public void RemoveCartItemRemovesValueFromCache()
        {
            controller.AddItem("jerky");
            controller.RemoveItem("jerky");
            Assert.IsFalse(controller._cache.Any(x => x.Identifier == "jerky"));
        }

        [TestMethod]
        public void RemoveDifferentCartItemRemovesValueFromCache()
        {
            controller.AddItem("turkey");
            controller.RemoveItem("turkey");
            Assert.IsFalse(controller._cache.Any(x => x.Identifier == "turkey"));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void RemoveItemNotInCartReturnsDetailedException()
        {
            try
            {
                controller.RemoveItem("jerky");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("jerky not in Cart so it cannot be removed.", e.Message);
                throw;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void CalculateTotalWithNoCartItemsReturnsZero()
        {
            Assert.AreEqual(0, controller.CalcuateTotal());
        }

        [TestMethod]
        public void CalculateTotalWithSingleJerkyReturnOneAndAQuarter()
        {
            controller.AddItem("jerky");
            Assert.AreEqual(1.25m, controller.CalcuateTotal());
        }

        [TestMethod]
        public void CalculateTotalWithSingleTurkeyReturnOneAndAHalf()
        {
            controller.AddItem("turkey");
            Assert.AreEqual(1.50m, controller.CalcuateTotal());
        }

        [TestMethod]
        public void CalculateTotalWithSingleTurkeyAndSingleJerkyReturnTwoAndThreeQuarters()
        {
            controller.AddItem("jerky");
            controller.AddItem("turkey");
            Assert.AreEqual(2.75m, controller.CalcuateTotal());
        }

        [TestMethod]
        public void CalculateTotalWithTwoTurkeyAndThreeJerkyReturnSixAndThreeQuarters()
        {
            controller.AddItem("jerky");
            controller.UpdateQuantity("jerky", 3);

            controller.AddItem("turkey");
            controller.UpdateQuantity("turkey", 2);

            Assert.AreEqual(6.75m, controller.CalcuateTotal());
        }

        [TestMethod]
        public void CalculateTotalWithTwoTurkeyAndThreeJerkyWithAMarkdownOnJerkyReturnSix()
        {
            controller.AddItem("jerky");
            controller.UpdateQuantity("jerky", 3);
            CacheManager.MarkDownCache.Add(new Markdown("jerky", 1));

            controller.AddItem("turkey");
            controller.UpdateQuantity("turkey", 2);

            Assert.AreEqual(6, controller.CalcuateTotal());
        }
    }
}
