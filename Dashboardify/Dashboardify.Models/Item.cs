using Dashboardify.Models;
using System;
using System.Collections.Generic;

namespace Dashboardify.Repositories
{
    public class Item
    {       
        public int Id { get; set; }

        public int DashBoardId { get; set; }

        public string Name { get; set; }

        public string Website { get; set; }

        public int CheckInterval { get; set; }

        public bool IsActive { get; set; }

        public string XPath { get; set; }

        public string CSS { get; set; }

        public bool NotifyByEmail { get; set; }

        public bool UserNotified { get; set; }

        public int Failed { get; set; }

        public DateTime LastChecked { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public string Content { get; set; }

        public IList<Screenshot> Screenshots { get; set; }

        public Item()
        {
            Screenshots = new List<Screenshot>();
        }
        
    }
}
