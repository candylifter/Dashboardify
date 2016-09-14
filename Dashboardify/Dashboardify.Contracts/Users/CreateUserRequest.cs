namespace Dashboardify.Contracts.Users
{
    public class CreateUserRequest:BaseRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
