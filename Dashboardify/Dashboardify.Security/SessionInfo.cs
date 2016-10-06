using Dashboardify.Models;

namespace Dashboardify.Security
{
    public class SessionInfo
    {
        public UserSession Session { get; set; }

        public User User { get; set; }
    }
}
