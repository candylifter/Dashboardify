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
        private string _connectionString = "Data Source=23.251.133.254;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=xc6AjzBx6QA2pKUU";

        public void Do()
        {
            //TestItemUpdateHandler();
            //TestUserUpdateHandler();
            //TestItemCreation();
            //TestDeleteItemHandler();
            //TestUpdateDash();
            TestCreateUser();   //leidzia dubliuoti
            //TestLoginUser();

            //TestGetDashById();

            //TestDeleteSession();

            //TestCreateUserHandler();
            

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
                XPath = "fdsgdfgfd",
                CSS = "blank",
                Name = "mazutis",
                Website = "www.google.com"
            };

            request.Ticket =
                "96d0bcc478e94faa8bbe62bf7af55f4c076489f5b98d44e79ad0c26bcabd9e142c84bf2f2c5b47b09e85e599c7ad11ded94391c070c34d6f8a4aa3f90976fdf2";

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

            request.User = new User()
            {
                Email = "mail@maestro.com",
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

            request.Password = "";
            request.Email = "";
            request.Username = "";

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

        private void TestGetDashById()
        {
            var getDashHandler = new GetDashboardsHandler(_connectionString);
            
            var request = new GetDashboardsRequest();

            request.UserId = 7;
            request.Ticket =
                "a04ef843509f45f68d1c204dec83181f37aca563515646c89f70cbd457b9b0fdadbb6114c1e543248b4392efa504ffd6f27e63b14f5e4d25814c09812989aacc";


            var response = getDashHandler.Handle(request);
            if (response.HasErrors)
            {
                foreach (var msg in response.Errors)
                {
                    Console.WriteLine(msg.Code);
                }
            }
            else
            {
                Console.WriteLine("Session is valid");
            }
        }

        private void TestDeleteSession()
        {
            var handler = new LogoutUserHandler(_connectionString);

            var response = new LogoutUserResponse();

            var request = new LogoutUserRequest();

            request.Ticket = "maestro";

            response = handler.Handle(request);

            if (response.HasErrors)
            {
                foreach (var msg in response.Errors)
                {
                    Console.WriteLine(msg);
                }
            }
            else
            {
                Console.Write("succesfully loged out :))))))");
            }
        }

        private void TestCreateUserHandler()
        {
            var request = new CreateUserRequest();
            request.Email = "test@maestro.lt";
            request.Username ="maestro";
            request.Password = "maestro";

            var handler = new CreateUserHandler(_connectionString);

            var response = handler.Handle(request);

            if (response.HasErrors)
            {
                foreach (var error in response.Errors)
                {
                    Console.WriteLine(error.Code);
                }
            }
            else
            {
                Console.Write("succes");
            }
        }

        
    }
}
