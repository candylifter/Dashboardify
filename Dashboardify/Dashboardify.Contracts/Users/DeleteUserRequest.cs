using Dashboardify.Models;

namespace Dashboardify.Contracts.Users
{
    public class DeleteUserRequest : BaseRequest
    {
        public string Ticket { get; set; }

        public int userId { get; set; }
    }
}
