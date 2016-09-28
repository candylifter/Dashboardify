using System;
using System.Configuration;
using System.Timers;
using Dashboardify.Repositories;
using System.Collections.Generic;
using System.Linq;
using log4net;

namespace Dashboardify.Service
{
    public class Service
    {
        string connectionString = ConfigurationManager.ConnectionStrings["GCP"].ConnectionString;
        private readonly Timer _timer;
        private readonly ItemsRepository _itemsRepository;
        private readonly ScreenshotRepository _screenshotRepository;
        private readonly ItemFilters _itemFilters = new ItemFilters();
        private readonly ContentHandler _contentHandler = new ContentHandler();
        private ILog logger = log4net.LogManager.GetLogger("Dashboardify.Service");

        public Service()
        {
            Console.WriteLine(connectionString);
            _timer = new Timer(Int32.Parse(ConfigurationManager.AppSettings["interval"])) { AutoReset = true };
            _timer.Elapsed += TimeElapsedEventHandler;

            _itemsRepository = new ItemsRepository(connectionString);
            _screenshotRepository = new ScreenshotRepository(connectionString);

            //UpdateItems();
            //Console.WriteLine("--> Completed updating items");
            //logger.Info("--> Completed updating items");

        }


        public void TimeElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            //var startTime = DateTime.Now;
            UpdateItems();
            //Console.WriteLine("\n Cycle completed in {0} miliseconds\n", (DateTime.Now - startTime).TotalMilliseconds.ToString());
            //Console.WriteLine("--> Completed updating items");
            logger.Info("--> Completed updating items");
        }

        public void UpdateItems()
        {
            //Console.WriteLine("\n->  Updating\n");
            logger.Info("\n->  Updating\n");

            //Console.WriteLine("\n\nItems from db:\n");
            logger.Info("\n\nItems from db:\n");

            var items = _itemsRepository.GetList();
            foreach (var item in items)
                logger.Info(item.Name);
                //Console.WriteLine(item.Name);


            logger.Info("\n\nScheduled items:\n");
            //Console.WriteLine("\n\nScheduled items:\n");
            var scheduledItems = _itemFilters.GetScheduledList(items);
            foreach (var item in scheduledItems)
                logger.Info(item.Name);
                //Console.WriteLine(item.Name);

            logger.Info("\n\nOutdated items:\n");
            //Console.WriteLine("\n\nOutdated items:\n");
            var outdatedItems = _itemFilters.GetOutdatedList(scheduledItems);
            foreach (var item in outdatedItems)
                logger.Info(item.Name);
                //Console.WriteLine(item.Name);

            UpdateNonOutdatedItems(items, outdatedItems);

            UpdateOutdatedItems(outdatedItems);
        }

        public void UpdateOutdatedItems(IList<Item> items)
        {
            foreach (var item in items)
            {

                var task = _contentHandler.GetScreenshotAsync(item);

                string filename = task.Result;

                if (filename != null)
                {
                    var now = DateTime.Now;

                    item.LastChecked = now;
                    item.Modified = now;

                    _itemsRepository.Update(item);

                    Models.Screenshot screenshot = new Models.Screenshot();

                    screenshot.ItemId = item.Id;
                    screenshot.ScrnshtURL = filename;
                    screenshot.DateTaken = now;

                    _screenshotRepository.Create(screenshot);

                    logger.Info("Updated item: " + item.Name);
                    //Console.WriteLine("Updated item: " + item.Name);
                }
                else
                {
                    logger.Info("Cannot get screenshot");
                    //Console.WriteLine("Cannot get screenshot");
                }

          
            }
        }

        public void UpdateNonOutdatedItems(IList<Item> allItems, IList<Item> outdatedItems)
        {
            var items = allItems.Where(a => !outdatedItems.Any(o => o.Id == a.Id)).ToList();

            logger.Info("\n\nNot outdated items:");
            //Console.WriteLine("\n\nNot outdated items:");
            foreach(var item in items)
            {
                //Console.WriteLine(item.Name);
                logger.Info(item.Name);


                item.LastChecked = DateTime.Now;

                _itemsRepository.Update(item);
            }
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}