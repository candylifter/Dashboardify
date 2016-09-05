using Dashboardify.Repositories;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dashboardify.Service
{
    public class ContentHandler
    {
        public string GetContentByXPath(string url, string xpath)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();

            try
            {
                doc = htmlWeb.Load(url);
                var node = doc.DocumentNode.SelectSingleNode(xpath);
                var content = node.InnerText;

                return Regex.Replace(content, @"\s", " ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public string GetScreenshot(Item item)
        {
            // generate uuid for filename without dashes
            // create phantomjs instance
            // adjust viewport size
            // open webpage
                // return null if not found or timeout
            // try to find element
                // return null if not found
                // OR
                // try to find it by CSS
                    // return null
            // clip viewport by element dimensions
            // set background color to white (element's background could be transparent)
            // save screenshot as jpg to Dashboardify.Screenshots (adjust quality if necessary)
            // exit phantomjs
            // return filename
            return "";
        }

    }
}
