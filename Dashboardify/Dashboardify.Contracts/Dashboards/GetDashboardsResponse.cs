using Dashboardify.Models;
using System.Collections.Generic;

namespace Dashboardify.Contracts.Dashboards
{
    public class GetDashboardsResponse : BaseResponse
    {
        public IList<DashBoard> Items { get; set; }
    }
}
