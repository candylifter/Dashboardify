namespace Dashboardify.Contracts.Dashboards
{
    public class DeleteDashRequest:BaseRequest
    {
        public string Ticket { get; set; }

        public string DashName { get; set; }
    }
}
