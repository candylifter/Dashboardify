using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboardify.Models
{
    public class UserSession
    {
        public string Id { get; set; }

        public int UserId { get; set; }

        public DateTime Expires { get; set; }
         
    }
}
