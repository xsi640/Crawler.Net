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
    /// RssHostInfo数据库操作类
    /// </summary>
    public class RssHostInfoDAL : IRssHostInfoDAL
    {
        #region 常量
        public const string INSERT = "INSERT INTO `RssHostInfo`(`RssHostInfoId`, `CategoryId`, `Title`, `Url`, `LastUpdateTime`, `Status`, `CheckedId`, `CreateUserId`, `Tags`, `ReadedTime`, `NextReadTime`) VALUES(@RssHostInfoId, @CategoryId, @Title, @Url, @LastUpdateTime, @Status, @CheckedId, @CreateUserId, @Tags, @ReadedTime, @NextReadTime)";
        public const string UPDATE = "UPDATE `RssHostInfo` SET `CategoryId`=@CategoryId ,`Title`=@Title ,`Url`=@Url ,`LastUpdateTime`=@LastUpdateTime ,`Status`=@Status ,`CheckedId`=@CheckedId ,`CreateUserId`=@CreateUserId ,`Tags`=@Tags ,`ReadedTime`=@ReadedTime ,`NextReadTime`=@NextReadTime WHERE `RssHostInfoId`=@RssHostInfoId";
        public const string DELETE = "DELETE FROM `RssHostInfo` WHERE `RssHostInfoId`=@RssHostInfoId";
        public const string DELETEALL = "DELETE FROM `RssHostInfo`";
        public const string DELETEWHERE = "DELETE FROM `RssHostInfo` WHERE {0}";
        public const string SELECT = "SELECT `RssHostInfoId`, `CategoryId`, `Title`, `Url`, `LastUpdateTime`, `Status`, `CheckedId`, `CreateUserId`, `Tags`, `ReadedTime`, `NextReadTime` FROM `RssHostInfo` WHERE {0} ORDER BY {1}";
        public const string SELECTALL = "SELECT `RssHostInfoId`, `CategoryId`, `Title`, `Url`, `LastUpdateTime`, `Status`, `CheckedId`, `CreateUserId`, `Tags`, `ReadedTime`, `NextReadTime` FROM `RssHostInfo` ORDER BY {0}";
        public const string GET = "SELECT `RssHostInfoId`, `CategoryId`, `Title`, `Url`, `LastUpdateTime`, `Status`, `CheckedId`, `CreateUserId`, `Tags`, `ReadedTime`, `NextReadTime` FROM `RssHostInfo` WHERE `RssHostInfoId`=@RssHostInfoId";
        public const string COUNTALL = "SELECT COUNT(-1) FROM `RssHostInfo`";
        public const string COUNT = "SELECT COUNT(-1) FROM `RssHostInfo` WHERE {0}";
        #endregion
        
        #region 构造函数
        public RssHostInfoDAL()
        { }
        #endregion
        
        #region 方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="rssHostInfo"></param>
        /// <returns>受影响行数</returns>
        public int Insert(RssHostInfo rssHostInfo)
        {
            int result = 0;
            if(rssHostInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramRssHostInfoId = new MySqlParameter("@RssHostInfoId", rssHostInfo.RssHostInfoId);
            MySqlParameter paramCategoryId = new MySqlParameter("@CategoryId", rssHostInfo.CategoryId);
            MySqlParameter paramTitle = new MySqlParameter("@Title", rssHostInfo.Title);
            MySqlParameter paramUrl = new MySqlParameter("@Url", rssHostInfo.Url);
            MySqlParameter paramLastUpdateTime = new MySqlParameter("@LastUpdateTime", rssHostInfo.LastUpdateTime);
            MySqlParameter paramStatus = new MySqlParameter("@Status", rssHostInfo.Status);
            MySqlParameter paramCheckedId = new MySqlParameter("@CheckedId", rssHostInfo.CheckedId);
            MySqlParameter paramCreateUserId = new MySqlParameter("@CreateUserId", rssHostInfo.CreateUserId);
            MySqlParameter paramTags = new MySqlParameter("@Tags", rssHostInfo.Tags);
            MySqlParameter paramReadedTime = new MySqlParameter("@ReadedTime", rssHostInfo.ReadedTime);
            MySqlParameter paramNextReadTime = new MySqlParameter("@NextReadTime", rssHostInfo.NextReadTime);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(INSERT, paramRssHostInfoId, paramCategoryId, paramTitle, paramUrl, paramLastUpdateTime, paramStatus, paramCheckedId, paramCreateUserId, paramTags, paramReadedTime, paramNextReadTime);
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
        /// <param name="IList<rssHostInfo>"></param>
        /// <returns>受影响行数</returns>
        public int Insert(IList<RssHostInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (RssHostInfo rssHostInfo in lists)
                {
                    MySqlParameter paramRssHostInfoId = new MySqlParameter("@RssHostInfoId", rssHostInfo.RssHostInfoId);
                    MySqlParameter paramCategoryId = new MySqlParameter("@CategoryId", rssHostInfo.CategoryId);
                    MySqlParameter paramTitle = new MySqlParameter("@Title", rssHostInfo.Title);
                    MySqlParameter paramUrl = new MySqlParameter("@Url", rssHostInfo.Url);
                    MySqlParameter paramLastUpdateTime = new MySqlParameter("@LastUpdateTime", rssHostInfo.LastUpdateTime);
                    MySqlParameter paramStatus = new MySqlParameter("@Status", rssHostInfo.Status);
                    MySqlParameter paramCheckedId = new MySqlParameter("@CheckedId", rssHostInfo.CheckedId);
                    MySqlParameter paramCreateUserId = new MySqlParameter("@CreateUserId", rssHostInfo.CreateUserId);
                    MySqlParameter paramTags = new MySqlParameter("@Tags", rssHostInfo.Tags);
                    MySqlParameter paramReadedTime = new MySqlParameter("@ReadedTime", rssHostInfo.ReadedTime);
                    MySqlParameter paramNextReadTime = new MySqlParameter("@NextReadTime", rssHostInfo.NextReadTime);
                    
                    result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramRssHostInfoId, paramCategoryId, paramTitle, paramUrl, paramLastUpdateTime, paramStatus, paramCheckedId, paramCreateUserId, paramTags, paramReadedTime, paramNextReadTime);
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
        /// <param name="rssHostInfo"></param>
        /// <returns></returns>
        public int InsertOrUpdate(RssHostInfo rssHostInfo)
        {
            int result = 0;
            if (Count(string.Format("`RssHostInfoId`='{0}'", rssHostInfo.RssHostInfoId)) == 0)
            {
                result = Insert(rssHostInfo);
            }
            else
            {
                result = Update(rssHostInfo);
            }
            return result;
        }
        
        /// <summary>
        /// 批量增加或更新对象
        /// </summary>
        /// <param name="IList<rssHostInfo>"></param>
        /// <returns>受影响行数</returns>
        public int InsertOrUpdate(IList<RssHostInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (RssHostInfo rssHostInfo in lists)
                {
                    if (Count(string.Format("`RssHostInfoId`='{0}'", rssHostInfo.RssHostInfoId)) == 0)
                    {
                        MySqlParameter paramRssHostInfoId = new MySqlParameter("@RssHostInfoId", rssHostInfo.RssHostInfoId);
                        MySqlParameter paramCategoryId = new MySqlParameter("@CategoryId", rssHostInfo.CategoryId);
                        MySqlParameter paramTitle = new MySqlParameter("@Title", rssHostInfo.Title);
                        MySqlParameter paramUrl = new MySqlParameter("@Url", rssHostInfo.Url);
                        MySqlParameter paramLastUpdateTime = new MySqlParameter("@LastUpdateTime", rssHostInfo.LastUpdateTime);
                        MySqlParameter paramStatus = new MySqlParameter("@Status", rssHostInfo.Status);
                        MySqlParameter paramCheckedId = new MySqlParameter("@CheckedId", rssHostInfo.CheckedId);
                        MySqlParameter paramCreateUserId = new MySqlParameter("@CreateUserId", rssHostInfo.CreateUserId);
                        MySqlParameter paramTags = new MySqlParameter("@Tags", rssHostInfo.Tags);
                        MySqlParameter paramReadedTime = new MySqlParameter("@ReadedTime", rssHostInfo.ReadedTime);
                        MySqlParameter paramNextReadTime = new MySqlParameter("@NextReadTime", rssHostInfo.NextReadTime);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramRssHostInfoId, paramCategoryId, paramTitle, paramUrl, paramLastUpdateTime, paramStatus, paramCheckedId, paramCreateUserId, paramTags, paramReadedTime, paramNextReadTime);
                    }
                    else
                    {
                        MySqlParameter paramRssHostInfoId = new MySqlParameter("@RssHostInfoId", rssHostInfo.RssHostInfoId);
                        MySqlParameter paramCategoryId = new MySqlParameter("@CategoryId", rssHostInfo.CategoryId);
                        MySqlParameter paramTitle = new MySqlParameter("@Title", rssHostInfo.Title);
                        MySqlParameter paramUrl = new MySqlParameter("@Url", rssHostInfo.Url);
                        MySqlParameter paramLastUpdateTime = new MySqlParameter("@LastUpdateTime", rssHostInfo.LastUpdateTime);
                        MySqlParameter paramStatus = new MySqlParameter("@Status", rssHostInfo.Status);
                        MySqlParameter paramCheckedId = new MySqlParameter("@CheckedId", rssHostInfo.CheckedId);
                        MySqlParameter paramCreateUserId = new MySqlParameter("@CreateUserId", rssHostInfo.CreateUserId);
                        MySqlParameter paramTags = new MySqlParameter("@Tags", rssHostInfo.Tags);
                        MySqlParameter paramReadedTime = new MySqlParameter("@ReadedTime", rssHostInfo.ReadedTime);
                        MySqlParameter paramNextReadTime = new MySqlParameter("@NextReadTime", rssHostInfo.NextReadTime);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, UPDATE, paramRssHostInfoId, paramCategoryId, paramTitle, paramUrl, paramLastUpdateTime, paramStatus, paramCheckedId, paramCreateUserId, paramTags, paramReadedTime, paramNextReadTime);
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
        public int Update(RssHostInfo rssHostInfo)
        {
            int result = 0;
            if(rssHostInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramRssHostInfoId = new MySqlParameter("@RssHostInfoId", rssHostInfo.RssHostInfoId);
            MySqlParameter paramCategoryId = new MySqlParameter("@CategoryId", rssHostInfo.CategoryId);
            MySqlParameter paramTitle = new MySqlParameter("@Title", rssHostInfo.Title);
            MySqlParameter paramUrl = new MySqlParameter("@Url", rssHostInfo.Url);
            MySqlParameter paramLastUpdateTime = new MySqlParameter("@LastUpdateTime", rssHostInfo.LastUpdateTime);
            MySqlParameter paramStatus = new MySqlParameter("@Status", rssHostInfo.Status);
            MySqlParameter paramCheckedId = new MySqlParameter("@CheckedId", rssHostInfo.CheckedId);
            MySqlParameter paramCreateUserId = new MySqlParameter("@CreateUserId", rssHostInfo.CreateUserId);
            MySqlParameter paramTags = new MySqlParameter("@Tags", rssHostInfo.Tags);
            MySqlParameter paramReadedTime = new MySqlParameter("@ReadedTime", rssHostInfo.ReadedTime);
            MySqlParameter paramNextReadTime = new MySqlParameter("@NextReadTime", rssHostInfo.NextReadTime);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(UPDATE,paramRssHostInfoId, paramCategoryId, paramTitle, paramUrl, paramLastUpdateTime, paramStatus, paramCheckedId, paramCreateUserId, paramTags, paramReadedTime, paramNextReadTime);
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
        public int Delete(string rssHostInfoId)
        {
            int result = 0;
            
            MySqlParameter paramRssHostInfoId = new MySqlParameter("@RssHostInfoId", rssHostInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(DELETE, paramRssHostInfoId);
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
        public int Delete(IList<string> rssHostInfoIdLists)
        {
             int result = 0;
            if (rssHostInfoIdLists == null || rssHostInfoIdLists.Count == 0)
                return result;

            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            try
            {
                foreach (string rssHostInfoId in rssHostInfoIdLists)
                {
                    MySqlParameter paramRssHostInfoId = new MySqlParameter("@RssHostInfoId", rssHostInfoId);
                    result += MySqlHelper.ExecuteNonQuery(DELETE, paramRssHostInfoId);
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
        public List<RssHostInfo> Select(string where, string order = "")
        {
            List<RssHostInfo> result = new List<RssHostInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`RssHostInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<RssHostInfo>(sql);
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
        public List<RssHostInfo> Select(string where, int offset, int limit, string order = "")
        {
            List<RssHostInfo> result = new List<RssHostInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`RssHostInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<RssHostInfo>(sql);
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
        public List<RssHostInfo> SelectAll(string order = "")
        {
            List<RssHostInfo> result = new List<RssHostInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`RssHostInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<RssHostInfo>(sql);
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
        public List<RssHostInfo> SelectAll(int offset, int limit, string order = "")
        {
            List<RssHostInfo> result = new List<RssHostInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`RssHostInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<RssHostInfo>(sql);
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
        /// <param name="rssHostInfoId">rssHostInfoId</param>
        /// <returns>查询的结果</returns>
        public RssHostInfo Get(string rssHostInfoId)
        {
            RssHostInfo result = null;
            
            MySqlParameter paramRssHostInfoId = new MySqlParameter("@RssHostInfoId", rssHostInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteEntity<RssHostInfo>(GET, paramRssHostInfoId);
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