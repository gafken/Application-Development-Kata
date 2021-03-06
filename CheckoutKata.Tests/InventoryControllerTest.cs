﻿using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using CheckoutKata.Controllers;
using CheckoutKata.Models;

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
            CacheManager.InventoryCache = new Dictionary<string, InventoryItem>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            controller = null;
        }

        #region Insert
        [TestMethod]
        public void BeefSticksPostedToAPIIsInCache()
        {
            controller.AddNewItem("beef sticks", 1.23m);
            Assert.IsTrue(controller._cache.ContainsKey("beef sticks"));
            Assert.AreEqual(1.23m, controller.GetPrice("beef sticks"));
        }

        [TestMethod]
        public void SausagePostedToAPIIsInCache()
        {
            controller.AddNewItem("suasage", 1.23m);
            Assert.IsTrue(controller._cache.ContainsKey("suasage"));
            Assert.AreEqual(1.23m, controller.GetPrice("suasage"));
        }

        [TestMethod]
        public void BrisketPostedToAPIWithTwoDollarValueIsInCache()
        {
            controller.AddNewItem("brisket", 2m);
            Assert.IsTrue(controller._cache.ContainsKey("brisket"));
            Assert.AreEqual(2m, controller.GetPrice("brisket"));
        }

        [TestMethod]
        public void AddItemByIdToAPIWithTwoDollarValueIsInCache()
        {
            controller.AddNewItem(1, 2m);
            Assert.IsTrue(controller._cache.ContainsKey("1"));
            Assert.AreEqual(2m, controller.GetPrice("1"));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddDuplicateItemThrowsArgumentException()
        {
            try
            {
                controller.AddNewItem("long johns", 2);
                controller.AddNewItem("long johns", 1);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Item already added.  Recommend UpdatePrice() instead.", e.Message);
                throw;
            }

            Assert.Fail(); //should not be hit because of throw
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddDuplicateItemByIDThrowsArgumentException()
        {
            try
            {
                controller.AddNewItem("long johns", 2);
                controller.AddNewItem("long johns", 1);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Item already added.  Recommend UpdatePrice() instead.", e.Message);
                throw;
            }

            Assert.Fail(); //should not be hit because of throw
        }

        [TestMethod]
        public void AddItemByClassValueIsInCache()
        {
            controller.AddNewItem(new InventoryItem("id", 1.23m));
            Assert.IsTrue(controller._cache.ContainsKey("id"));
            Assert.AreEqual(1.23m, controller.GetPrice("id"));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddDuplicateItemByClassThrowsArgumentException()
        {
            try
            {
                controller.AddNewItem(new InventoryItem("long johns", 2));
                controller.AddNewItem(new InventoryItem("long johns", 1));
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Item already added.  Recommend UpdatePrice() instead.", e.Message);
                throw;
            }

            Assert.Fail(); //should not be hit because of throw
        }
        #endregion Insert

        #region Read
        [TestMethod]
        public void GetItemThatExistsInCacheReturnsPrice()
        {
            controller.AddNewItem("bread", 2.15m);
            Assert.AreEqual(2.15m, controller.GetPrice("bread"));
        }

        [TestMethod]
        public void GetItemByIDThatExistsInCacheReturnsPrice()
        {
            controller.AddNewItem(1, 2.15m);
            Assert.AreEqual(2.15m, controller.GetPrice(1));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetItemThatDoesntExistsInCacheThrowsDetailedException()
        {
            try
            {
                controller.GetPrice("bread");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Item bread does not exist in the cache and must be inserted.", e.Message);
                throw;
            }

            Assert.Fail();
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetItemByIDThatDoesntExistsInCacheThrowsDetailedException()
        {
            try
            {
                controller.GetPrice(1);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Item 1 does not exist in the cache and must be inserted.", e.Message);
                throw;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void SearchItemReturnsListTheContainQueryString()
        {
            var expected = new List<InventoryItem>(new[] {
                new InventoryItem("inresults1", 1.23m), new InventoryItem("inresults2", 1.58m)
            });

            foreach (var item in expected)
            {
                controller.AddNewItem(item.Identifier, item.Price);
            }

            controller.AddNewItem("notfound1", 3m);
            controller.AddNewItem("notfound2", 4m);

            controller.SearchFor("results");
        }
        #endregion Read

        #region Update
        [TestMethod]
        public void UpdatePriceOfExistingItemUpdatesValue()
        {
            controller.AddNewItem("slim jim", 1);
            controller.UpdatePrice("slim jim", 2);
            Assert.AreEqual(2, controller.GetPrice("slim jim"));
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
            Assert.AreEqual(2, controller.GetPrice("1"));
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void UpdatePriceOfNonExistingItemByIDThrowsDetailedError()
        {
            controller.UpdatePrice(1, 3);
        }
        #endregion Update
    }
}
