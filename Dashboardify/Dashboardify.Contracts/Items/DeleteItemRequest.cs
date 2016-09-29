namespace Dashboardify.Contracts.Items
{
    public class DeleteItemRequest: BaseRequest
    {
        public int ItemId { get; set; }

        public string Ticket { get; set; }
    }
}
