using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Crawler.Capture
{
    public class UrlCapture
    {
        private static Regex _UrlRegex = new Regex("<a\\s+(?:[^>]*?\\s+)?href=\"([^\"]*)\"", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static string[] _UrlPrefix = new string[] { "http", "https" };


        private IList<string> GetUrls(string content)
        {
            IList<string> result = new List<string>();
            Match m;
            for (m = _UrlRegex.Match(content); m.Success; m = m.NextMatch())
            {
                string url = m.Groups[1].Value;
                foreach (string u in _UrlPrefix)
                {
                    if (url.StartsWith(u))
                    {
                        result.Add(url);
                        break;
                    }
                }
            }
            return result;
        }
    }
}
