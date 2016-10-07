namespace Dashboardify.Contracts.UserSession
{
    public class LogoutUserRequest:BaseRequest
    {
        public int UserId { get; set; }
    }
}
