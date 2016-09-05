using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboardify.Contracts.Users;
using Dashboardify.Handlers.Users;
using Dashboardify.Models;
using NUnit.Framework;

namespace Dashboardify.Handlers.Test
{
    [TestFixture]
    public class UserHandlerTest
    {
        [Test]
        public void NullUser_Update()
        {
            var request = new UpdateUserRequest();

            request.User = new User();


            var handler =
                new UpdateUserHandler(
                    "Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");



            var response = handler.Handle(request);





        }
    }
}
