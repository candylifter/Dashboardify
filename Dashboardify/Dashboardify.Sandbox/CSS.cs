using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Dashboardify.Sandbox
{
    public class CSS
    {
        

        public void GetContentByCSS()
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();

            try
            {
                doc = htmlWeb.Load("https://news.ycombinator.com/");

                //var nodeHtml = doc.


                var node = doc.QuerySelector(@"31 2604767 > td:nth-child(3)");

                Console.WriteLine(node.InnerText);




                //var node = doc.QuerySelector(css);

                //if (node == null)
                //{
                //    throw new Exception("Cannot find element by CSS in website: " + url);
                //}

                //var content = node.InnerText;

                //return content;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
