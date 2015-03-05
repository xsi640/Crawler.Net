
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using PoReader.DBAccess.IDAL;
using PoReader.DBAccess.MySqlDAL;

namespace PoReader.DBAccess.Factory
{
    public static class DALFactory
    {
    
        private static readonly string WebSiteSqlDAL = System.Configuration.ConfigurationManager.AppSettings["WebSiteSqlDAL"];
        
        private static IAdminInfoDAL _AdminInfoDAL = (IAdminInfoDAL)Assembly.Load(WebSiteSqlDAL).CreateInstance(WebSiteSqlDAL + ".AdminInfoDAL"); 
        private static IArticleInfoDAL _ArticleInfoDAL = (IArticleInfoDAL)Assembly.Load(WebSiteSqlDAL).CreateInstance(WebSiteSqlDAL + ".ArticleInfoDAL"); 
        private static ICategoryInfoDAL _CategoryInfoDAL = (ICategoryInfoDAL)Assembly.Load(WebSiteSqlDAL).CreateInstance(WebSiteSqlDAL + ".CategoryInfoDAL"); 
        private static IFavoritesCategoryInfoDAL _FavoritesCategoryInfoDAL = (IFavoritesCategoryInfoDAL)Assembly.Load(WebSiteSqlDAL).CreateInstance(WebSiteSqlDAL + ".FavoritesCategoryInfoDAL"); 
        private static IFavoritesInfoDAL _FavoritesInfoDAL = (IFavoritesInfoDAL)Assembly.Load(WebSiteSqlDAL).CreateInstance(WebSiteSqlDAL + ".FavoritesInfoDAL"); 
        private static IRssHostInfoDAL _RssHostInfoDAL = (IRssHostInfoDAL)Assembly.Load(WebSiteSqlDAL).CreateInstance(WebSiteSqlDAL + ".RssHostInfoDAL"); 
        private static IUrlDirectInfoDAL _UrlDirectInfoDAL = (IUrlDirectInfoDAL)Assembly.Load(WebSiteSqlDAL).CreateInstance(WebSiteSqlDAL + ".UrlDirectInfoDAL"); 
        private static IUserCategoryInfoDAL _UserCategoryInfoDAL = (IUserCategoryInfoDAL)Assembly.Load(WebSiteSqlDAL).CreateInstance(WebSiteSqlDAL + ".UserCategoryInfoDAL"); 
        private static IUserInfoDAL _UserInfoDAL = (IUserInfoDAL)Assembly.Load(WebSiteSqlDAL).CreateInstance(WebSiteSqlDAL + ".UserInfoDAL"); 
        
        public static IAdminInfoDAL AdminInfoDAL
        {
            get{ return _AdminInfoDAL;}
        }
        public static IArticleInfoDAL ArticleInfoDAL
        {
            get{ return _ArticleInfoDAL;}
        }
        public static ICategoryInfoDAL CategoryInfoDAL
        {
            get{ return _CategoryInfoDAL;}
        }
        public static IFavoritesCategoryInfoDAL FavoritesCategoryInfoDAL
        {
            get{ return _FavoritesCategoryInfoDAL;}
        }
        public static IFavoritesInfoDAL FavoritesInfoDAL
        {
            get{ return _FavoritesInfoDAL;}
        }
        public static IRssHostInfoDAL RssHostInfoDAL
        {
            get{ return _RssHostInfoDAL;}
        }
        public static IUrlDirectInfoDAL UrlDirectInfoDAL
        {
            get{ return _UrlDirectInfoDAL;}
        }
        public static IUserCategoryInfoDAL UserCategoryInfoDAL
        {
            get{ return _UserCategoryInfoDAL;}
        }
        public static IUserInfoDAL UserInfoDAL
        {
            get{ return _UserInfoDAL;}
        }
    }
}