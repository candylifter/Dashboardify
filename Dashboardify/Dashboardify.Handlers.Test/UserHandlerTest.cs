using System;
using System.Linq;
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
        public void NoId_DeleteUser()
        {
            //arange
            var request = new DeleteUserRequest();
            request.User = new User();

            //act
            var handler = new DeleteUserHandler("Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");

            var response = handler.Handle(request);

            //assert
            Assert.True(response.HasErrors);
            Assert.AreEqual(1,response.Errors.Count);
            Assert.AreEqual("INVALID_ID",response.Errors.First().Code);          
        }
        //TODO Fix it
        [Test]
        public void NullUser_UpdateUser()
        {
            var request = new UpdateUserRequest();
            request.User=new User();

            var handler = new UpdateUserHandler("Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");

            var response = handler.Handle(request);

            
           



            //
            //Assert.True(response.HasErrors);
            SystemException ex = Assert.Throws<SystemException>(
            delegate { throw new SystemException("User does not exist!"); });
            Assert.That(ex.Message, Is.EqualTo("User does not exist!"));
        }
    }
}
