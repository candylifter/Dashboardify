using Dashboardify.Repositories;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Dashboardify.Service.Classes;

namespace Dashboardify.Service
{
    public class ItemFilters
    {

        
        private readonly ContentHandler _contentHandler = new ContentHandler();
        private ILog logger = LogManager.GetLogger("Dashboardify.Service");
        private ItemsRepository _itemsRepository = new ItemsRepository(ConfigurationManager.ConnectionStrings["GCP"].ConnectionString);
    

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

        public List<UsernameEmailItem> GetEmailContacts(IList<Item> items)
        {
            List<UsernameEmailItem> contactsToSendEmail = new List<UsernameEmailItem>();

            foreach (var item in items)
            {
                if (!item.UserNotified && item.IsActive) //default true in DB
                {
                    var user = _itemsRepository.GetUserByItemId(item.Id);

                    var contactInfo = new UsernameEmailItem();
                    
                    contactInfo.Email = user.Email;

                    contactInfo.ItemName = item.Name;

                    contactInfo.Username = user.Name;

                    contactsToSendEmail.Add(contactInfo);

                    item.UserNotified = true;

                    _itemsRepository.Update(item);
                }
                //prisideti db kur user notified
                //po atsirinkimo suupdatinti kad notifiinta
            }


            return contactsToSendEmail;
        }
        
        
    }
}
