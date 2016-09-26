namespace Dashboardify.Contracts.Items
{
    public class GetItemsListRequest : BaseRequest
    {
        public int DashboardId { get; set; }

        public string Ticket { get; set; }
    }
}
