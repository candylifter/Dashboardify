using Dashboardify.Repositories;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NReco.PhantomJS;
using System;
using System.Net.Http;
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> GetScreenshotAsync(Item item)
        {
            var json = JsonConvert.SerializeObject(new { Website = item.Website, XPath = item.XPath, CSS = item.CSS });

            using (var client = new HttpClient())
            {


                client.BaseAddress = new Uri("http://localhost:3000");
                Console.WriteLine("   Sending request to Phantom Node API...");
                var response = await client.PostAsync("", new StringContent(json, Encoding.UTF8, "application/json"));
                var contents = await response.Content.ReadAsStringAsync();
                Console.WriteLine("   Got response from Phantom Node API");

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

        public string GetScreenshot(Item item)
        {
            // generate guid for filename without dashes
            Guid guid = Guid.NewGuid();
            string filename = Regex.Replace(guid.ToString(), @"-", "");

            // create phantomjs instance
            // adjust viewport size
            // open webpage
            // return null if not found or timeout
            // try to find element
            // return null if not found
                // OR
                // try to find it by CSS
                // if not found by CSS, return null
            // clip viewport by element dimensions
            // set background color to white if element's background is transparent
            // save screenshot as jpg to Dashboardify.Screenshots (adjust quality if necessary)
            // exit phantomjs
            var phantomJS = new PhantomJS();

            phantomJS.OutputReceived += (sender, e) => {
                Console.WriteLine("    PhantomJS output: {0}", e.Data);
            };
            phantomJS.RunScript(@"var page = require('webpage').create();

                page.viewportSize = {
                    width: 1920,
                    height: 1080
                };

                page.open('" + item.Website + @"', function() {
                    var clipRect = page.evaluate(function() {

                        var element;

                        try {
                            element = document
                                            .evaluate( 
                                                '" + item.XPath + @"' ,
                                                document, 
                                                null, 
                                                XPathResult.FIRST_ORDERED_NODE_TYPE,
                                                null
                                            )
                                            .singleNodeValue; " + /* Often throws exception here*/ @"

                        } catch(err) {
                            console.error(err.message);
                            element = document.body;
                        }
                        
                        document.body.bgColor = 'white';
                                                                  

                        return element.getBoundingClientRect();
                    });

                    page.clipRect = {
                        top: clipRect.top,
                        left: clipRect.left,
                        width: clipRect.width,
                        height: clipRect.height
                    };
                

                    page.render('../../../Dashboardify.Screenshots/" + filename + @".jpg');
                    phantom.exit();
                });", null);


            
            // return filename
            return filename + ".jpg";
        }

    }
}
