using Dashboardify.Models;

namespace Dashboardify.Contracts.Users
{
    public class UpdateUserRequest : BaseRequest
    {
        public User User { get; set; }
    }
}
