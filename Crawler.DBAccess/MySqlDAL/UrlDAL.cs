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
    /// Url数据库操作类
    /// </summary>
    public class UrlDAL : IUrlDAL
    {
        #region 常量
        public const string INSERT = "INSERT INTO `Url`(`UrlId`, `Address`, `Count`) VALUES(@UrlId, @Address, @Count)";
        public const string UPDATE = "UPDATE `Url` SET `Address`=@Address ,`Count`=@Count WHERE UrlId=@UrlId";
        public const string DELETE = "DELETE FROM `Url` WHERE UrlId=@UrlId";
        public const string DELETEALL = "DELETE FROM `Url`";
        public const string DELETEWHERE = "DELETE FROM `Url` WHERE {0}";
        public const string SELECT = "SELECT `UrlId`, `Address`, `Count` FROM `Url` WHERE {0} ORDER BY {1}";
        public const string SELECTALL = "SELECT `UrlId`, `Address`, `Count` FROM `Url` ORDER BY {0}";
        public const string GET = "SELECT `UrlId`, `Address`, `Count` FROM `Url` WHERE UrlId=@UrlId";
        public const string COUNTALL = "SELECT COUNT(-1) FROM `Url`";
        public const string COUNT = "SELECT COUNT(-1) FROM `Url` WHERE {0}";
        #endregion
        
        #region 构造函数
        public UrlDAL()
        { }
        #endregion
        
        #region 方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="url"></param>
        /// <returns>受影响行数</returns>
        public int Insert(Url url)
        {
            int result = 0;
            if(url == null)
            {
                return result;
            }
            
            MySqlParameter paramUrlId = new MySqlParameter("@UrlId", url.UrlId);
            MySqlParameter paramAddress = new MySqlParameter("@Address", url.Address);
            MySqlParameter paramCount = new MySqlParameter("@Count", url.Count);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(INSERT, paramUrlId, paramAddress, paramCount);
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
        /// <param name="IList<url>"></param>
        /// <returns>受影响行数</returns>
        public int Insert(IList<Url> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (Url url in lists)
                {
                    MySqlParameter paramUrlId = new MySqlParameter("@UrlId", url.UrlId);
                    MySqlParameter paramAddress = new MySqlParameter("@Address", url.Address);
                    MySqlParameter paramCount = new MySqlParameter("@Count", url.Count);
                    
                    result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramUrlId, paramAddress, paramCount);
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
        /// <param name="url"></param>
        /// <returns></returns>
        public int InsertOrUpdate(Url url)
        {
            int result = 0;
            if (Count(string.Format("`UrlId`='{0}'", url.UrlId)) == 0)
            {
                result = Insert(url);
            }
            else
            {
                result = Update(url);
            }
            return result;
        }
        
        /// <summary>
        /// 批量增加或更新对象
        /// </summary>
        /// <param name="IList<url>"></param>
        /// <returns>受影响行数</returns>
        public int InsertOrUpdate(IList<Url> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (Url url in lists)
                {
                    if (Count(string.Format("`UrlId`='{0}'", url.UrlId)) == 0)
                    {
                        MySqlParameter paramUrlId = new MySqlParameter("@UrlId", url.UrlId);
                        MySqlParameter paramAddress = new MySqlParameter("@Address", url.Address);
                        MySqlParameter paramCount = new MySqlParameter("@Count", url.Count);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramUrlId, paramAddress, paramCount);
                    }
                    else
                    {
                        MySqlParameter paramUrlId = new MySqlParameter("@UrlId", url.UrlId);
                        MySqlParameter paramAddress = new MySqlParameter("@Address", url.Address);
                        MySqlParameter paramCount = new MySqlParameter("@Count", url.Count);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, UPDATE, paramUrlId, paramAddress, paramCount);
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
        public int Update(Url url)
        {
            int result = 0;
            if(url == null)
            {
                return result;
            }
            
            MySqlParameter paramUrlId = new MySqlParameter("@UrlId", url.UrlId);
            MySqlParameter paramAddress = new MySqlParameter("@Address", url.Address);
            MySqlParameter paramCount = new MySqlParameter("@Count", url.Count);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(UPDATE,paramUrlId, paramAddress, paramCount);
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
        public int Delete(long urlId)
        {
            int result = 0;
            
            MySqlParameter paramUrlId = new MySqlParameter("@UrlId", urlId);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(DELETE, paramUrlId);
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
        public int Delete(IList<long> urlIdLists)
        {
             int result = 0;
            if (urlIdLists == null || urlIdLists.Count == 0)
                return result;

            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            try
            {
                foreach (long urlId in urlIdLists)
                {
                    MySqlParameter paramUrlId = new MySqlParameter("@UrlId", urlId);
                    result += MySqlHelper.ExecuteNonQuery(DELETE, paramUrlId);
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
        public List<Url> Select(string where, string order = "")
        {
            List<Url> result = new List<Url>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`UrlId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<Url>(sql);
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
        public List<Url> SelectAll(string order = "")
        {
            List<Url> result = new List<Url>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`UrlId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<Url>(sql);
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
        /// <param name="urlId">urlId</param>
        /// <returns>查询的结果</returns>
        public Url Get(long urlId)
        {
            Url result = null;
            
            MySqlParameter paramUrlId = new MySqlParameter("@UrlId", urlId);
            
            try
            {
                result = MySqlHelper.ExecuteEntity<Url>(GET, paramUrlId);
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