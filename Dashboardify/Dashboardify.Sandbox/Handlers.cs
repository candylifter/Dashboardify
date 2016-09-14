using System;
using Dashboardify.Contracts.Dashboards;
using Dashboardify.Contracts.Items;
using Dashboardify.Contracts.Users;
using Dashboardify.Contracts.UserSession;
using Dashboardify.Handlers.Dashboards;
using Dashboardify.Handlers.Items;
using Dashboardify.Handlers.Users;
using Dashboardify.Handlers.UserSession;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.Sandbox
{
    public class Handlers
    {
        private string _connectionString = "Data Source=DESKTOP-11VK3U9;Initial Catalog = DashBoardify; User id = DashboardifyUser; Password=123456;";
        public void Do()
        {
            //TestItemUpdateHandler();
            //TestUserUpdateHandler();
            //TestItemCreation();
            //TestDeleteItemHandler();
            //TestUpdateDash();
            TestCreateUser();   //leidzia dubliuoti
            TestLoginUser();
           
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

        private void TestItemCreation()
        {
            var handler = new CreateItemHandler(_connectionString);
            var request = new CreateItemRequest();

            request.Item = new Item()
            {
                DashBoardId = 1,
                CheckInterval = 10001,
                XPath = "fdsgdfgfd"
            };

            var response = handler.Handle(request);
            foreach (var msg in response.Errors)
            {
                Console.WriteLine(msg.Code);
            }

        }

        private void TestDeleteItemHandler()
        {
            var handler = new DeleteItemHandler(_connectionString);
            var request = new DeleteItemRequest();
            request.Item = new Item()
            {
                Id = -5
            };
            var response = handler.Handle(request);

            foreach (var msg in response.Errors)
            {
                Console.WriteLine(msg.Code);
            }

        }

        private void TestUpdateDash()
        {
            var handler = new UpdateDashBoardHandler(_connectionString);

            var request = new UpdateDashboardRequest();
            request.DashBoard = new DashBoard()
            {
                Id = 1,
                Name = "naujas dahsas"
            };
            var resposne = handler.Handle(request);
            foreach (var msg in resposne.Errors)
            {
                Console.WriteLine(msg.Code);
            }

        }

        private void TestLoginUser()
        {
            var handler = new LoginUserHandler(_connectionString);

            var request= new LoginUserRequest();

            request.user = new User()
            {
                Name = "zilas",
                Password = "maestro"
            };

            var response = handler.Handle(request);
            if (response.HasErrors)
            {
                foreach (var msg in response.Errors)
                {
                    Console.WriteLine(msg.Code);
                }
            }
            else
            {
                Console.WriteLine("Mission complete");
            }

        }

        private void TestCreateUser()
        {
            var createUserHandler = new CreateUserHandler(_connectionString);

            var request = new CreateUserRequest();

            request.Password = "maestro";
            request.Email = "zygimantas.zilevicius@gmail.com";
            request.Username = "zilas";

            var response = createUserHandler.Handle(request);

            if (response.HasErrors)
            {
                Console.WriteLine("Errrrr");
            }
            else
            {
                Console.WriteLine("User created");
            }
        }
    }
}
