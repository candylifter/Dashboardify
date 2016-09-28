namespace Dashboardify.Contracts.Dashboards
{
    public class CreateDashboardRequest:BaseRequest
    {
        public string Ticket { get; set; }

        public string DashName { get; set; }
    }
}
