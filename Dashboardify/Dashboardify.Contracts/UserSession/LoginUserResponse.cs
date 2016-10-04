

using System;

namespace Dashboardify.Contracts.UserSession
{
    public class LoginUserResponse:BaseResponse
    {
        public string Ticket { get; set; }

        public DateTime ExpireDate { get; set; }
    }
}
