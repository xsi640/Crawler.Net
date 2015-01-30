using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crawler.DBAccess.Entities;
using Crawler.DBAccess.IDAL;
using MySql.Data.MySqlClient;

namespace Crawler.DBAccess.MySqlDAL
{
    /// <summary>
    /// RssHost数据库操作类
    /// </summary>
    public class RssHostDAL : IRssHostDAL
    {
        #region 常量
        public const string INSERT = "INSERT INTO `RssHost`(`RssHostId`, `Title`, `SubTitle`, `Url`, `LastUpdateTime`) VALUES(@RssHostId, @Title, @SubTitle, @Url, @LastUpdateTime)";
        public const string UPDATE = "UPDATE `RssHost` SET `Title`=@Title ,`SubTitle`=@SubTitle ,`Url`=@Url ,`LastUpdateTime`=@LastUpdateTime WHERE RssHostId=@RssHostId";
        public const string DELETE = "DELETE FROM `RssHost` WHERE RssHostId=@RssHostId";
        public const string DELETEALL = "DELETE FROM `RssHost`";
        public const string DELETEWHERE = "DELETE FROM `RssHost` WHERE {0}";
        public const string SELECT = "SELECT `RssHostId`, `Title`, `SubTitle`, `Url`, `LastUpdateTime` FROM `RssHost` WHERE {0} ORDER BY {1}";
        public const string SELECTALL = "SELECT `RssHostId`, `Title`, `SubTitle`, `Url`, `LastUpdateTime` FROM `RssHost` ORDER BY {0}";
        public const string GET = "SELECT `RssHostId`, `Title`, `SubTitle`, `Url`, `LastUpdateTime` FROM `RssHost` WHERE RssHostId=@RssHostId";
        public const string COUNTALL = "SELECT COUNT(-1) FROM `RssHost`";
        public const string COUNT = "SELECT COUNT(-1) FROM `RssHost` WHERE {0}";
        #endregion
        
        #region 构造函数
        public RssHostDAL()
        { }
        #endregion
        
        #region 方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="rssHost"></param>
        /// <returns>受影响行数</returns>
        public int Insert(RssHost rssHost)
        {
            int result = 0;
            if(rssHost == null)
            {
                return result;
            }
            
            MySqlParameter paramRssHostId = new MySqlParameter("@RssHostId", rssHost.RssHostId);
            MySqlParameter paramTitle = new MySqlParameter("@Title", rssHost.Title);
            MySqlParameter paramSubTitle = new MySqlParameter("@SubTitle", rssHost.SubTitle);
            MySqlParameter paramUrl = new MySqlParameter("@Url", rssHost.Url);
            MySqlParameter paramLastUpdateTime = new MySqlParameter("@LastUpdateTime", rssHost.LastUpdateTime);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(INSERT, paramRssHostId, paramTitle, paramSubTitle, paramUrl, paramLastUpdateTime);
            }
            catch(MySqlException ex)
            {
                throw ex;
            }
            
            return result;
        }
        
        /// <summary>
        /// 批量增加新纪录
        /// </summary>
        /// <param name="IList<rssHost>"></param>
        /// <returns>受影响行数</returns>
        public int Insert(IList<RssHost> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (RssHost rssHost in lists)
                {
                    MySqlParameter paramRssHostId = new MySqlParameter("@RssHostId", rssHost.RssHostId);
                    MySqlParameter paramTitle = new MySqlParameter("@Title", rssHost.Title);
                    MySqlParameter paramSubTitle = new MySqlParameter("@SubTitle", rssHost.SubTitle);
                    MySqlParameter paramUrl = new MySqlParameter("@Url", rssHost.Url);
                    MySqlParameter paramLastUpdateTime = new MySqlParameter("@LastUpdateTime", rssHost.LastUpdateTime);
                    
                    result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramRssHostId, paramTitle, paramSubTitle, paramUrl, paramLastUpdateTime);
                }
                
