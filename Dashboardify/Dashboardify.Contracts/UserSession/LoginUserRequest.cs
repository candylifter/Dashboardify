
using Dashboardify.Models;

namespace Dashboardify.Contracts.UserSession
{
    public class LoginUserRequest:BaseRequest
    {
        public User user { get; set; }
    }
}
