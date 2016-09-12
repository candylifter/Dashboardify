using System;
using System.ComponentModel;
using System.Linq;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Users;
using Dashboardify.Handlers.Users;
using Dashboardify.Models;
using Dashboardify.Repositories;
using Moq;
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
        [Test]
        public void NullUser_UpdateUser()
        {
            
            var request = new UpdateUserRequest();
            request.User = null;

            var handler = new UpdateUserHandler("Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");

            var response = handler.Handle(request);
            
            Assert.True(response.HasErrors);
            Assert.AreEqual("BAD_REQUEST",response.Errors.First().Code);

            //var ex = Assert.Throws<Exception>(() => handler.Handle(request));
            //Assert.That(ex.Message, Is.EqualTo("User does not exist!"));
            //TODO needs to mock, to get null from item repo
        }
        
        [Test]
        public void WrongEmail_UpdateUser()
        {
            var request = new UpdateUserRequest();
            request.User = new User();
            request.User.Id = 1;
            request.User.Email = "maestroIrmantas@one.lt";
            request.User.Name = "UniteTest";
            

            var handler = new UpdateUserHandler("Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");

            var response = handler.Handle(request);
            //

            Assert.AreEqual("EMAIL_WRONG_FORMAT", response.Errors.First().Code);
             

        }

        [Test]
        public void WrongId_UpdateUser()
        {
            var request = new UpdateUserRequest();
            request.User = new User();
            request.User.Id = -5;
            request.User.Email = "dsafsdf";
            request.User.Name = "UniteTest";

            var handler = new UpdateUserHandler("Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");
            var response = handler.Handle(request);
            Assert.True(response.HasErrors);
            Assert.AreEqual("USER_NOT_FOUND",response.Errors.First().Code);
        }

        [Test]
        public void OnlyIdPassed_UpdateUser()
        {
            var request = new UpdateUserRequest();
            request.User = new User();
            request.User.Id = 1;
            request.User.Email = "";
            request.User.Name = "";

            var handler = new UpdateUserHandler("Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");
            var response = handler.Handle(request);

            Assert.False(response.HasErrors);
        }
    }
}
