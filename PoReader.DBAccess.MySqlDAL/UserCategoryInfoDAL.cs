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
    /// UserCategoryInfo数据库操作类
    /// </summary>
    public class UserCategoryInfoDAL : IUserCategoryInfoDAL
    {
        #region 常量
        public const string INSERT = "INSERT INTO `UserCategoryInfo`(`UserCategoryInfoId`, `UserId`, `Name`, `ParentId`, `Count`, `SubCount`, `Layer`, `RssHostIds`, `CreateTime`) VALUES(@UserCategoryInfoId, @UserId, @Name, @ParentId, @Count, @SubCount, @Layer, @RssHostIds, @CreateTime)";
        public const string UPDATE = "UPDATE `UserCategoryInfo` SET `UserId`=@UserId ,`Name`=@Name ,`ParentId`=@ParentId ,`Count`=@Count ,`SubCount`=@SubCount ,`Layer`=@Layer ,`RssHostIds`=@RssHostIds ,`CreateTime`=@CreateTime WHERE `UserCategoryInfoId`=@UserCategoryInfoId";
        public const string DELETE = "DELETE FROM `UserCategoryInfo` WHERE `UserCategoryInfoId`=@UserCategoryInfoId";
        public const string DELETEALL = "DELETE FROM `UserCategoryInfo`";
        public const string DELETEWHERE = "DELETE FROM `UserCategoryInfo` WHERE {0}";
        public const string SELECT = "SELECT `UserCategoryInfoId`, `UserId`, `Name`, `ParentId`, `Count`, `SubCount`, `Layer`, `RssHostIds`, `CreateTime` FROM `UserCategoryInfo` WHERE {0} ORDER BY {1}";
        public const string SELECTALL = "SELECT `UserCategoryInfoId`, `UserId`, `Name`, `ParentId`, `Count`, `SubCount`, `Layer`, `RssHostIds`, `CreateTime` FROM `UserCategoryInfo` ORDER BY {0}";
        public const string GET = "SELECT `UserCategoryInfoId`, `UserId`, `Name`, `ParentId`, `Count`, `SubCount`, `Layer`, `RssHostIds`, `CreateTime` FROM `UserCategoryInfo` WHERE `UserCategoryInfoId`=@UserCategoryInfoId";
        public const string COUNTALL = "SELECT COUNT(-1) FROM `UserCategoryInfo`";
        public const string COUNT = "SELECT COUNT(-1) FROM `UserCategoryInfo` WHERE {0}";
        #endregion
        
        #region 构造函数
        public UserCategoryInfoDAL()
        { }
        #endregion
        
        #region 方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="userCategoryInfo"></param>
        /// <returns>受影响行数</returns>
        public int Insert(UserCategoryInfo userCategoryInfo)
        {
            int result = 0;
            if(userCategoryInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramUserCategoryInfoId = new MySqlParameter("@UserCategoryInfoId", userCategoryInfo.UserCategoryInfoId);
            MySqlParameter paramUserId = new MySqlParameter("@UserId", userCategoryInfo.UserId);
            MySqlParameter paramName = new MySqlParameter("@Name", userCategoryInfo.Name);
            MySqlParameter paramParentId = new MySqlParameter("@ParentId", userCategoryInfo.ParentId);
            MySqlParameter paramCount = new MySqlParameter("@Count", userCategoryInfo.Count);
            MySqlParameter paramSubCount = new MySqlParameter("@SubCount", userCategoryInfo.SubCount);
            MySqlParameter paramLayer = new MySqlParameter("@Layer", userCategoryInfo.Layer);
            MySqlParameter paramRssHostIds = new MySqlParameter("@RssHostIds", userCategoryInfo.RssHostIds);
            MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", userCategoryInfo.CreateTime);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(INSERT, paramUserCategoryInfoId, paramUserId, paramName, paramParentId, paramCount, paramSubCount, paramLayer, paramRssHostIds, paramCreateTime);
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
        /// <param name="IList<userCategoryInfo>"></param>
        /// <returns>受影响行数</returns>
        public int Insert(IList<UserCategoryInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (UserCategoryInfo userCategoryInfo in lists)
                {
                    MySqlParameter paramUserCategoryInfoId = new MySqlParameter("@UserCategoryInfoId", userCategoryInfo.UserCategoryInfoId);
                    MySqlParameter paramUserId = new MySqlParameter("@UserId", userCategoryInfo.UserId);
                    MySqlParameter paramName = new MySqlParameter("@Name", userCategoryInfo.Name);
                    MySqlParameter paramParentId = new MySqlParameter("@ParentId", userCategoryInfo.ParentId);
                    MySqlParameter paramCount = new MySqlParameter("@Count", userCategoryInfo.Count);
                    MySqlParameter paramSubCount = new MySqlParameter("@SubCount", userCategoryInfo.SubCount);
                    MySqlParameter paramLayer = new MySqlParameter("@Layer", userCategoryInfo.Layer);
                    MySqlParameter paramRssHostIds = new MySqlParameter("@RssHostIds", userCategoryInfo.RssHostIds);
                    MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", userCategoryInfo.CreateTime);
                    
                    result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramUserCategoryInfoId, paramUserId, paramName, paramParentId, paramCount, paramSubCount, paramLayer, paramRssHostIds, paramCreateTime);
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
        /// <param name="userCategoryInfo"></param>
        /// <returns></returns>
        public int InsertOrUpdate(UserCategoryInfo userCategoryInfo)
        {
            int result = 0;
            if (Count(string.Format("`UserCategoryInfoId`='{0}'", userCategoryInfo.UserCategoryInfoId)) == 0)
            {
                result = Insert(userCategoryInfo);
            }
            else
            {
                result = Update(userCategoryInfo);
            }
            return result;
        }
        
        /// <summary>
        /// 批量增加或更新对象
        /// </summary>
        /// <param name="IList<userCategoryInfo>"></param>
        /// <returns>受影响行数</returns>
        public int InsertOrUpdate(IList<UserCategoryInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (UserCategoryInfo userCategoryInfo in lists)
                {
                    if (Count(string.Format("`UserCategoryInfoId`='{0}'", userCategoryInfo.UserCategoryInfoId)) == 0)
                    {
                        MySqlParameter paramUserCategoryInfoId = new MySqlParameter("@UserCategoryInfoId", userCategoryInfo.UserCategoryInfoId);
                        MySqlParameter paramUserId = new MySqlParameter("@UserId", userCategoryInfo.UserId);
                        MySqlParameter paramName = new MySqlParameter("@Name", userCategoryInfo.Name);
                        MySqlParameter paramParentId = new MySqlParameter("@ParentId", userCategoryInfo.ParentId);
                        MySqlParameter paramCount = new MySqlParameter("@Count", userCategoryInfo.Count);
                        MySqlParameter paramSubCount = new MySqlParameter("@SubCount", userCategoryInfo.SubCount);
                        MySqlParameter paramLayer = new MySqlParameter("@Layer", userCategoryInfo.Layer);
                        MySqlParameter paramRssHostIds = new MySqlParameter("@RssHostIds", userCategoryInfo.RssHostIds);
                        MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", userCategoryInfo.CreateTime);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramUserCategoryInfoId, paramUserId, paramName, paramParentId, paramCount, paramSubCount, paramLayer, paramRssHostIds, paramCreateTime);
                    }
                    else
                    {
                        MySqlParameter paramUserCategoryInfoId = new MySqlParameter("@UserCategoryInfoId", userCategoryInfo.UserCategoryInfoId);
                        MySqlParameter paramUserId = new MySqlParameter("@UserId", userCategoryInfo.UserId);
                        MySqlParameter paramName = new MySqlParameter("@Name", userCategoryInfo.Name);
                        MySqlParameter paramParentId = new MySqlParameter("@ParentId", userCategoryInfo.ParentId);
                        MySqlParameter paramCount = new MySqlParameter("@Count", userCategoryInfo.Count);
                        MySqlParameter paramSubCount = new MySqlParameter("@SubCount", userCategoryInfo.SubCount);
                        MySqlParameter paramLayer = new MySqlParameter("@Layer", userCategoryInfo.Layer);
                        MySqlParameter paramRssHostIds = new MySqlParameter("@RssHostIds", userCategoryInfo.RssHostIds);
                        MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", userCategoryInfo.CreateTime);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, UPDATE, paramUserCategoryInfoId, paramUserId, paramName, paramParentId, paramCount, paramSubCount, paramLayer, paramRssHostIds, paramCreateTime);
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
        public int Update(UserCategoryInfo userCategoryInfo)
        {
            int result = 0;
            if(userCategoryInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramUserCategoryInfoId = new MySqlParameter("@UserCategoryInfoId", userCategoryInfo.UserCategoryInfoId);
            MySqlParameter paramUserId = new MySqlParameter("@UserId", userCategoryInfo.UserId);
            MySqlParameter paramName = new MySqlParameter("@Name", userCategoryInfo.Name);
            MySqlParameter paramParentId = new MySqlParameter("@ParentId", userCategoryInfo.ParentId);
            MySqlParameter paramCount = new MySqlParameter("@Count", userCategoryInfo.Count);
            MySqlParameter paramSubCount = new MySqlParameter("@SubCount", userCategoryInfo.SubCount);
            MySqlParameter paramLayer = new MySqlParameter("@Layer", userCategoryInfo.Layer);
            MySqlParameter paramRssHostIds = new MySqlParameter("@RssHostIds", userCategoryInfo.RssHostIds);
            MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", userCategoryInfo.CreateTime);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(UPDATE,paramUserCategoryInfoId, paramUserId, paramName, paramParentId, paramCount, paramSubCount, paramLayer, paramRssHostIds, paramCreateTime);
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
        public int Delete(string userCategoryInfoId)
        {
            int result = 0;
            
            MySqlParameter paramUserCategoryInfoId = new MySqlParameter("@UserCategoryInfoId", userCategoryInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(DELETE, paramUserCategoryInfoId);
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
        public int Delete(IList<string> userCategoryInfoIdLists)
        {
             int result = 0;
            if (userCategoryInfoIdLists == null || userCategoryInfoIdLists.Count == 0)
                return result;

            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            try
            {
                foreach (string userCategoryInfoId in userCategoryInfoIdLists)
                {
                    MySqlParameter paramUserCategoryInfoId = new MySqlParameter("@UserCategoryInfoId", userCategoryInfoId);
                    result += MySqlHelper.ExecuteNonQuery(DELETE, paramUserCategoryInfoId);
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
        public List<UserCategoryInfo> Select(string where, string order = "")
        {
            List<UserCategoryInfo> result = new List<UserCategoryInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`UserCategoryInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<UserCategoryInfo>(sql);
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
        public List<UserCategoryInfo> Select(string where, int offset, int limit, string order = "")
        {
            List<UserCategoryInfo> result = new List<UserCategoryInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`UserCategoryInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<UserCategoryInfo>(sql);
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
        public List<UserCategoryInfo> SelectAll(string order = "")
        {
            List<UserCategoryInfo> result = new List<UserCategoryInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`UserCategoryInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<UserCategoryInfo>(sql);
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
        public List<UserCategoryInfo> SelectAll(int offset, int limit, string order = "")
        {
            List<UserCategoryInfo> result = new List<UserCategoryInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`UserCategoryInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<UserCategoryInfo>(sql);
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
        /// <param name="userCategoryInfoId">userCategoryInfoId</param>
        /// <returns>查询的结果</returns>
        public UserCategoryInfo Get(string userCategoryInfoId)
        {
            UserCategoryInfo result = null;
            
            MySqlParameter paramUserCategoryInfoId = new MySqlParameter("@UserCategoryInfoId", userCategoryInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteEntity<UserCategoryInfo>(GET, paramUserCategoryInfoId);
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