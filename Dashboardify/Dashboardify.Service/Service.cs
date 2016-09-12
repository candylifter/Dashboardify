using System;
using System.Configuration;
using System.Timers;
using Dashboardify.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Dashboardify.Service
{
    public class Service
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBZygis"].ConnectionString;
        private readonly Timer _timer;
        private readonly ItemsRepository _itemsRepository;
        private readonly ScreenshotRepository _screenshotRepository;
        private readonly ItemFilters _itemFilters = new ItemFilters();
        private readonly ContentHandler _contentHandler = new ContentHandler();


        public Service()
        {
            Console.WriteLine(connectionString);
            _timer = new Timer(Int32.Parse(ConfigurationManager.AppSettings["interval"])) { AutoReset = true };
            _timer.Elapsed += TimeElapsedEventHandler;

            _itemsRepository = new ItemsRepository(connectionString);
            _screenshotRepository = new ScreenshotRepository(connectionString);

            UpdateItems();
        }


        public void TimeElapsedEventHandler(object sender, ElapsedEventArgs e)
        {

            UpdateItems();
            Console.WriteLine("--> Completed updating items");
            //Console.ReadLine();
            //_timer.Stop();
        }

        public void UpdateItems()
        {
            Console.WriteLine("\n->  Updating\n");



            Console.WriteLine("\n\nItems from db:\n");
            var items = _itemsRepository.GetList();
            foreach (var item in items)
                Console.WriteLine(item.Name);


            Console.WriteLine("\n\nScheduled items:\n");
            var scheduledItems = _itemFilters.GetScheduledList(items);
            foreach (var item in scheduledItems)
                Console.WriteLine(item.Name);

            Console.WriteLine("\n\nOutdated items:\n");
            var outdatedItems = _itemFilters.GetOutdatedList(scheduledItems);
            foreach (var item in outdatedItems)
                Console.WriteLine(item.Name);

            UpdateNonOutdatedItems(items, outdatedItems);


            foreach (var item in outdatedItems)
            {
                string filename = _contentHandler.GetScreenshot(item);

                var now = DateTime.Now;

                item.LastChecked = now;
                item.Modified = now;

                _itemsRepository.Update(item);

                Models.Screenshot screenshot = new Models.Screenshot();

                screenshot.ItemId = item.Id;
                screenshot.ScrnshtURL = filename;
                screenshot.DateTaken = now;

                _screenshotRepository.Create(screenshot);

                Console.WriteLine("Updated item: " + item.Name);
            }

        }

        public void UpdateNonOutdatedItems(IList<Item> allItems, IList<Item> outdatedItems)
        {
            var items = allItems.Where(a => !outdatedItems.Any(o => o.Id == a.Id)).ToList();

            Console.WriteLine("\n\nNot outdated items:");
            foreach(var item in items)
            {
                Console.WriteLine(item.Name);

                item.LastChecked = DateTime.Now;

                _itemsRepository.Update(item);
            }

            //List<Firm> results = Firms.Where(f => !TrackedFirms.Any(t => t.FirmId = f.FirmId)).ToList();
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