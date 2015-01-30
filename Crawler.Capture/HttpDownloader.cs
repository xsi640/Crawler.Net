using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Crawler.Capture
{
    public class HttpDownloader
    {
        private IWebProxy _WebProxy = null;
        private string _Url = string.Empty;
        private string _Domain = string.Empty;
        private string _UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        private int _Timeout = 30000;
        private string _ContentType = "application/x-www-form-urlencoded";
        private string _HtmlContent = string.Empty;

        public HttpDownloader(string url)
        {
            this._Url = url;
        }

        public string Url
        {
            get { return this._Url; }
        }
        public string HtmlContent
        {
            get { return this._HtmlContent; }
        }

        public void Download()
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this._Url);
            httpWebRequest.Proxy = this._WebProxy;
            httpWebRequest.UserAgent = this._UserAgent;
            httpWebRequest.ContentType = this._ContentType;
            httpWebRequest.Timeout = this._Timeout;
            using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (Stream stream = httpWebResponse.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        this._HtmlContent = sr.ReadToEnd();
                    }
                }
            }
        }
    }
}
