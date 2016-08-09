using System;
using System.Text.RegularExpressions;
using System.Timers;
using Dashboardify.Repositories;
using HtmlAgilityPack;

namespace Dashboardify.Service
{
    public class Service
    {
        private readonly Timer _timer;
        private readonly ItemsRepository _itemsRepository;

        public Service()
        {
            _timer = new Timer(5000) {AutoReset = true};
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

            PrintAllItems();

            _timer.Stop();
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