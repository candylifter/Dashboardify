using Dashboardify.Models;

namespace Dashboardify.Contracts.Users
{
    public class UpdateUserRequest : BaseRequest
    {
        
        public User UserToUpdate { get; set; }

        public User UserOrigin { get; set; }

        
        

    }
}