                tran.Commit();
            }
            catch(MySqlException ex)
            {
                tran.Rollback();
                result = 0;
                throw ex;
            }
            
            return result;
        }

        /// <summary>
        /// 增加或更新对象
        /// </summary>
        /// <param name="rssHost"></param>
        /// <returns></returns>
        public int InsertOrUpdate(RssHost rssHost)
        {
            int result = 0;
            if (Count(string.Format("`RssHostId`='{0}'", rssHost.RssHostId)) == 0)
            {
                result = Insert(rssHost);
            }
            else
            {
                result = Update(rssHost);
            }
            return result;
        }
        
        /// <summary>
        /// 批量增加或更新对象
        /// </summary>
        /// <param name="IList<rssHost>"></param>
        /// <returns>受影响行数</returns>
        public int InsertOrUpdate(IList<RssHost> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (RssHost rssHost in lists)
                {
                    if (Count(string.Format("`RssHostId`='{0}'", rssHost.RssHostId)) == 0)
                    {
                        MySqlParameter paramRssHostId = new MySqlParameter("@RssHostId", rssHost.RssHostId);
                        MySqlParameter paramTitle = new MySqlParameter("@Title", rssHost.Title);
                        MySqlParameter paramSubTitle = new MySqlParameter("@SubTitle", rssHost.SubTitle);
                        MySqlParameter paramUrl = new MySqlParameter("@Url", rssHost.Url);
                        MySqlParameter paramLastUpdateTime = new MySqlParameter("@LastUpdateTime", rssHost.LastUpdateTime);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramRssHostId, paramTitle, paramSubTitle, paramUrl, paramLastUpdateTime);
                    }
                    else
                    {
                        MySqlParameter paramRssHostId = new MySqlParameter("@RssHostId", rssHost.RssHostId);
                        MySqlParameter paramTitle = new MySqlParameter("@Title", rssHost.Title);
                        MySqlParameter paramSubTitle = new MySqlParameter("@SubTitle", rssHost.SubTitle);
                        MySqlParameter paramUrl = new MySqlParameter("@Url", rssHost.Url);
                        MySqlParameter paramLastUpdateTime = new MySqlParameter("@LastUpdateTime", rssHost.LastUpdateTime);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, UPDATE, paramRssHostId, paramTitle, paramSubTitle, paramUrl, paramLastUpdateTime);
                    }
                }
                
                tran.Commit();
            }
            catch(MySqlException ex)
            {
                tran.Rollback();
                result = 0;
                throw ex;
            }
            
            return result;
        }
        
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns>受影响行数</returns>
        public int Update(RssHost rssHost)
        {
            int result = 0;
            if(rssHost == null)
            {
                return result;
            }
            
            MySqlParameter paramRssHostId = new MySqlParameter("@RssHostId", rssHost.RssHostId);
            MySqlParameter paramTitle = new MySqlParameter("@Title", rssHost.Title);
            MySqlParameter paramSubTitle = new MySqlParameter("@SubTitle", rssHost.SubTitle);
            MySqlParameter paramUrl = new MySqlParameter("@Url", rssHost.Url);
            MySqlParameter paramLastUpdateTime = new MySqlParameter("@LastUpdateTime", rssHost.LastUpdateTime);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(UPDATE,paramRssHostId, paramTitle, paramSubTitle, paramUrl, paramLastUpdateTime);
            }
            catch(MySqlException ex)
            {
                throw ex;
            }
            
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns>受影响行数</returns>
        public int Delete(long rssHostId)
        {
            int result = 0;
            
            MySqlParameter paramRssHostId = new MySqlParameter("@RssHostId", rssHostId);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(DELETE, paramRssHostId);
            }
            catch(MySqlException ex)
            {
                throw ex;
            }
            
            return result;
        }
        
        /// <summary>
        /// 删除满足条件的
        /// </summary>
        /// <returns>受影响行数</returns>
        public int Delete(IList<long> rssHostIdLists)
        {
             int result = 0;
            if (rssHostIdLists == null || rssHostIdLists.Count == 0)
                return result;

            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            try
            {
                foreach (long rssHostId in rssHostIdLists)
                {
                    MySqlParameter paramRssHostId = new MySqlParameter("@RssHostId", rssHostId);
                    result += MySqlHelper.ExecuteNonQuery(DELETE, paramRssHostId);
                }

                tran.Commit();
            }
            catch (MySqlException ex)
            {
                tran.Rollback();
                result = 0;
                throw ex;
            }

            return result;
        }
        
        /// <summary>
        /// 删除满足条件的
        /// </summary>
        /// <returns>受影响行数</returns>
        public int DeleteAll(string where)
        {
            int result = 0;
            if(string.IsNullOrEmpty(where))
            {
                return result;
            }
            string sql = string.Format(DELETEWHERE, where);
            try
            {
                result = MySqlHelper.ExecuteNonQuery(sql);
            }
            catch(MySqlException ex)
            {
                throw ex;
            }
            return result;
        }
        
        /// <summary>
        /// 删除所有
        /// </summary>
        /// <returns>受影响行数</returns>
        public int DeleteAll()
        {
            int result = 0;
            try
            {
                result = MySqlHelper.ExecuteNonQuery(DELETEALL);
            }
            catch(MySqlException ex)
            {
                throw ex;
            }
            return result;
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序</param>
        /// <returns>查询的结果</returns>
        public List<RssHost> Select(string where, string order = "")
        {
            List<RssHost> result = new List<RssHost>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`RssHostId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<RssHost>(sql);
            }
            catch(MySqlException ex)
            {
                throw ex;
            }
            return result;
        }
        
        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <param name="order">排序</param>
        /// <returns>查询的结果</returns>
        public List<RssHost> SelectAll(string order = "")
        {
            List<RssHost> result = new List<RssHost>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`RssHostId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<RssHost>(sql);
            }
            catch(MySqlException ex)
            {
                throw ex;
            }
            return result;
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="rssHostId">rssHostId</param>
        /// <returns>查询的结果</returns>
        public RssHost Get(long rssHostId)
        {
            RssHost result = null;
            
            MySqlParameter paramRssHostId = new MySqlParameter("@RssHostId", rssHostId);
            
            try
            {
                result = MySqlHelper.ExecuteEntity<RssHost>(GET, paramRssHostId);
            }
            catch(MySqlException ex)
            {
                throw ex;
            }
            return result;
        }
        
        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>记录数</returns>
        public int Count(string where)
        {
            int result = 0;
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            
            string sql = string.Format(COUNT, where);
            
            try
            {
                result = MySqlHelper.ExecuteScalar(sql);
            }
            catch(MySqlException ex)
            {
                throw ex;
            }
            return result;
        }
        
        
        /// <summary>
        /// 查询所有记录数
        /// </summary>
        /// <returns>记录数</returns>
        public int CountAll()
        {
            int result = 0;
            try
            {
                result = MySqlHelper.ExecuteScalar(COUNTALL);
            }
            catch(MySqlException ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion
    }
}