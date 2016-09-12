using System;

namespace Dashboardify.Sandbox
{
    class Program
    {
        private static Repositories _repositories;
        private static Handlers _handlers;


        static void Main(string[] args)
        {
           // _repositories = new Repositories();
           // _repositories.Do();

            _handlers = new Handlers();
            _handlers.Do();


            //_repositories.DeleteUser("Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");
            
            Console.ReadKey();
        }
    }

}
