using System;
using System.Collections.Generic;
using System.Linq;
using Dashboardify.Repositories;
using Dashboardify.Service.Classes;
using log4net;

namespace Dashboardify.Service.Workers
{
    public class ItemContentWorker
    {
        private readonly string _connectionString;
        private ILog _logger;

        private readonly ItemsRepository _itemsRepository;
        private readonly ScreenshotRepository _screenshotRepository;

        private readonly ItemFilters _itemFilters = new ItemFilters();
        private readonly ContentHandler _contentHandler = new ContentHandler();

        public ItemContentWorker(string connectionString, ILog logger)
        {
            if(connectionString == null) throw new ArgumentException("connectionString");
            if(logger == null) throw new ArgumentException("logger");

            _connectionString = connectionString;
            _logger = logger;

            _itemsRepository = new ItemsRepository(_connectionString);
            _screenshotRepository = new ScreenshotRepository(_connectionString);
        }

        public void Do()
        {
            //var result = new List<UsernameEmailItem>();
            //GET ALL ITEMS
            var items = _itemsRepository.GetList();
            _logger.Info("\n --> ITEMS FROM DATABASE");
            _logger.Info("\n -->");

            foreach (var item in items)
            {
                _logger.Info(item.Name);
            }
            _logger.Info("GETTING ITEMS TO CHECK");
            
            //ITEMS TO CHECK
            _logger.Info("\n -->ITEMS TO CHECK");
            var scheduledItems = _itemFilters.GetScheduledList(items);

            foreach (var item in scheduledItems)
            {
                _logger.Info(item.Name);
            }

            //ITEMS WITH NEW CONTENT
            _logger.Info("\n -- > ITEMS WITH NEW CONTENT");

            var outdatedItems = _itemFilters.GetOutdatedList(scheduledItems);

            foreach (var item in outdatedItems)
            {
                _logger.Info(item.Name);
            }

            //UPDATING ITEMS
            _logger.Info("\n --> UPDATING ITEMS");

            UpdateOutdatedItems(outdatedItems);

            UpdateNonOutdatedItems(scheduledItems, outdatedItems);
            
            //FILTERING ITEMS TO SEND EMAIL
            //result = _itemFilters.GetEmailContacts(outdatedItems);

            //_logger.Info($"Changed items to send email: {result.Count}");

            //return result;
        }
        
        public void UpdateOutdatedItems(IList<Item> items)
        {

            foreach (var item in items)
            {

                var task = _contentHandler.GetScreenshot(item);

                string filename = task.Result;

                if (filename != null)
                {
                    _logger.Info($"Got screenshot of: {item.Name}");
                    var now = DateTime.Now;

                    item.LastChecked = now;
                    item.Modified = now;
                    item.UserNotified = false; //krc

                    _itemsRepository.Update(item);

                    Models.Screenshot screenshot = new Models.Screenshot();

                    screenshot.ItemId = item.Id;
                    screenshot.ScrnshtURL = filename;
                    screenshot.DateTaken = now;

                    _screenshotRepository.Create(screenshot);

                    _logger.Info("Updated item: " + item.Name);
                }
                else
                {
                    if (item.Failed >= 3)  {
                        item.IsActive = false;
                    }
                    else
                    {
                        item.Failed++;
                    }

                    _itemsRepository.Update(item);
                    _logger.Info($"Failed to get screenshot of item: {item.Name}");
                }


            }
        }

        public void UpdateNonOutdatedItems(IList<Item> allItems, IList<Item> outdatedItems)
        {
            var items = allItems.Where(a => !outdatedItems.Any(o => o.Id == a.Id)).ToList();

            _logger.Info("\n\nNot outdated items:");
            foreach (var item in items)
            {
                _logger.Info(item.Name);

                if (item.Failed >= 3)
                {
                    item.IsActive = false;
                } else if (item.Content.Length == 0)
                {
                    item.Failed++;
                }

                item.LastChecked = DateTime.Now;

                _itemsRepository.Update(item);
            }
            _logger.Info("Updated not outdated items");
        }
    }
}
