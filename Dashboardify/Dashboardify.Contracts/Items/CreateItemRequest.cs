using Dashboardify.Repositories;

namespace Dashboardify.Contracts.Items
{
    public class CreateItemRequest : BaseRequest
    {
        public Item Item { get; set; }

        public int DashId { get; set; }

        public int UserId { get; set; }

    }
}
