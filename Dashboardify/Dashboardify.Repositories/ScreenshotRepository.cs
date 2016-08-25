using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboardify.Models;

namespace Dashboardify.Repositories
{
    class ScreenshotRepository
    {
        private string _connectionString;

        public ScreenshotRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public Screenshot GetLast(int itemId)
        {
            
        }

    }
}
