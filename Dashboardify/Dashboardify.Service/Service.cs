using System;
using System.Timers;

namespace Dashboardify.Service
{
    public class Service
    {
        private readonly Timer _timer;
        public Service()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += TimeElapsedEventHandler;
        }

        public void TimeElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Testing");
        }

        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }



    }


}
