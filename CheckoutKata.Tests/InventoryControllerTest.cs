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
        public void ItemPostedToAPIIsInCache()
        {
            controller.AddNewItem("beef sticks", 1.23m);
            Assert.IsTrue(controller._cache.ContainsKey("beef sticks"));
            Assert.AreEqual(1.23m, controller._cache["beef sticks"]);
        }
    }
}
