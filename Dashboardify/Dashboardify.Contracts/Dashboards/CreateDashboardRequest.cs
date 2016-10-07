namespace Dashboardify.Contracts.Dashboards
{
    public class CreateDashboardRequest:BaseRequest
    {
        public string DashName { get; set; }

        public int UserId { get; set; }
    }
}
