using Crawler.Capture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crawler.Net.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            CapturerManager mananger = new CapturerManager();
            mananger.Add("http://www.cnblogs.com/");
            System.Console.ReadKey();
        }
    }
}
