namespace Dashboardify.Contracts.Users
{
    public class ForgotPasswordRequest:BaseRequest
    {
        public string Email { get; set; }
    }
}
