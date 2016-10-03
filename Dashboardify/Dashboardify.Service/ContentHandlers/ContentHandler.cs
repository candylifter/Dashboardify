using Dashboardify.Repositories;
using HtmlAgilityPack;
using log4net;
using Newtonsoft.Json;
using NReco.PhantomJS;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dashboardify.Service
{
    public class ContentHandler
    {
        private ILog logger = LogManager.GetLogger("Dashboardify.Service");

        public HtmlDocument GetHtmlDocument(string url)
        {
            var htmlWeb = new HtmlWeb();

            try
            {
                var doc = htmlWeb.Load(url);
                return doc;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }

        public string GetContentByXPath(HtmlDocument doc, string xpath)
        {
            var node = doc.DocumentNode.SelectSingleNode(xpath);

            if (node == null)
            {
                const string ex = "Cannot find element by XPath";
                logger.Error(ex);
                throw new Exception(ex);
            }

            var content = node.InnerText;

            return content;
        }

        public string GetContentByCSS(HtmlDocument doc, string css)
        {
            var node = doc.QuerySelector(css);

            if (node == null)
            {
                const string ex = "Cannot find element by CSS";
                logger.Error(ex);
                throw new Exception(ex);
            }

            var content = node.InnerText;

            return content;

        }

        public string GetContentByXPath(string url, string xpath)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();

            try
            {
                doc = htmlWeb.Load(url);
                var node = doc.DocumentNode.SelectSingleNode(xpath);
              
                // TODO: if node == null try finding element by CSS selector.
                if (node == null)
                {
                    throw new Exception("Cannot find element by XPath in website: " + url);
                }

                var content = node.InnerText;


                return content;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                return null;
            }
        }

        public string GetContentByCSS(string url, string css)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();

            try
            {
                doc = htmlWeb.Load(url);
                var node = doc.QuerySelector(css);

                if (node == null)
                {
                    throw new Exception("Cannot find element by CSS in website: " + url);
                }

                var content = node.InnerText;

                return content;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);

                return null;
            }
        }

        public async Task<string> GetScreenshot(Item item)
        {
            var json = JsonConvert.SerializeObject(new { Website = item.Website, XPath = item.XPath, CSS = item.CSS });

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["phantom-api-url"]);
                logger.Info("Sending request to Phantom Node API...");

                var response = await client.PostAsync("", new StringContent(json, Encoding.UTF8, "application/json"));
                var contents = await response.Content.ReadAsStringAsync();
                logger.Info("Got response from Phantom Node API");

                dynamic responseJson = JsonConvert.DeserializeObject(contents);

                if (responseJson.success == true)
                {
                    return responseJson.filename;
                } else
                {
                    return null;
                }

            }

        }
    }
}
