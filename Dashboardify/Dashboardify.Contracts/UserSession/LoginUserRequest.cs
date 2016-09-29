namespace Dashboardify.Contracts.UserSession
{
    public class LoginUserRequest:BaseRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
