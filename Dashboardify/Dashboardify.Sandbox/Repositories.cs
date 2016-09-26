//using System;
//using Dashboardify.Models;
//using Dashboardify.Repositories;

//namespace Dashboardify.Sandbox
//{
//    public class Repositories
//    {
//        public void Do()
//        {
//            string connectionString = "Data Source=23.251.133.254;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=xc6AjzBx6QA2pKUU";


//            // PrintUsers(connectionString);

//            //  CreateUser(connectionString);

//            //PrintItems(connectionString);

//            //UpdateItem(connectionString);

//            //GetItem(connectionString);

//            //GetByDashId(connectionString);

//            //CreateItem(connectionString);

//            //UpdateUser(connectionString);

//            //GetDashList(connectionString);

//            //GetDash(connectionString);

//            //UpdateDash(connectionString);

//            //CreateDash(connectionString);

//            //GetDashByUserId(connectionString);

//            //PrintAllScreens(connectionString);

//            //GetScreen(connectionString);

//            //CreateScreenShoot(connectionString);

//            //DeleteScren(connectionString);

//            //PrinScreen(connectionString);

//            //DeleteUser(connectionString);

//            //DeleteUser(connectionString);

//            //DeleteDash(connectionString);

//            //DeleteItem(connectionString);

//            // TestAddSession(connectionString);

//            //TestReturnIfExsists(connectionString);

//            //TestReturnIfValidSession(connectionString);
            
//            //TestGetBySessionId(connectionString);

//            //TestDeleteSession(connectionString);

//            TestGetEmail(connectionString);

            
//            Console.ReadKey();
//        }

//        public void PrintUsers(string connectionString)
//        {
//            var UserRepository = new UsersRepository(connectionString);

//            var kzk = UserRepository.GetList();

//            foreach (var user in kzk)
//            {
//                Console.WriteLine(user.Id.ToString() + " " + user.Email + " " + user.IsActive.ToString() + " " + user.Name + " " + user.Password + " " + user.DateModified + " " + user.DateRegistered);
//            }
//        }

//        public void CreateUser(string connectionString)
//        {


//            DateTime myDate = DateTime.Now;
//            string sqlFormat = myDate.ToString("yyyy-MM-dd HH:mm:ss.fff");


//            var user = new User();
//            user.Name = "Maestro";
//            user.Password = "Slaptazodis";
//            user.DateRegistered = DateTime.Now;
//            user.DateModified = DateTime.Now;
//            user.Email = "trumpas@maestro.lt";

//            //Console.WriteLine(user.DateRegistered.ToString());

//            var userrepo = new UsersRepository(connectionString);

//            userrepo.CreateUser(user);

//        }

//        public void DeleteUser(string connectionString)
//        {

//            PrintUsers(connectionString);

//            var repo = new UsersRepository(connectionString);
//            var user = repo.Get(1);
//            Console.WriteLine(user.Id);
//            repo.DeleteUser(1);

//            Console.WriteLine("po");
//            PrintUsers(connectionString);
//        }

//        public void PrintItems(string connectionString)
//        {
//            var itemsRepo = new ItemsRepository(connectionString);
//            foreach (var item in itemsRepo.GetList())
//            {
//                Console.WriteLine(item.Id);
//                Console.WriteLine(item.DashBoardId);
//                Console.WriteLine(item.Name);
//                Console.WriteLine(item.Website);
//                Console.WriteLine(item.CheckInterval);
//                Console.WriteLine(item.IsActive);
//                Console.WriteLine(item.XPath);
//                Console.WriteLine(item.LastChecked);
//                Console.WriteLine(item.Created);
//                Console.WriteLine(item.Modified);
//                Console.WriteLine(item.Content);
//                Console.WriteLine("");


//            }
//        }

