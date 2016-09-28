namespace Dashboardify.Contracts.Users
{
    public class UpdateUserRequest : BaseRequest
    {
        public string Ticket { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

    }
}
