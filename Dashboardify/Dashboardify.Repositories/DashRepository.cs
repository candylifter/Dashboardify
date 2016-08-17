using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboardify.Models;

namespace Dashboardify.Repositories
{
    public class DashRepository
    {

        private string _connectionString = "Data Source=.;" +
                                            "Initial Catalog =DashBoardify;" +
                                            "User id=DashBoardify;" +
                                            "Password=123456;";

        private IList<DashBoard> _dashBoards;

        public void UpdateDash(DashBoard dash)
        {
            
        }
    }
}
