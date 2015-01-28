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
        public string GetConetn(string url)
        {
            string result = string.Empty;
            var webRequest = WebRequest.Create(url);
            using (var response = webRequest.GetResponse())
            {
                using (var content = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(content))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }
            return result;
        }
    }
}
