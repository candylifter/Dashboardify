namespace Dashboardify.Contracts.Items
{
    public class DeleteItemRequest: BaseRequest
    {
        public int ItemId { get; set; }

        public int UserId { get; set; }

    }
}
