
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Crawler.DBAccess.IDAL;
using Crawler.DBAccess.MySqlDAL;

namespace Crawler.DBAccess.Factory
{
    public static class DALFactory
    {
    
        private static readonly string WebSiteSqlDAL = System.Configuration.ConfigurationManager.AppSettings["WebSiteSqlDAL"];
        
        private static ICaptureDAL _CaptureDAL = (ICaptureDAL)Assembly.Load(WebSiteSqlDAL).CreateInstance(WebSiteSqlDAL + ".CaptureDAL"); 
        private static IRssHostDAL _RssHostDAL = (IRssHostDAL)Assembly.Load(WebSiteSqlDAL).CreateInstance(WebSiteSqlDAL + ".RssHostDAL"); 
        private static IUrlDAL _UrlDAL = (IUrlDAL)Assembly.Load(WebSiteSqlDAL).CreateInstance(WebSiteSqlDAL + ".UrlDAL"); 
        
        public static ICaptureDAL CaptureDAL
        {
            get{ return _CaptureDAL;}
        }
        public static IRssHostDAL RssHostDAL
        {
            get{ return _RssHostDAL;}
        }
        public static IUrlDAL UrlDAL
        {
            get{ return _UrlDAL;}
        }
    }
}