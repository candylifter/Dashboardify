using Dashboardify.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dashboardify.Service
{
    public class ItemFilters
    {
        private readonly ContentHandler _contentHandler = new ContentHandler();

        public IList<Item> GetScheduledList(IList<Item> items)
        {
            DateTime now = DateTime.Now;

            var scheduledItems = items.Where(item =>
                item.LastChecked.AddMilliseconds(item.CheckInterval) <= now 
                && item.IsActive == true
            ).ToList();

            return scheduledItems;
        }

        public IList<Item> GetOutdatedList(IList<Item> items)
        {
            DateTime now = DateTime.Now;

            var outdatedItems = new List<Item>();

            foreach (var item in items)
            {
                var newContent = _contentHandler.GetContentByXPath(item.Website, item.XPath);

                if (newContent != null && item.Content != newContent)
                {
                    item.Content = newContent;

                    outdatedItems.Add(item);
                }
            }

            return outdatedItems;
        }
    }
}
