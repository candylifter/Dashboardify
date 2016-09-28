using Dashboardify.Repositories;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dashboardify.Service
{
    public class ItemFilters
    {
        private readonly ContentHandler _contentHandler = new ContentHandler();
        private ILog logger = log4net.LogManager.GetLogger("Dashboardify.Service");

        public IList<Item> GetScheduledList(IList<Item> items)
        {
            DateTime now = DateTime.Now;

            var scheduledItems = items.Where(item =>
                item.LastChecked.AddMilliseconds(item.CheckInterval) <= now 
                && item.IsActive
            ).ToList();

            logger.Info("Get schedules list up");

            return scheduledItems;
        }

        public IList<Item> GetOutdatedList(IList<Item> items)
        {
            var outdatedItems = new List<Item>();

            foreach (var item in items)
            {
                var newContent = _contentHandler.GetContentByXPath(item.Website, item.XPath);

                if (newContent == null)
                {
                    newContent = _contentHandler.GetContentByCSS(item.Website, item.CSS);
                }

                if (newContent != null && item.Content != newContent)
                {
                    item.Content = newContent;

                    outdatedItems.Add(item);
                }
            }

            logger.Info("Get outdated items");

            return outdatedItems;
        }
    }
}
