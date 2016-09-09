using System;
using Dashboardify.Contracts.Items;
using Dashboardify.Contracts.Users;
using Dashboardify.Handlers.Items;
using Dashboardify.Handlers.Users;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.Sandbox
{
    public class Handlers
    {
        private string _connectionString = "Data Source=DESKTOP-11VK3U9;Initial Catalog = DashBoardify; User id = DashboardifyUser; Password=123456;";
        public void Do()
        {
           TestItemUpdateHandler();
           //TestUserUpdateHandler();
        }

        private void TestUserUpdateHandler()
        {
            string connectionString = "Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;";

            var updateUserHandler = new UpdateUserHandler(connectionString);

            var request = new UpdateUserRequest();

            request.User = new User()
            {
                //Id = 1,
                //Name = "Laba diena",
                //Email = "neone@one.lt",
                //Password = "Testas123"

                Id = -5,
                Email = "",
                Name = "UniteTest"

        };

            var response = updateUserHandler.Handle(request);
            foreach (var message in response.Errors)
            {
                Console.WriteLine(message.Code);
            }
        }

        private void TestGetItemsListHandler()
        {
            string connectionString = "Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;";

            var getItemsListHandler = new GetItemsListHandler(connectionString);

            var request = new GetItemsListRequest();

            request.DashboarId = 1;

            var response = getItemsListHandler.Handle(request);
        }

        private void TestDeleteUserHandler()
        {
            var getDeleteUserHandler = new DeleteUserHandler(_connectionString);
            var request = new DeleteUserRequest();
            request.User = new User();
            request.User.Id = 1;

            var response = getDeleteUserHandler.Handle(request);
        }

        private void TestItemUpdateHandler()
        {
            var updateUserHandelr = new UpdateItemHandler(_connectionString);

            var request = new UpdateItemRequest();
            
            request.Item = new Item()
            {
                CheckInterval = 5,
                Content = "Tuscias",
                IsActive = false,
                Id = 1
            };
            var response = updateUserHandelr.Handle(request);

            foreach (var msg in response.Errors)
            {
                Console.WriteLine(msg.Code);
            }

        }
    }
}
