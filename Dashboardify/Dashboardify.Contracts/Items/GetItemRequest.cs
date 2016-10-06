namespace Dashboardify.Contracts.Items
{
    public class GetItemRequest : BaseRequest
    {
        public int ItemId { get; set; }

        public string Ticket { get; set; }

    }
}
