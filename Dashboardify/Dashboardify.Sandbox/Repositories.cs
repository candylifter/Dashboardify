﻿using System;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.Sandbox
{
    public class Repositories
    {
        public void Do()
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

            // Works finePrinScreen(connectionString);

            // Works fine DeleteUser(connectionString);

            // Works fine DeleteUser(connectionString);

            // Works fine DeleteDash(connectionString);
            
            // DeleteItem(connectionString);

            
            Console.ReadKey();
        }

        public void PrintUsers(string connectionString)
        {
            var UserRepository = new UsersRepository(connectionString);

            var kzk = UserRepository.GetList();

            foreach (var user in kzk)
            {
                Console.WriteLine(user.Id.ToString() + " " + user.Email + " " + user.IsActive.ToString() + " " + user.Name + " " + user.Password + " " + user.DateModified + " " + user.DateRegistered);
            }
        }

        public void CreateUser(string connectionString)
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

        public void DeleteUser(string connectionString)
        {

            PrintUsers(connectionString);
            
            var repo = new UsersRepository(connectionString);
            var user = repo.Get(1);
            Console.WriteLine(user.Id);
            repo.DeleteUser(1);

            Console.WriteLine("po");
            PrintUsers(connectionString);
        }

        public void PrintItems(string connectionString)
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

        public void UpdateItem(string connectionString)
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

        public void GetItem(string connectionString)
        {
            var itemRepo = new ItemsRepository(connectionString);
            Console.Write(itemRepo.Get(2).ToString());
        }

        public void GetByDashId(string connectionString)
        {
            var itemRepo = new ItemsRepository(connectionString);
            foreach (var item in itemRepo.GetByDashboardId(1))
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void CreateItem(string connectionString)
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

        public void UpdateUser(string connectionString)
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

        public void GetDashList(string connectionString)
        {
            var dashRepo = new DashRepository(connectionString);
            foreach (var dash in dashRepo.GetList())
            {
                Console.WriteLine(dash.ToString());
            }
        }

        public void GetDash(string connectionString)
        {
            var DashRepo = new DashRepository(connectionString);
            Console.Write(DashRepo.Get(1));

        }

        public void UpdateDash(string connectionString)
        {
            var dashRepo = new DashRepository(connectionString);
            var origin = dashRepo.Get(1).ToString();
            var update = dashRepo.Get(1);
            if (update == null) throw new ArgumentNullException(nameof(update));
            update.Name = "Maestro Trumpi";
            Console.WriteLine(origin);
            Console.WriteLine(update);
            dashRepo.Update(update);
            Console.WriteLine(dashRepo.Get(1).ToString());



        }

        public void CreateDash(string connectionString)
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

        public void GetDashByUserId(string connctionString)
        {
            var dash = new DashRepository(connctionString);
            foreach (var ds in dash.GetByUserId(1))
            {
                Console.WriteLine(ds.ToString());
            }
        }

        public void PrintAllScreens(string connectionString)
        {
            var screenRepo = new ScreenshotRepository(connectionString);
            foreach (var screen in screenRepo.GetAll())
            {
                Console.WriteLine(screen.ToString());
            }
        }

        public void GetScreen(string connectionString)
        {
            var screenRepo = new ScreenshotRepository(connectionString);
            Console.WriteLine(screenRepo.Get(0));
        }

        public void CreateScreenShoot(string connectionString)
        {
            var screenRepo = new ScreenshotRepository(connectionString);
            PrintAllScreens(connectionString);
            Console.WriteLine("before");
            var screen = screenRepo.Get(2);
            screen.ScrnshtURL = "www.maestro.com";
            screenRepo.Create(screen);
            PrintAllScreens(connectionString);
        }

        public void DeleteScren(string conn)
        {
            var screen = new ScreenshotRepository(conn);
            PrintAllScreens(conn);
            Console.WriteLine("fdgfdgfdg");
            screen.Delete(4);
            PrintAllScreens(conn);
        }

        public void PrinScreen(string connectionString)
        {
            var screeRepo = new ScreenshotRepository(connectionString);
            Console.WriteLine(screeRepo.Get(1).ToString());
        }

        public void DeleteDash(string connectionString)
        {
            var dashRepo = new DashRepository(connectionString);
            GetDashList(connectionString);
            dashRepo.DeleteDashboard(2);
            Console.WriteLine("Po");
            GetDashList(connectionString);
        }

        public void DeleteItem(string connectionString)
        {
            PrintItems(connectionString);
            var itemsRepo = new ItemsRepository(connectionString);
            itemsRepo.Delete(2);
            PrintItems(connectionString);
        }

        
    }
}
