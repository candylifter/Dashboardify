using System;
using System.Timers;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using Dashboardify.Repositories;

namespace Dashboardify.Service
{
    public class Service
    {
        private ItemsRepository _itemsRepository;

        private readonly Timer _timer;
        public Service()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += TimeElapsedEventHandler;

            _itemsRepository = new ItemsRepository();
        }
        
        public void TimeElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Testing");

            var items = _itemsRepository.GetList();

            var firstItem = items[0];

            firstItem.Xpath = RemakeXpath(firstItem.Xpath);

            HtmlDocument doc = new HtmlDocument();

            HtmlWeb hw = new HtmlWeb();
            doc = hw.Load(firstItem.Url);

            HtmlNode node = doc.DocumentNode.SelectSingleNode(firstItem.Xpath);
            string content = node.InnerText;
            Console.WriteLine(Regex.Replace(content,@"\s+"," "));


            _timer.Stop();


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
        /// Method that makes XPath compatible with HTMLAgilityPack
        /// </summary>
        /// <param name="xpath">Xpath</param>
        /// <returns>Xpath added with extra /</returns>
        private string RemakeXpath(string xpath)
        {
            string goodXpath = "";
            for (int i=0; i<xpath.Length;i++)
            {
                if (xpath[i].ToString()=="/")
                {
                    goodXpath = goodXpath + "/";
                }
                goodXpath = goodXpath + xpath[i];
            }
            return goodXpath;
        }
   



        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }



    }


}
