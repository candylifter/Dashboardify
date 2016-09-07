using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboardify.Contracts.Dashboards;
using Dashboardify.Handlers.Dashboards;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Dashboardify.Handlers.Test
{
    [TestFixture]
    public class DashboardHandlerTest
    {
        [Test]
        public void ZeroId_GetDashboardList()
        {
            var request = new GetDashboardsRequest();
            request.UserId = 0;

            var handler = new GetDashboardsHandler("Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");
            var response = handler.Handle(request);

            Assert.True(response.HasErrors);
            Assert.AreEqual(1,response.Errors.Count);

        }
    }
}
