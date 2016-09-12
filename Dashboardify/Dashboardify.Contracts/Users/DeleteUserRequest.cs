using Dashboardify.Models;

namespace Dashboardify.Contracts.Users
{
    public class DeleteUserRequest : BaseRequest
    {
        public User User { get; set; }
    }
}
