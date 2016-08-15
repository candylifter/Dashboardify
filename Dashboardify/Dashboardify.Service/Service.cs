using System;
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

namespace Dashboardify.Service
{
    public class Service
    {
        private readonly Timer _timer;
        private readonly ItemsRepository _itemsRepository;

        public Service()
        {
            _timer = new Timer(15000) {AutoReset = true};
            _timer.Elapsed += TimeElapsedEventHandler;

            _itemsRepository = new ItemsRepository();
        }

        public void TimeElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("\n->  Testing\n");

            //var items = _itemsRepository.GetList();

            //var firstItem = items[0];

            //firstItem.Xpath = RemakeXpath(firstItem.Xpath);

            //var content = GetContentFromWebsite(firstItem.Url, firstItem.Xpath);

            //Console.WriteLine(Regex.Replace(content, @"\s+", " "));

            _timer.Stop();
            //PrintAllItems();
            //TakeScreenshots();
            
        }

        // TODO: Refactor to separate methods
        public void TakeScreenshots()
        {
            var driver = new FirefoxDriver();

            var items = _itemsRepository.GetList();

            foreach (var item in items)
            {
                driver.Navigate().GoToUrl(item.Url);

                var el = driver.FindElement(By.XPath(item.Xpath));

                RemoteWebElement remElement = (RemoteWebElement)driver.FindElement(By.XPath(item.Xpath));
                Point location = remElement.LocationOnScreenOnceScrolledIntoView;

                int viewportWidth = Convert.ToInt32(((IJavaScriptExecutor)driver).ExecuteScript("return document.documentElement.clientWidth"));
                int viewportHeight = Convert.ToInt32(((IJavaScriptExecutor)driver).ExecuteScript("return document.documentElement.clientHeight"));


                driver.SwitchTo();

                int elementLocation_X = location.X;
                int elementLocation_Y = location.Y;

                IWebElement img = driver.FindElement(By.XPath(item.Xpath));

                int elementSize_Width = img.Size.Width;
                int elementSize_Height = img.Size.Height;
                Console.WriteLine("Taking screenshot");

                Size s = new Size();
                s.Width = driver.Manage().Window.Size.Width;
                s.Height = driver.Manage().Window.Size.Height;

                Bitmap bitmap = new Bitmap(s.Width, s.Height);
                Graphics graphics = Graphics.FromImage(bitmap as Image);
                graphics.CopyFromScreen(0, 0, 0, 0, s);

                bitmap.Save(item.Name + ".png", System.Drawing.Imaging.ImageFormat.Png);

                RectangleF part = new RectangleF(elementLocation_X, elementLocation_Y + (s.Height - viewportHeight), elementSize_Width, elementSize_Height);

                Bitmap bmpobj = (Bitmap)Image.FromFile(item.Name + ".png");
                Bitmap bn = bmpobj.Clone(part, bmpobj.PixelFormat);
                bn.Save(item.Name + "-cropped.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            driver.Close();
        }

        public string GetContentFromWebsite(string url, string xpath)
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();


            try
            {
                doc = hw.Load(url);
                var node = doc.DocumentNode.SelectSingleNode(xpath);
                var content =  node.InnerText ;


                return content;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        public void PrintAllItems()
        {
            var items = _itemsRepository.GetList();

            foreach (var item in items)
            {
                string content = GetContentFromWebsite(item.Url, RemakeXpath(item.Xpath));

                Console.WriteLine("\n" + item.Url + "\n");

                if (content != null) {
                    Console.WriteLine(Regex.Replace(content, @"\s+", " "));
                } else {
                    Console.WriteLine("Content is null");
                }

                //TakeScreenshot(item.Url, item.Name, item.Xpath);

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