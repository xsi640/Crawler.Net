using Crawler.Helper.Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Crawler.Capture
{
    public class HtmlParser
    {
        private string _HtmlContent = string.Empty;
        private HtmlDocument _HtmlDocument = new HtmlDocument();
        private IList<string> _Urls = null;
        private IList<string> _RsssUrls = null;

        public HtmlParser(string htmlContent)
        {
            this._HtmlContent = htmlContent;
        }

        private void Init()
        {
            this._HtmlDocument.Load(this._HtmlContent);
        }

        private void GetRssUrls()
        {
            var rssLinks = this._HtmlDocument.DocumentNode.Descendants("link")
                           .Where(n => n.Attributes["type"] != null && n.Attributes["type"].Value == "application/rss+xml")
                           .Select(n => n.Attributes["href"].Value);
            this._RsssUrls = rssLinks.ToList();
        }

        private void GetUrls(string htmlContent)
        {
            var links = this._HtmlDocument.DocumentNode.Descendants("a")
                           .Select(n => n.Attributes["href"].Value);
            return links.ToList();
        }
    }
}
