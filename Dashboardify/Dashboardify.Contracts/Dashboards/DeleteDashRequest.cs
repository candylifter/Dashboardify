namespace Dashboardify.Contracts.Dashboards
{
    public class DeleteDashRequest:BaseRequest
    {
        public int DashboardId { get; set; }

        public int UserId { get; set; }
    }
}
