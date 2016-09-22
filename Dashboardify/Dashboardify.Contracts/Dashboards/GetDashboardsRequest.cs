namespace Dashboardify.Contracts.Dashboards 
{
    public class GetDashboardsRequest : BaseRequest
    {
        public int UserId { get; set; }

        public string Ticket { get; set; }
    }
}
