
using Dashboardify.Models;

namespace Dashboardify.Contracts.UserSession
{
    public class LoginUserRequest:BaseRequest
    {
        public User User { get; set; }
    }
}
