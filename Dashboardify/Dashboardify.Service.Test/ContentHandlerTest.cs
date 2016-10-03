using System;
using HtmlAgilityPack;
using NUnit.Framework;

namespace Dashboardify.Service.Test
{
    [TestFixture]
    public class ContentHandlerTest
    {
        private ContentHandler _contentHandler =  new ContentHandler();

        [Test]
        public void ShouldGetHtmlDocument()
        {
            var url = "http://google.com";
            var doc = _contentHandler.GetHtmlDocument(url);

            Assert.NotNull(doc);
        }

        [Test]
        public void ShouldThrowExceptionOnHtmlDocumentFail()
        {
            var url = "http://reallyfakedomainthatnobodywoulduse.com";
            var doc = _contentHandler.GetHtmlDocument(url);

            Assert.IsNull(doc);
        }

        [Test]
        public void ShouldGetContentByXPath()
        {
            var doc = new HtmlDocument();
            var node = HtmlNode.CreateNode("<html><head></head><body><div>Test</div></body></html>");

            doc.DocumentNode.AppendChild(node);

            var xpath = "/html/body/div";

            var expectedContent = "Test";
            var actualContent = _contentHandler.GetContentByXPath(doc, xpath);

            Assert.AreEqual(expectedContent, actualContent);
        }

        [Test]
        public void ShouldThrowOnXPathFail()
        {
            var doc = new HtmlDocument();
            var node = HtmlNode.CreateNode("<html><head></head><body><div>Test</div></body></html>");

            doc.DocumentNode.AppendChild(node);

            var xpath = "/html/body/p";

            Assert.Throws<Exception>(() => _contentHandler.GetContentByXPath(doc, xpath));
        }

        [Test]
        public void ShouldGetContentByCSS()
        {
            var doc = new HtmlDocument();
            var node = HtmlNode.CreateNode("<html><head></head><body><div id=\"item\">Test</div></body></html>");

            doc.DocumentNode.AppendChild(node);

            var css = "#item";

            var expectedContent = "Test";
            var actualContent = _contentHandler.GetContentByCSS(doc, css);

            Assert.AreEqual(expectedContent, actualContent);
        }

        [Test]
        public void ShouldThrowOnCSSFail()
        {
            var doc = new HtmlDocument();
            var node = HtmlNode.CreateNode("<html><head></head><body><div id=\"item\">Test</div></body></html>");

            doc.DocumentNode.AppendChild(node);

            var css = "#test\\";

            Assert.Throws<Exception>(() => _contentHandler.GetContentByCSS(doc, css));
        }
    }
}
