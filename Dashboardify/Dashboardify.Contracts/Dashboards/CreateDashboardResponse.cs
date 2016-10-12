using Dashboardify.Models;

namespace Dashboardify.Contracts.Dashboards
{
    public class CreateDashboardResponse:BaseResponse
    {
        public DashBoard Dashboard { get; set; }
    }
}
