using System.Linq;
using Dashboardify.Contracts.Dashboards;
using Dashboardify.Handlers.Dashboards;
using Dashboardify.Models;
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
            request.UserId = 0; //check again

            var handler = new GetDashboardsHandler("Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");
            var response = handler.Handle(request);

            Assert.True(response.HasErrors);
            

        }

        [Test]
        public void NotFound_UpdateDash()
        {
            var request = new UpdateDashboardRequest();
            request.DashBoard = new DashBoard() {Id = 15};


            var handler = new UpdateDashBoardHandler("Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");
            var response = handler.Handle(request);

            Assert.True(response.HasErrors);
            Assert.AreEqual("DASH_NOT_FOUND", response.Errors.First().Code);
        }


    }

}
