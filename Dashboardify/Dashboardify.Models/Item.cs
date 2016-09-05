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

        [Column(Name="isActive")]
        public bool IsActive { get; set; }

        public string XPath { get; set; }

        public DateTime LastChecked { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return
                String.Format(
                    "Id: {0}, DashBoardId: {1}, Name: {2},Website: {3}, Checkinterval: {4}, IsActive: {5}, XPath: {6}, LastChecked: {7}, Created: {8}, Modified:{9}, Content: {10}",
                    Id, DashBoardId, Name, Website, CheckInterval, IsActive, XPath, LastChecked, Created, Modified,
                    Content);
        }
    }
}
