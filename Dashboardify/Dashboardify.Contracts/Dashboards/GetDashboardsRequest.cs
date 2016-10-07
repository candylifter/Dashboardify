namespace Dashboardify.Contracts.Dashboards 
{
    public class GetDashboardsRequest : BaseRequest
    {
        public int Id { get; set; }

        public string Ticket { get; set; }
    }
}
