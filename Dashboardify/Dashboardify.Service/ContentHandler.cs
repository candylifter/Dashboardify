using Dashboardify.Repositories;
using HtmlAgilityPack;
using NReco.PhantomJS;
using System;
using System.Text.RegularExpressions;

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
