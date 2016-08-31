using Dashboardify.Contracts.Items;
using Dashboardify.Contracts.Users;
using Dashboardify.Handlers.Items;
using Dashboardify.Handlers.Users;
using Dashboardify.Models;

namespace Dashboardify.Sandbox
{
    public class Handlers
    {
        public void Do()
        {
            TestGetItemsListHandler();
        }

        private void TestUserUpdateHandler()
        {
            string connectionString = "Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;";

            var updateUserHandler = new UpdateUserHandler(connectionString);

            var request = new UpdateUserRequest();

            request.User = new User()
            {
                Id = 1,
                Name = "Laba diena",
                Email = "neone@one.lt",
                Password = "Testas123"
            };

            var response = updateUserHandler.Handle(request);
        }

        private void TestGetItemsListHandler()
        {
            string connectionString = "Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;";

            var getItemsListHandler = new GetItemsListHandler(connectionString);

            var request = new GetItemsListRequest();

            request.DashboarId = 1;

            var response = getItemsListHandler.Handle(request);
        }
    }
}
