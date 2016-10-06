using Dashboardify.Repositories;

namespace Dashboardify.Contracts.Items
{
    public class UpdateItemRequest:BaseRequest
    {
        public Item Item { get; set; }
       
        public int UserId { get; set; }
       
    }
}
