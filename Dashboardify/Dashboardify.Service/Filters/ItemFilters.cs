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
        private ItemsRepository _itemsRepository;
    

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
                var doc = _contentHandler.GetHtmlDocument(item.Website);

                if (doc != null)
                {
                    string newContent = null;

                    try
                    {
                        newContent = _contentHandler.GetContentByXPath(doc, item.XPath);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }

                    if (string.IsNullOrEmpty(newContent))
                    {
                        try
                        {
                            newContent = _contentHandler.GetContentByCSS(doc, item.CSS);
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                        }
                    }

                    if (!string.IsNullOrEmpty(newContent) && item.Content != newContent)
                    {
                        item.Content = newContent;
                        outdatedItems.Add(item);
                    }
                }
            }

            logger.Info("Got outdated items");

            return outdatedItems;
        }

        public List<UsernameEmailItem> GetEmailContacts(IList<Item> items)
        {
            _itemsRepository = new ItemsRepository(ConfigurationManager.ConnectionStrings["GCP"].ConnectionString);

            List<UsernameEmailItem> contactsToSendEmail = new List<UsernameEmailItem>();

            foreach (var item in items)
            {
                if (!item.UserNotified && item.IsActive) 
                {
                    var user = _itemsRepository.GetUserByItemId(item.Id);

                    var contactInfo = new UsernameEmailItem
                    {
                        Email = user.Email,
                        ItemName = item.Name,
                        Username = user.Name
                    };


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
