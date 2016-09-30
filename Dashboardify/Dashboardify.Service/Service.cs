using System;
using System.Configuration;
using System.Timers;
using Dashboardify.Service.Workers;
using log4net;

namespace Dashboardify.Service
{
    public class Service
    {
        private readonly Timer _timer;
        private string _connectionString;
        private ILog _logger;

        private ItemContentWorker _itemContentWorker;
        private EmailNotificationWorker _emailNotificationWorker;

        public Service()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["GCP"].ConnectionString;
            _logger = LogManager.GetLogger("Dashboardify.Service");

            _itemContentWorker = new ItemContentWorker(_connectionString, _logger);
            _emailNotificationWorker = new EmailNotificationWorker(_connectionString, _logger);

            _timer = new Timer(Int32.Parse(ConfigurationManager.AppSettings["interval"])) { AutoReset = true };
            _timer.Elapsed += TimeElapsedEventHandler;
        }

        public void TimeElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            DoAction();
        }

        public void DoAction()
        {
            _logger.Info("Check started.");

            _logger.Info("Item content worker started.");

            var items = _itemContentWorker.Do();

            _logger.Info("Items Updated, starting to send emails");

            _logger.Info("Email notification worker started.");

            _emailNotificationWorker.Do(items);

            _logger.Info("Check ended.");
        }

        public void Start()
        {
            _logger.Info("Service started.");
            _timer.Start();
        }

        public void Stop()
        {
            _logger.Info("Service stoped.");
            _timer.Stop();
        }
    }
}