using System;
using System.Collections.Generic;
using Dashboardify.Repositories;
using Dashboardify.Service.Classes;
using Dashboardify.Service.Helpers;
using log4net;

namespace Dashboardify.Service.Workers
{
    public class EmailNotificationWorker
    {
        private readonly string _connectionString;
        private readonly UsersRepository _usersRepository;
        private readonly ItemsRepository _itemsRepository;
        private readonly ScreenshotRepository _screenshotRepository;
        private readonly ItemFilters _itemFilters;
        private ILog _logger;

        public EmailNotificationWorker(string connectionString, ILog logger) //netinka pavadinimas worker
        {
            if(connectionString == null) throw new ArgumentException("connectionString");
            if(logger == null) throw new ArgumentException("logger");

            _connectionString = connectionString;
            _logger = logger;

            _usersRepository = new UsersRepository(_connectionString);
            _itemsRepository = new ItemsRepository(_connectionString);
            _screenshotRepository = new ScreenshotRepository(_connectionString);
            _itemFilters = new ItemFilters();
        }

        public void Do()
        {
            //_logger.Info($"Items to send email: {items.Count}");
            
            //// GAUTI USER INFO IR SUFORMUOTI NAUJA SARASA
            
            //foreach (var item in items)
            //{
            //    _logger.Info($"Sending mail to: {item.Email}");

            //    EmailSenderHelper.SendMessage(item);
            //}
            //_logger.Info("Emails sent");

            _logger.Info("Getting list of items");

            var items = _itemsRepository.GetList();
            var notifyItems = _itemFilters.GetNotifyItems(items);

            SendEmails(notifyItems);

        }

        public void SendEmails(IList<Item> items)
        {
            foreach (var item in items)
            {
                var userId = _itemsRepository.GetUserByItemId(item.Id).Id;
                var user = _usersRepository.Get(userId);
                var screenshot = _screenshotRepository.GetLastsByItemId(item.Id, 2);

                if (screenshot.Count >= 2)
                {
                    item.Screenshots = screenshot;
                    EmailSenderHelper.SendMessage(user, item);

                    item.UserNotified = true;
                    _itemsRepository.Update(item);
                }

            }
        }
    }
}
