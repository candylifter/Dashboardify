using Dashboardify.Repositories;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Dashboardify.Models;

namespace Dashboardify.Service
{
    public class ItemFilters
    {
        private readonly ContentHandler _contentHandler = new ContentHandler();
        private ILog logger = LogManager.GetLogger("Dashboardify.Service");
        private ItemsRepository _itemsRepository = new ItemsRepository("SS");

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

        public List<User> GetEmailContacts(IList<Item> items)
        {
            List<User> emails = new List<User>();

            foreach (var item in items)
            {
                if (!item.UserNotified)
                {
                    emails.Add(_itemsRepository.GetUserByItemId(item.Id));
                }
                //prisideti db kur user notified
                //po atsirinkimo suupdatinti kad notifiinta
            }


            return emails;
        }
    }
}
