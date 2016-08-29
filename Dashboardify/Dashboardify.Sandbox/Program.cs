using System;
using Dashboardify.Models;
using Dashboardify.Repositories;
using System.Data;
using System.Runtime.InteropServices;

namespace Dashboardify.Sandbox
{
    class Program
    {
        
        static void Main(string[] args)
        {
            

            string connectionString = "Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;";

            //PrintUsers(connectionString);

            //CreateUser(connectionString);

            DeleteUser(connectionString);
            



            Console.ReadKey();
        }
        public static void PrintUsers(string connectionString)
        {
            var UserRepository = new UsersRepository(connectionString);

            var kzk = UserRepository.GetList();

            foreach (var user in kzk)
            {
                Console.WriteLine(user.Id.ToString() + " " + user.Email + " " + user.IsActive.ToString() + " " + user.Name + " " + user.Password + " " + user.DateModified + " " + user.DateRegistered);
            }
        }

        static void CreateUser(string connectionString)
        {
           

            DateTime myDate = DateTime.Now;
            string sqlFormat = myDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
           

            var user = new User();
            user.Name = "Maestro";
            user.Password = "Slaptazodis";
            user.DateRegistered = DateTime.Parse(sqlFormat);
            user.Email = "trumpas@maestro.lt";

            //Console.WriteLine(user.DateRegistered.ToString());
            
            var userrepo = new UsersRepository(connectionString);

            userrepo.CreateUser(user);

        }

        static void DeleteUser(string connectionString)
        {
            var repo = new UsersRepository(connectionString);
            repo.DeleteUser(1);
        }
    }

}
