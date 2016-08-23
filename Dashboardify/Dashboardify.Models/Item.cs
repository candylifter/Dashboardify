using System;

namespace Dashboardify.Repositories
{
    public class Item
    {
        
        public int Id { get; set; }

        public int DashBoardId { get; set; }

        public string Name { get; set; }

        public string Website { get; set; }

        public int CheckInterval { get; set; }

        public bool isActive { get; set; }

        public string XPath { get; set; }

        public DateTime LastChecked { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public string ScrnshtURL { get; set; }

        public string Content { get; set; }
    }
}
