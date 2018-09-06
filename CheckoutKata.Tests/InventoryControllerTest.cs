using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CheckoutKata.Controllers;

namespace CheckoutKata.Tests
{
    [TestClass]
    public class InventoryControllerTest
    {
        private InventoryController controller { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            controller = new InventoryController();
        }

        [TestCleanup]
        public void Cleanup()
        {
            controller = null;
        }

        [TestMethod]
        public void BeefSticksPostedToAPIIsInCache()
        {
            controller.AddNewItem("beef sticks", 1.23m);
            Assert.IsTrue(controller._cache.ContainsKey("beef sticks"));
            Assert.AreEqual(1.23m, controller._cache["beef sticks"]);
        }

        [TestMethod]
        public void SausagePostedToAPIIsInCache()
        {
            controller.AddNewItem("suasage", 1.23m);
            Assert.IsTrue(controller._cache.ContainsKey("suasage"));
            Assert.AreEqual(1.23m, controller._cache["suasage"]);
        }

        [TestMethod]
        public void BrisketPostedToAPIWithTwoDollarValueIsInCache()
        {
            controller.AddNewItem("brisket", 2m);
            Assert.IsTrue(controller._cache.ContainsKey("brisket"));
            Assert.AreEqual(2m, controller._cache["brisket"]);
        }

        [TestMethod]
        public void AddItemByIdToAPIWithTwoDollarValueIsInCache()
        {
            controller.AddNewItem(1, 2m);
            Assert.IsTrue(controller._cache.ContainsKey("1"));
            Assert.AreEqual(2m, controller._cache["1"]);
        }

        [TestMethod]
        public void UpdatePriceOfExistingItemUpdatesValue()
        {
            controller.AddNewItem("slim jim", 1);
            controller.UpdatePrice("slim jim", 2);
            Assert.AreEqual(2, controller._cache["slim jim"]);
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void UpdatePriceOfNonExistingItemThrowsDetailedError()
        {
            controller.UpdatePrice("no items exist", 3);
        }

        [TestMethod]
        public void UpdatePriceOfExistingItemByIDUpdatesValue()
        {
            controller.AddNewItem(1, 1);
            controller.UpdatePrice(1, 2);
            Assert.AreEqual(2, controller._cache["1"]);
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void UpdatePriceOfNonExistingItemByIDThrowsDetailedError()
        {
            controller.UpdatePrice(1, 3);
        }
    }
}