//        public void UpdateItem(string connectionString)
//        {
//            var itemRepo = new ItemsRepository(connectionString);
//            var itemOrigin = itemRepo.Get(1);
//            var itemNew = new Item();
//            itemNew.Id = 1;
//            itemNew.DashBoardId = 1;
//            itemNew.Name = "Maestro";
//            itemNew.Website = "www.orlauskas.juokingai.juodas.betabu.dziazmenas.lt";
//            itemNew.CheckInterval = 5;
//            itemNew.IsActive = true;
//            itemNew.XPath = "body[0]/content[1]";
//            itemNew.LastChecked = DateTime.Now.AddMinutes(-itemNew.CheckInterval);
//            itemNew.Modified = DateTime.Now;
//            itemNew.Content = "Trumpas";
//            itemNew.CSS = "default";

//            Console.WriteLine(itemNew.ToString());
//            Console.WriteLine(itemOrigin.ToString());

//            itemRepo.Update(itemNew);
//            Console.WriteLine("Updated");
//            Console.WriteLine(itemRepo.Get(1).ToString());


//            //itemRepo.Update();



//        }

//        public void GetItem(string connectionString)
//        {
//            var itemRepo = new ItemsRepository(connectionString);
//            Console.Write(itemRepo.Get(2).ToString());
//        }

//        public void GetByDashId(string connectionString)
//        {
//            var itemRepo = new ItemsRepository(connectionString);
//            foreach (var item in itemRepo.GetByDashboardId(1))
//            {
//                Console.WriteLine(item.ToString());
//            }
//        }

//        public void CreateItem(string connectionString)
//        {
//            PrintItems(connectionString);

//            Console.WriteLine("Pries");

//            var itemRepo = new ItemsRepository(connectionString);
//            var itemtoWrite = itemRepo.Get(2);
//            itemtoWrite.Id = 0;
//            itemRepo.Create(itemtoWrite);

//            Console.WriteLine("po");

//            PrintItems(connectionString);



//        }

//        public void UpdateUser(string connectionString)
//        {
//            var userRepo = new UsersRepository(connectionString);
//            var UserOrigin = userRepo.Get(1);
//            var UserUpdate = userRepo.Get(1);
//            UserUpdate.Email = "Maestro@Irmantas.com";
//            UserUpdate.Id = 1;
            
//            UserUpdate.Name = "UniteTest";
//            Console.WriteLine(UserOrigin.ToString());
//            Console.WriteLine(UserUpdate.ToString());

//            Console.WriteLine("Updating");
//            userRepo.Update(UserUpdate);
//            Console.Write("Done, Result = " + userRepo.Get(1).ToString());

//        }

//        public void GetDashList(string connectionString)
//        {
//            var dashRepo = new DashRepository(connectionString);
//            foreach (var dash in dashRepo.GetList())
//            {
//                Console.WriteLine(dash.ToString());
//            }
//        }

//        public void GetDash(string connectionString)
//        {
//            var DashRepo = new DashRepository(connectionString);
//            Console.Write(DashRepo.Get(1));

//        }

//        public void UpdateDash(string connectionString)
//        {
//            var dashRepo = new DashRepository(connectionString);
//            var origin = dashRepo.Get(1).ToString();
//            var update = dashRepo.Get(1);
//            if (update == null) throw new ArgumentNullException(nameof(update));
//            update.Name = "Maestro Trumpi";
//            Console.WriteLine(origin);
//            Console.WriteLine(update);
//            dashRepo.Update(update);
//            Console.WriteLine(dashRepo.Get(1).ToString());



//        }

//        public void CreateDash(string connectionString)
//        {
//            DateTime rngMin = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;

//            DateTime rngMax = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;

//            //Console.WriteLine(rngMin);
//            //Console.WriteLine(rngMax);
//            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"));

//            var dashRepo = new DashRepository(connectionString);

//            Console.WriteLine("Before");

//            GetDashList(connectionString);

//            var dash = new DashBoard();

//            dash.Name = "Klarko Skrybeles";

//            dash.DateCreated = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"));
//            dash.DateModified = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss"));

//            dash.IsActive = true;

//            dash.UserId = 1;

//            Console.WriteLine("Item to create" + dash.ToString());

//            dashRepo.Create(dash);

//            Console.WriteLine("Dash created");

//            GetDashList(connectionString);



//        }

//        public void GetDashByUserId(string connctionString)
//        {
//            var dash = new DashRepository(connctionString);
//            foreach (var ds in dash.GetByUserId(1))
//            {
//                Console.WriteLine(ds.ToString());
//            }
//        }

