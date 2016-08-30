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

            // Works fine GetDash(connectionString);

            // Works fine UpdateDash(connectionString);
            
            // Works fine CreateDash(connectionString);

            // Works fine GetDashByUserId(connectionString);

            // Works fine PrintAllScreens(connectionString);

            // Works fine GetScreen(connectionString);

            // Works fine CreateScreenShoot(connectionString);

            // Works fine DeleteScren(connectionString);

            PrinScreen(connectionString);


            //TODO Wait Zilvinas response
            // Needs work DeleteUser(connectionString);
            //All delete methods needs wurk wurk wurk wurk
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
            foreach (var item in itemRepo.GetByDashId(1))
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

        static void UpdateDash(string connectionString)
        {
            var dashRepo = new DashRepository(connectionString);
            var origin = dashRepo.Get(1).ToString();
            var update = dashRepo.Get(1);
            update.Name = "Maestro Trumpi";
            Console.WriteLine(origin);
            Console.WriteLine(update);
            dashRepo.Update(update);
            Console.WriteLine(dashRepo.Get(1).ToString());



        }

        static void CreateDash(string connectionString)
        {
            DateTime rngMin = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;

            DateTime rngMax = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
            
            //Console.WriteLine(rngMin);
            //Console.WriteLine(rngMax);
            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"));

            var dashRepo = new DashRepository(connectionString);

            Console.WriteLine("Before");

            GetDashList(connectionString);

            var dash = new DashBoard();

            dash.Name = "Klarko Skrybeles";

            dash.DateCreated = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"));
            dash.DateModified = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"));

            dash.IsActive = true;

            dash.UserId = 1;

            Console.WriteLine("Item to create" + dash.ToString());

            dashRepo.Create(dash);

            Console.WriteLine("Dash created");

            GetDashList(connectionString);



        }

        static void GetDashByUserId(string connctionString)
        {
            var dash =new DashRepository(connctionString);
            foreach (var ds in dash.GetByUserId(1))
            {
                Console.WriteLine(ds.ToString());
            }
        }

        static void PrintAllScreens(string connectionString)
        {
            var screenRepo = new ScreenshotRepository(connectionString);
            foreach (var screen in screenRepo.GetAll())
            {
                Console.WriteLine(screen.ToString());
            }
        }

        static void GetScreen(string connectionString)
        {
            var screenRepo = new ScreenshotRepository(connectionString);
            Console.WriteLine(screenRepo.Get(0));
        }

        static void CreateScreenShoot(string connectionString)
        {
            var screenRepo = new ScreenshotRepository(connectionString);
            PrintAllScreens(connectionString);
            Console.WriteLine("before");
            var screen = screenRepo.Get(2);
            screen.ScrnshtURL = "www.maestro.com";
            screenRepo.Create(screen);
            PrintAllScreens(connectionString);
        }

        static void DeleteScren(string conn)
        {
            var screen = new ScreenshotRepository(conn);
            PrintAllScreens(conn);
            Console.WriteLine("fdgfdgfdg");
            screen.Delete(4);
            PrintAllScreens(conn);
        }

        static void PrinScreen(string connectionString)
        {
            var screeRepo = new ScreenshotRepository(connectionString);
            Console.WriteLine(screeRepo.Get(1).ToString());
        }
    }

}
