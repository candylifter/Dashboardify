using Dashboardify.Models;

namespace Dashboardify.Contracts.Dashboards
{
    public class UpdateDashboardRequest:BaseRequest
    {
        public DashBoard DashBoard { get; set; }

        public int UserId { get; set; }
    }
}
