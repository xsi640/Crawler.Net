using Crawler.Helper.Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Crawler.Capture
{
    public class HtmlParser
    {
        private string _HtmlContent = string.Empty;
        private string _Url = string.Empty;
        private string _Domain = string.Empty;
        private string _UrlPath = string.Empty;
        private HtmlDocument _HtmlDocument = new HtmlDocument();
        private IList<string> _Urls = null;
        private IList<string> _RsssUrls = null;

        public HtmlParser(string url, string htmlContent)
        {
            this._Url = url;
            this._HtmlContent = htmlContent;
            this.Init();
        }

        public string Url
        {
            get { return this._Url; }
        }

        public string HtmlContent
        {
            get { return this._HtmlContent; }
        }

        public IList<string> Urls
        {
            get { return this._Urls; }
        }

        public IList<string> RssUrls
        {
            get { return this._RsssUrls; }
        }

        private void Init()
        {
            Uri uri = new Uri(this._Url);
            this._Domain = uri.GetLeftPart(UriPartial.Authority);
            this._UrlPath = uri.GetLeftPart(UriPartial.Path);

            this._HtmlDocument.LoadHtml(this._HtmlContent);
        }

        public void GetRssUrls()
        {
            HtmlNodeCollection collection = this._HtmlDocument.DocumentNode.SelectNodes("//link[@type=\"application/rss+xml\"]");
            if (collection != null && collection.Count > 0)
            {
                this._RsssUrls = new List<string>();
                foreach (HtmlNode node in collection)
                {
                    string url = node.Attributes["href"].Value;
                    this._RsssUrls.Add(url);
                }
            }
        }

        public void GetUrls()
        {
            try
            {
                HtmlNodeCollection collection = this._HtmlDocument.DocumentNode.SelectNodes("//a[@href]");
                if (collection != null && collection.Count > 0)
                {
                    this._Urls = new List<string>();
                    foreach (HtmlNode node in collection)
                    {
                        string url = string.Empty;
                        string u = node.Attributes["href"].Value;
                        if (u.StartsWith("#"))
                            continue;
                        if (u == "/" || string.IsNullOrWhiteSpace(u))
                            continue;
                        if (!u.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (u.StartsWith("/", StringComparison.CurrentCultureIgnoreCase))
                            {
                                url = this._Domain + u;
                            }
                            else
                            {
                                url = this._Url + u;
                            }
                        }
                        else
                            url = u;

                        if (!url.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Debug.WriteLine("");
                        }
                        this._Urls.Add(url);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
