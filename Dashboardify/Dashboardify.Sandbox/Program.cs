using System;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;";

            var usersRepository = new UsersRepository(connectionString);

            var user = new User()
            {
                Id = 10,
                IsActive = false,
                DateModified = DateTime.Now,
                Email = "email@mailfdf.com",
                Name = "Laba diena",
                Password = "labadiena145"
            };
            try
            {
                usersRepository.Update(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
