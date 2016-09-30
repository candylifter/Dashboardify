using System;
using System.Collections.Generic;
using Dashboardify.Service.Classes;
using Dashboardify.Service.Helpers;
using log4net;

namespace Dashboardify.Service.Workers
{
    public class EmailNotificationWorker
    {
        private readonly string _connectionString;
        private ILog _logger;

        public EmailNotificationWorker(string connectionString, ILog logger) //netinka pavadinimas worker
        {
            if(connectionString == null) throw new ArgumentException("connectionString");
            if(logger == null) throw new ArgumentException("logger");

            _connectionString = connectionString;
            _logger = logger;
        }

        public void Do(IList<UsernameEmailItem> items)
        {
            _logger.Info($"Items to send email: {items.Count}");
            
            // GAUTI USER INFO IR SUFORMUOTI NAUJA SARASA
            
            foreach (var item in items)
            {
                _logger.Info($"Sending mail to: {item.Email}");

                EmailSenderHelper.SendMessage(item);
            }
            _logger.Info("Emails sent");
        }
    }
}
