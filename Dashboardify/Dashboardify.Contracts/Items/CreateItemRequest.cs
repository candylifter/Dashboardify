using Dashboardify.Repositories;

namespace Dashboardify.Contracts.Items
{
    public class CreateItemRequest : BaseRequest
    {
        public Item Item { get; set; }

        public string Ticket { get; set; }
    }
}
