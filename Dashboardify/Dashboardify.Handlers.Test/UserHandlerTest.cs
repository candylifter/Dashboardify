using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


            //var ex2 = Assert.Throws(, () => handler.Handle(request));
            //var ex = Assert.Throws<SystemException>(() => handler.Handle(request));

            //Assert.That(ex.Message, Is.EqualTo("User does not exist!"));

            Assert.That(() => handler.Handle(request),
                Throws.Exception.With.Property("Message").EqualTo("User does not exist!"));


        }
    }
}
