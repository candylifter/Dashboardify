using Dashboardify.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace Dashboardify.Service.Test
{
    [TestFixture]
    public class ItemFiltersTest
    {
        private ItemFilters _itemFilters;

        [Test]
        public void ShouldFilterActiveItems()
        {
            _itemFilters = new ItemFilters();

            var items = new List<Item>
            {
                new Item()
                {
                    Id = 1,
                    LastChecked = DateTime.MinValue,
                    CheckInterval = 10000,
                    IsActive = true
                },
                new Item()
                {
                    Id = 2,
                    LastChecked = DateTime.MinValue,
                    CheckInterval = 10000,
                    IsActive = false
                },
                new Item()
                {
                    Id = 3,
                    LastChecked = DateTime.MinValue,
                    CheckInterval = 300000,
                    IsActive = true
                }

                
            };

            var expectedItems = items.Where(x => x.Id != 2).ToList();
            var actualItems = _itemFilters.GetScheduledList(items);

            Assert.AreEqual(expectedItems, actualItems);
        }

        [Test]
        public void ShouldFilterByCheckInterval()
        {
            _itemFilters = new ItemFilters();

            var items = new List<Item>
            {
                new Item()
                {
                    Id = 1,
                    CheckInterval = 10,
                    LastChecked = DateTime.Now.AddMilliseconds(-10),
                    IsActive = true
                },
                new Item()
                {
                    Id = 2,
                    CheckInterval = 10,
                    LastChecked = DateTime.Now.AddMilliseconds(-9),
                    IsActive = false
                },
                new Item()
                {
                    Id = 3,
                    CheckInterval = 10,
                    LastChecked = DateTime.Now.AddMilliseconds(-11),
                    IsActive = true
                }
            };

            var expectedItems = items.Where(x => x.Id != 2).ToList();
            var actualItems = _itemFilters.GetScheduledList(items);

            Assert.AreEqual(expectedItems, actualItems);
        }
    }
}
