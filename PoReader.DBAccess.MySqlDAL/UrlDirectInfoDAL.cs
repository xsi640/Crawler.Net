using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PoReader.DBAccess.Entities;
using PoReader.DBAccess.IDAL;
using MySql.Data.MySqlClient;

namespace PoReader.DBAccess.MySqlDAL
{
    /// <summary>
    /// UrlDirectInfo数据库操作类
    /// </summary>
    public class UrlDirectInfoDAL : IUrlDirectInfoDAL
    {
        #region 常量
        public const string INSERT = "INSERT INTO `UrlDirectInfo`(`UrlDirectInfoId`, `RssHostInfoId`, `Url`) VALUES(@UrlDirectInfoId, @RssHostInfoId, @Url)";
        public const string UPDATE = "UPDATE `UrlDirectInfo` SET `RssHostInfoId`=@RssHostInfoId ,`Url`=@Url WHERE `UrlDirectInfoId`=@UrlDirectInfoId";
        public const string DELETE = "DELETE FROM `UrlDirectInfo` WHERE `UrlDirectInfoId`=@UrlDirectInfoId";
        public const string DELETEALL = "DELETE FROM `UrlDirectInfo`";
        public const string DELETEWHERE = "DELETE FROM `UrlDirectInfo` WHERE {0}";
        public const string SELECT = "SELECT `UrlDirectInfoId`, `RssHostInfoId`, `Url` FROM `UrlDirectInfo` WHERE {0} ORDER BY {1}";
        public const string SELECTALL = "SELECT `UrlDirectInfoId`, `RssHostInfoId`, `Url` FROM `UrlDirectInfo` ORDER BY {0}";
        public const string GET = "SELECT `UrlDirectInfoId`, `RssHostInfoId`, `Url` FROM `UrlDirectInfo` WHERE `UrlDirectInfoId`=@UrlDirectInfoId";
        public const string COUNTALL = "SELECT COUNT(-1) FROM `UrlDirectInfo`";
        public const string COUNT = "SELECT COUNT(-1) FROM `UrlDirectInfo` WHERE {0}";
        #endregion
        
        #region 构造函数
        public UrlDirectInfoDAL()
        { }
        #endregion
        
