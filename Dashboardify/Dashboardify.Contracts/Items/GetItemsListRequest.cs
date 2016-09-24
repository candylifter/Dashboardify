namespace Dashboardify.Contracts.Items
{
    public class GetItemsListRequest : BaseRequest
    {
        public int DashboarId { get; set; }

        public string Ticket { get; set; }
    }
}
