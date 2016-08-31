using System.Collections.Generic;
using Dashboardify.Repositories;

namespace Dashboardify.Contracts.Items
{
    public class GetItemsListResponse : BaseResponse
    {
        public IList<Item> Items { get; set; }
    }
}
