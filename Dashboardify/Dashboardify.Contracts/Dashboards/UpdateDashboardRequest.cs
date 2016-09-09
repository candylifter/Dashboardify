using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboardify.Models;

namespace Dashboardify.Contracts.Dashboards
{
    public class UpdateDashboardRequest:BaseRequest
    {
        public DashBoard DashBoard { get; set; }
    }
}
