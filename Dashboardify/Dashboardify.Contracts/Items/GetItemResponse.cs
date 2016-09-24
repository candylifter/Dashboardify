using Dashboardify.Repositories;

namespace Dashboardify.Contracts.Items
{
    public class GetItemResponse : BaseResponse
    {
        public Item Item { get; set; }
    }
}
