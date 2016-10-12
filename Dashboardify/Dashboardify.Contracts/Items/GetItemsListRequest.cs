using Dashboardify.Models;

namespace Dashboardify.Contracts.Items
{
    public class GetItemsListRequest : BaseRequest
    {
        public int DashboardId { get; set; }
        
        public User User { get; set; }

    }
}
