using Dashboardify.Repositories;

namespace Dashboardify.Contracts.Items
{
    public class UpdateItemRequest:BaseRequest
    {
       
        public int CheckInterval { get; set; }

        public int ItemId { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool NotifyByEmail { get; set; }

       
    }
}
