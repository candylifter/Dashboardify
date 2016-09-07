using Dashboardify.Repositories;

namespace Dashboardify.Contracts.Items
{
    public class UpdateItemResponse : BaseResponse
    {
        public Item Item { get; set; }
    }
}