        #region 方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="urlDirectInfo"></param>
        /// <returns>受影响行数</returns>
        public int Insert(UrlDirectInfo urlDirectInfo)
        {
            int result = 0;
            if(urlDirectInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramUrlDirectInfoId = new MySqlParameter("@UrlDirectInfoId", urlDirectInfo.UrlDirectInfoId);
            MySqlParameter paramRssHostInfoId = new MySqlParameter("@RssHostInfoId", urlDirectInfo.RssHostInfoId);
            MySqlParameter paramUrl = new MySqlParameter("@Url", urlDirectInfo.Url);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(INSERT, paramUrlDirectInfoId, paramRssHostInfoId, paramUrl);
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
        /// <param name="IList<urlDirectInfo>"></param>
        /// <returns>受影响行数</returns>
        public int Insert(IList<UrlDirectInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (UrlDirectInfo urlDirectInfo in lists)
                {
                    MySqlParameter paramUrlDirectInfoId = new MySqlParameter("@UrlDirectInfoId", urlDirectInfo.UrlDirectInfoId);
                    MySqlParameter paramRssHostInfoId = new MySqlParameter("@RssHostInfoId", urlDirectInfo.RssHostInfoId);
                    MySqlParameter paramUrl = new MySqlParameter("@Url", urlDirectInfo.Url);
                    
                    result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramUrlDirectInfoId, paramRssHostInfoId, paramUrl);
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
        /// <param name="urlDirectInfo"></param>
        /// <returns></returns>
        public int InsertOrUpdate(UrlDirectInfo urlDirectInfo)
        {
            int result = 0;
            if (Count(string.Format("`UrlDirectInfoId`='{0}'", urlDirectInfo.UrlDirectInfoId)) == 0)
            {
                result = Insert(urlDirectInfo);
            }
            else
            {
                result = Update(urlDirectInfo);
            }
            return result;
        }
        
        /// <summary>
        /// 批量增加或更新对象
        /// </summary>
        /// <param name="IList<urlDirectInfo>"></param>
        /// <returns>受影响行数</returns>
        public int InsertOrUpdate(IList<UrlDirectInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (UrlDirectInfo urlDirectInfo in lists)
                {
                    if (Count(string.Format("`UrlDirectInfoId`='{0}'", urlDirectInfo.UrlDirectInfoId)) == 0)
                    {
                        MySqlParameter paramUrlDirectInfoId = new MySqlParameter("@UrlDirectInfoId", urlDirectInfo.UrlDirectInfoId);
                        MySqlParameter paramRssHostInfoId = new MySqlParameter("@RssHostInfoId", urlDirectInfo.RssHostInfoId);
                        MySqlParameter paramUrl = new MySqlParameter("@Url", urlDirectInfo.Url);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramUrlDirectInfoId, paramRssHostInfoId, paramUrl);
                    }
                    else
                    {
                        MySqlParameter paramUrlDirectInfoId = new MySqlParameter("@UrlDirectInfoId", urlDirectInfo.UrlDirectInfoId);
                        MySqlParameter paramRssHostInfoId = new MySqlParameter("@RssHostInfoId", urlDirectInfo.RssHostInfoId);
                        MySqlParameter paramUrl = new MySqlParameter("@Url", urlDirectInfo.Url);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, UPDATE, paramUrlDirectInfoId, paramRssHostInfoId, paramUrl);
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
        public int Update(UrlDirectInfo urlDirectInfo)
        {
            int result = 0;
            if(urlDirectInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramUrlDirectInfoId = new MySqlParameter("@UrlDirectInfoId", urlDirectInfo.UrlDirectInfoId);
            MySqlParameter paramRssHostInfoId = new MySqlParameter("@RssHostInfoId", urlDirectInfo.RssHostInfoId);
            MySqlParameter paramUrl = new MySqlParameter("@Url", urlDirectInfo.Url);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(UPDATE,paramUrlDirectInfoId, paramRssHostInfoId, paramUrl);
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
        public int Delete(string urlDirectInfoId)
        {
            int result = 0;
            
            MySqlParameter paramUrlDirectInfoId = new MySqlParameter("@UrlDirectInfoId", urlDirectInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(DELETE, paramUrlDirectInfoId);
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
        public int Delete(IList<string> urlDirectInfoIdLists)
        {
             int result = 0;
            if (urlDirectInfoIdLists == null || urlDirectInfoIdLists.Count == 0)
                return result;

            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            try
            {
                foreach (string urlDirectInfoId in urlDirectInfoIdLists)
                {
                    MySqlParameter paramUrlDirectInfoId = new MySqlParameter("@UrlDirectInfoId", urlDirectInfoId);
                    result += MySqlHelper.ExecuteNonQuery(DELETE, paramUrlDirectInfoId);
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
        public List<UrlDirectInfo> Select(string where, string order = "")
        {
            List<UrlDirectInfo> result = new List<UrlDirectInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`UrlDirectInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<UrlDirectInfo>(sql);
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
        /// <param name="offset">索引位置</param>
        /// <param name="limit">返回记录数</param>
        /// <param name="order">排序</param>
        /// <returns>查询的结果</returns>
        public List<UrlDirectInfo> Select(string where, int offset, int limit, string order = "")
        {
            List<UrlDirectInfo> result = new List<UrlDirectInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`UrlDirectInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<UrlDirectInfo>(sql);
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
        public List<UrlDirectInfo> SelectAll(string order = "")
        {
            List<UrlDirectInfo> result = new List<UrlDirectInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`UrlDirectInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<UrlDirectInfo>(sql);
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
        public List<UrlDirectInfo> SelectAll(int offset, int limit, string order = "")
        {
            List<UrlDirectInfo> result = new List<UrlDirectInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`UrlDirectInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<UrlDirectInfo>(sql);
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
        /// <param name="urlDirectInfoId">urlDirectInfoId</param>
        /// <returns>查询的结果</returns>
        public UrlDirectInfo Get(string urlDirectInfoId)
        {
            UrlDirectInfo result = null;
            
            MySqlParameter paramUrlDirectInfoId = new MySqlParameter("@UrlDirectInfoId", urlDirectInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteEntity<UrlDirectInfo>(GET, paramUrlDirectInfoId);
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