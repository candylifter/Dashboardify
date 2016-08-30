using System;
using Dashboardify.Models;
using Dashboardify.Repositories;
using System.Data;
using System.Dynamic;
using System.Runtime.InteropServices;

namespace Dashboardify.Sandbox
{
    class Program
    {
        
        static void Main(string[] args)
        {
            

            string connectionString = "Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;";


            // Works finePrintUsers(connectionString);

            // Works Fine CreateUser(connectionString);

            // Works fine PrintItems(connectionString);

            // Works fine UpdateItem(connectionString);

            // Works fine GetItem(connectionString);

            // Works fine GetByDashId(connectionString);

            // Works fine CreateItem(connectionString);

            // Works fine UpdateUser(connectionString);

            // Works fine GetDashList(connectionString);

            GetDash(connectionString);
            

            //TODO Wait Zilvinas response
            // Needs work DeleteUser(connectionString);

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
            var user = repo.Get(1);
            Console.WriteLine(user.Id);
            repo.DeleteUser(1);
        }

        static void PrintItems(string connectionString)
        {
            var itemsRepo = new ItemsRepository(connectionString);
            foreach (var item in itemsRepo.GetList())
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.DashBoardId);
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Website);
                Console.WriteLine(item.CheckInterval);
                Console.WriteLine(item.isActive);
                Console.WriteLine(item.XPath);
                Console.WriteLine(item.LastChecked);
                Console.WriteLine(item.Created);
                Console.WriteLine(item.Modified);
                Console.WriteLine(item.Content);
                Console.WriteLine("");


            }
        }

        static void UpdateItem(string connectionString)
        {
            var itemRepo = new ItemsRepository(connectionString);
            var itemOrigin = itemRepo.Get(1);
            var itemNew = new Item();
            itemNew.Id = 1;
            itemNew.DashBoardId = 1;
            itemNew.Name = "Maestro";
            itemNew.Website = "www.orlauskas.juokingai.juodas.betabu.dziazmenas.lt";
            itemNew.CheckInterval = 5;
            itemNew.isActive = true;
            itemNew.XPath = "body[0]/content[1]";
            itemNew.LastChecked = DateTime.Now.AddMinutes(-itemNew.CheckInterval);
            itemNew.Modified = DateTime.Now;
            itemNew.Content = "Trumpas";

            Console.WriteLine(itemNew.ToString());
            Console.WriteLine(itemOrigin.ToString());
            
            itemRepo.Update(itemNew);
            Console.WriteLine("Updated");
            Console.WriteLine(itemRepo.Get(1).ToString());
            

            //itemRepo.Update();
            

            
        }

        static void GetItem(string connectionString)
        {
            var itemRepo = new ItemsRepository(connectionString);
            Console.Write(itemRepo.Get(2).ToString());
        }

        static void GetByDashId(string connectionString)
        {
            var itemRepo = new ItemsRepository(connectionString);
            foreach (var item in itemRepo.GetByDashId(2))
            {
                Console.WriteLine(item.ToString());
            }
        }

        static void CreateItem(string connectionString)
        {
            PrintItems(connectionString);

            Console.WriteLine("Pries");

            var itemRepo = new ItemsRepository(connectionString);
            var itemtoWrite = itemRepo.Get(2);
            itemtoWrite.Id = 0;
            itemRepo.Create(itemtoWrite);

            Console.WriteLine("po");

            PrintItems(connectionString);



        }

        static void UpdateUser(string connectionString)
        {
            var userRepo = new UsersRepository(connectionString);
            var UserOrigin = userRepo.Get(1);
            var UserUpdate = userRepo.Get(1);
            UserUpdate.Email = "Maestro@Irmantas.com";
            Console.WriteLine(UserOrigin.ToString());
            Console.WriteLine(UserUpdate.ToString());

            Console.WriteLine("Updating");
            userRepo.Update(UserUpdate);
            Console.Write("Done, Result = " + userRepo.Get(1).ToString());

        }

        static void GetDashList(string connectionString)
        {
            var dashRepo = new DashRepository(connectionString);
            foreach (var dash in dashRepo.GetList())
            {
                Console.WriteLine(dash.ToString());
            }
        }

        static void GetDash(string connectionString)
        {
            var DashRepo = new DashRepository(connectionString);
            Console.Write(DashRepo.Get(1));

        }

        static void Update(string connectionString)
        {
            var dashRepo = new DashRepository(connectionString);


        }
    }

}
