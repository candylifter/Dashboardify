using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboardify.Models;

namespace Dashboardify.Contracts.Users
{
    public class DeleteUserRequest : BaseRequest
    {
        public User User { get; set; }
    }
}