//        public void PrintAllScreens(string connectionString)
//        {
//            var screenRepo = new ScreenshotRepository(connectionString);
//            foreach (var screen in screenRepo.GetAll())
//            {
//                Console.WriteLine(screen.ToString());
//            }
//        }

//        public void GetScreen(string connectionString)
//        {
//            var screenRepo = new ScreenshotRepository(connectionString);
//            Console.WriteLine(screenRepo.Get(0));
//        }

//        public void CreateScreenShoot(string connectionString)
//        {
//            var screenRepo = new ScreenshotRepository(connectionString);
//            PrintAllScreens(connectionString);
//            Console.WriteLine("before");
//            var screen = screenRepo.Get(2);
//            screen.ScrnshtURL = "www.maestro.com";
//            screenRepo.Create(screen);
//            PrintAllScreens(connectionString);
//        }

//        public void DeleteScren(string conn)
//        {
//            var screen = new ScreenshotRepository(conn);
//            PrintAllScreens(conn);
//            Console.WriteLine("fdgfdgfdg");
//            screen.Delete(4);
//            PrintAllScreens(conn);
//        }

//        public void PrinScreen(string connectionString)
//        {
//            var screeRepo = new ScreenshotRepository(connectionString);
//            Console.WriteLine(screeRepo.Get(1).ToString());
//        }

//        public void DeleteDash(string connectionString)
//        {
//            var dashRepo = new DashRepository(connectionString);
//            GetDashList(connectionString);
//            dashRepo.DeleteDashboard(2);
//            Console.WriteLine("Po");
//            GetDashList(connectionString);
//        }

//        public void DeleteItem(string connectionString)
//        {
//            PrintItems(connectionString);
//            var itemsRepo = new ItemsRepository(connectionString);
//            itemsRepo.Delete(2);
//            PrintItems(connectionString);
//        }

//        public void TestAddSession(string connectionString)
//        {
//            var userSesRep = new  UserSessionRepository(connectionString);

//            var sesija = new UserSession()
//            {
//                Ticket = "g1dsf56g1df65g1fd56gdfs",
//                UserId = 1,
//                Expires = DateTime.Now
//            };

            
//            if(userSesRep.AddSession(sesija)==true)
//            {
//                Console.WriteLine("Succes");
//            }
//            else
//            {
//                Console.WriteLine("Unsuccesful");
//            }

//        }

//        public void TestReturnIfExsists(string connectionString)
//        {
//            var usersrepo = new UsersRepository(connectionString);
//            var user = new User()
//            {
//                Name = "4524534Laba diena",
//                Password = "asd56a+5d6asd"
//            };
//            if (!(usersrepo.ReturnIfExsists(user.Name, user.Password) == null))
//            {
//                Console.WriteLine("user exsists");
//                Console.WriteLine(usersrepo.ReturnIfExsists(user.Name,user.Password).ToString());
//            }
//            else
//            {
//                Console.WriteLine("User does not exsist");   
                
//            }
//        }

//        public void TestGetBySessionId(string connectionString)
//        {
//            var userSesRepo = new UserSessionRepository(connectionString);

//            Console.WriteLine(userSesRepo.GetUserBySessionId(
//                "a04ef843509f45f68d1c204dec83181f37aca563515646c89f70cbd457b9b0fdadbb6114c1e543248b4392efa504ffd6f27e63b14f5e4d25814c09812989aacc").ToString());

//        }

//        private void TestDeleteSession(string connectionString)
//        {
//            var userSesRepo = new UserSessionRepository(connectionString);

//            userSesRepo.DeleteUserSession("maestro");

//        }

//        private void TestGetEmail(string connectionString)
//        {
//            var userRepo = new UsersRepository(connectionString);

//            var anw = userRepo.ReturnEmail("mail@maestr o.com");

//            if (string.IsNullOrEmpty(anw))
//            {
//                Console.WriteLine("Email is nto taken");
//            }
//            else
//            {
//                Console.WriteLine("Email is taken");
//            }

//            //Console.WriteLine(anw);


//        }


//    }
//}
