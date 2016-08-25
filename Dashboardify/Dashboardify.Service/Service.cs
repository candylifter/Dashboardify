using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Timers;
using Dashboardify.Repositories;
using HtmlAgilityPack;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Drawing.Imaging;
using System.Drawing;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Linq;

namespace Dashboardify.Service
{
    public class Service
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBZygis"].ConnectionString;
        private readonly Timer _timer;
        private readonly ItemsRepository _itemsRepository;

        private readonly int _timerInterval = 60000; //miliseconds

        public Service()
        {
            Console.WriteLine(connectionString);
            _timer = new Timer(_timerInterval) { AutoReset = true };
            _timer.Elapsed += TimeElapsedEventHandler;

            _itemsRepository = new ItemsRepository(connectionString);

            UpdateItems();
        }


        public void TimeElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("\n->  Testing\n");

            //var items = _itemsRepository.GetList();

            //var firstItem = items[0];

            //firstitem.XPath = RemakeXpath(firstitem.XPath);

            //var content = GetContentFromWebsite(firstitem.Website, firstitem.XPath);

            //Console.WriteLine(Regex.Replace(content, @"\s+", " "));


            //PrintAllItems();
            //TakeScreenshots();
            UpdateItems();
            //_timer.Stop();
        }

        public void UpdateItems()
        {

            DateTime now = DateTime.Now;
            var items = _itemsRepository.GetList();
            var scheduledItems = FilterScheduledItems(items, now);

            //Testing filtering
            Console.WriteLine("\n\nItems from db:\n");

            foreach (var item in items)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("\n\nScheduled items:\n");

            foreach (var item in scheduledItems)
            {
                Console.WriteLine(item.Name);
            }


            var outdatedItems = FilterOutdatedItems(scheduledItems, now);

            Console.WriteLine("\n\nOutdated items:\n");


            foreach (var item in outdatedItems)
            {
                Console.WriteLine(item.Name);
            }

            TakeScreenshots(outdatedItems);

        }

        public IList<Item> FilterScheduledItems(IList<Item> items, DateTime now)
        {
            var filteredItems = items.Where(item => (
                    item.LastChecked.AddMilliseconds(item.CheckInterval)) <= now &&
                    item.isActive == true
                ).ToList();

            return filteredItems;
        }

        public IList<Item> FilterOutdatedItems(IList<Item> items, DateTime now)
        {

            IList<Item> outdatedItems = new List<Item>();

            foreach (var item in items)
            {
                if (CheckIfOutdated(item, now))
                {
                    outdatedItems.Add(item);
                }
            }

            return outdatedItems;
        }

        public bool CheckIfOutdated(Item item, DateTime now)
        {

            var newContent = GetContentFromWebsite(item.Website, item.XPath);

            if (newContent != item.Content)
            {

                Console.WriteLine("Content from DB: " + item.Content);
                item.Content = newContent;
                Console.WriteLine("Content from Web: " + item.Content);

                item.LastChecked = now;
                _itemsRepository.Update(item);
                return true;
            }
            else
            {
                return false;
            }
        }


        // TODO: Refactor to separate methods
        public void TakeScreenshots(IList<Item> items)
        {
            Console.WriteLine("Creating webdriver instance");
            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            foreach (var item in items)
            {
                Console.WriteLine("Navigating to: " + item.Website);

                driver.Navigate().GoToUrl(item.Website);

                try
                {
                    RemoteWebElement el = (RemoteWebElement)driver.FindElement(By.XPath(item.XPath));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    break;
                }


                Console.WriteLine("Scrolling to the element");

                driver.ExecuteScript(@"
                                    (function () {

                                     var rect = document.evaluate(
                                      '" + item.XPath + @"',
                                      document,
                                      null,
                                      XPathResult.FIRST_ORDERED_NODE_TYPE,
                                      null
                                     ).singleNodeValue.getBoundingClientRect();
                                     window.scrollTo(0, rect.top - ((document.documentElement.clientHeight / 2) - (rect.height / 2)));

                                    })();");


                IWebElement img = driver.FindElement(By.XPath(item.XPath));

                int elementWidth = img.Size.Width;
                int elementHeight = img.Size.Height;


                //Using built in selenium methods fails getting exact coordactes for some reason, so I used javascript instead to get X and Y
                string jsQuery = @"return document.evaluate(
                                '" + item.XPath + @"',
                                document,
                                null,
                                XPathResult.FIRST_ORDERED_NODE_TYPE,
                                null
                                ).singleNodeValue.getBoundingClientRect()";

                int elementTop = Convert.ToInt32(((IJavaScriptExecutor)driver).ExecuteScript(jsQuery + ".top;"));


                int elementLeft = Convert.ToInt32(((IJavaScriptExecutor)driver).ExecuteScript(jsQuery + ".left;"));



                Console.WriteLine("Taking screenshot");

                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();

                //Save screenshot
                string screenshot = ss.AsBase64EncodedString;
                byte[] screenshotAsByteArray = ss.AsByteArray;
                ss.SaveAsFile(item.Name + ".png", ImageFormat.Png);
                ss.ToString();


                //Crop screenshot
                Rectangle cropRect = new Rectangle(elementLeft, elementTop, elementWidth, elementHeight);

                Bitmap src = Image.FromFile(item.Name + ".png") as Bitmap;
                Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                     cropRect,
                                     GraphicsUnit.Pixel);
                }

                target.Save(item.Name + "-cropped.jpeg", ImageFormat.Jpeg);
            }

            driver.Close();



        }


        // TODO: Handle all exceptions
        public string GetContentFromWebsite(string url, string xpath)
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();


            try
            {
                doc = hw.Load(url);
                var node = doc.DocumentNode.SelectSingleNode(xpath);
                var content = node.InnerText;


                return Regex.Replace(content, @"\s+", " ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return "Nope";
        }

        public void PrintAllItems()
        {
            var items = _itemsRepository.GetList();

            foreach (var item in items)
            {
                string content = GetContentFromWebsite(item.Website, RemakeXpath(item.XPath));

                Console.WriteLine("\n" + item.Website + "\n");

                if (content != null)
                {
                    Console.WriteLine(Regex.Replace(content, @"\s+", " "));
                }
                else
                {
                    Console.WriteLine("Content is null");
                }

                //TakeScreenshot(item.Website, item.Name, item.XPath);

                Console.WriteLine("\n---\n");
            }


        }


        /*
        public void GetItemContent(string url, string xpath)
        {
            xpath = RemakeXpath(xpath);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            HtmlWeb hw = new HtmlWeb();
            doc = hw.Load(url);

            HtmlNode node = doc.DocumentNode.SelectSingleNode(xpath);
            string content = node.InnerText;
            Console.WriteLine(content);


            _timer.Stop();
            //return null;
        }
        */

        /// <summary>
        ///     Method that makes XPath compatible with HTMLAgilityPack
        /// </summary>
        /// <param name="xpath">Xpath</param>
        /// <returns>Xpath added with extra /</returns>
        private string RemakeXpath(string xpath)
        {
            var goodXpath = "";
            for (var i = 0; i < xpath.Length; i++)
            {
                if (xpath[i].ToString() == "/")
                {
                    goodXpath = goodXpath + "/";
                }
                goodXpath = goodXpath + xpath[i];
            }
            return goodXpath;
        }


        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}