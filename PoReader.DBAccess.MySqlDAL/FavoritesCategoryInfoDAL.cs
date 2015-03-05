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
    /// FavoritesCategoryInfo数据库操作类
    /// </summary>
    public class FavoritesCategoryInfoDAL : IFavoritesCategoryInfoDAL
    {
        #region 常量
        public const string INSERT = "INSERT INTO `FavoritesCategoryInfo`(`FavoritesCategoryInfoId`, `UserId`, `Name`) VALUES(@FavoritesCategoryInfoId, @UserId, @Name)";
        public const string UPDATE = "UPDATE `FavoritesCategoryInfo` SET `UserId`=@UserId ,`Name`=@Name WHERE `FavoritesCategoryInfoId`=@FavoritesCategoryInfoId";
        public const string DELETE = "DELETE FROM `FavoritesCategoryInfo` WHERE `FavoritesCategoryInfoId`=@FavoritesCategoryInfoId";
        public const string DELETEALL = "DELETE FROM `FavoritesCategoryInfo`";
        public const string DELETEWHERE = "DELETE FROM `FavoritesCategoryInfo` WHERE {0}";
        public const string SELECT = "SELECT `FavoritesCategoryInfoId`, `UserId`, `Name` FROM `FavoritesCategoryInfo` WHERE {0} ORDER BY {1}";
        public const string SELECTALL = "SELECT `FavoritesCategoryInfoId`, `UserId`, `Name` FROM `FavoritesCategoryInfo` ORDER BY {0}";
        public const string GET = "SELECT `FavoritesCategoryInfoId`, `UserId`, `Name` FROM `FavoritesCategoryInfo` WHERE `FavoritesCategoryInfoId`=@FavoritesCategoryInfoId";
        public const string COUNTALL = "SELECT COUNT(-1) FROM `FavoritesCategoryInfo`";
        public const string COUNT = "SELECT COUNT(-1) FROM `FavoritesCategoryInfo` WHERE {0}";
        #endregion
        
        #region 构造函数
        public FavoritesCategoryInfoDAL()
        { }
        #endregion
        
        #region 方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="favoritesCategoryInfo"></param>
        /// <returns>受影响行数</returns>
        public int Insert(FavoritesCategoryInfo favoritesCategoryInfo)
        {
            int result = 0;
            if(favoritesCategoryInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramFavoritesCategoryInfoId = new MySqlParameter("@FavoritesCategoryInfoId", favoritesCategoryInfo.FavoritesCategoryInfoId);
            MySqlParameter paramUserId = new MySqlParameter("@UserId", favoritesCategoryInfo.UserId);
            MySqlParameter paramName = new MySqlParameter("@Name", favoritesCategoryInfo.Name);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(INSERT, paramFavoritesCategoryInfoId, paramUserId, paramName);
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
        /// <param name="IList<favoritesCategoryInfo>"></param>
        /// <returns>受影响行数</returns>
        public int Insert(IList<FavoritesCategoryInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (FavoritesCategoryInfo favoritesCategoryInfo in lists)
                {
                    MySqlParameter paramFavoritesCategoryInfoId = new MySqlParameter("@FavoritesCategoryInfoId", favoritesCategoryInfo.FavoritesCategoryInfoId);
                    MySqlParameter paramUserId = new MySqlParameter("@UserId", favoritesCategoryInfo.UserId);
                    MySqlParameter paramName = new MySqlParameter("@Name", favoritesCategoryInfo.Name);
                    
                    result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramFavoritesCategoryInfoId, paramUserId, paramName);
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
        /// <param name="favoritesCategoryInfo"></param>
        /// <returns></returns>
        public int InsertOrUpdate(FavoritesCategoryInfo favoritesCategoryInfo)
        {
            int result = 0;
            if (Count(string.Format("`FavoritesCategoryInfoId`='{0}'", favoritesCategoryInfo.FavoritesCategoryInfoId)) == 0)
            {
                result = Insert(favoritesCategoryInfo);
            }
            else
            {
                result = Update(favoritesCategoryInfo);
            }
            return result;
        }
        
        /// <summary>
        /// 批量增加或更新对象
        /// </summary>
        /// <param name="IList<favoritesCategoryInfo>"></param>
        /// <returns>受影响行数</returns>
        public int InsertOrUpdate(IList<FavoritesCategoryInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (FavoritesCategoryInfo favoritesCategoryInfo in lists)
                {
                    if (Count(string.Format("`FavoritesCategoryInfoId`='{0}'", favoritesCategoryInfo.FavoritesCategoryInfoId)) == 0)
                    {
                        MySqlParameter paramFavoritesCategoryInfoId = new MySqlParameter("@FavoritesCategoryInfoId", favoritesCategoryInfo.FavoritesCategoryInfoId);
                        MySqlParameter paramUserId = new MySqlParameter("@UserId", favoritesCategoryInfo.UserId);
                        MySqlParameter paramName = new MySqlParameter("@Name", favoritesCategoryInfo.Name);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramFavoritesCategoryInfoId, paramUserId, paramName);
                    }
                    else
                    {
                        MySqlParameter paramFavoritesCategoryInfoId = new MySqlParameter("@FavoritesCategoryInfoId", favoritesCategoryInfo.FavoritesCategoryInfoId);
                        MySqlParameter paramUserId = new MySqlParameter("@UserId", favoritesCategoryInfo.UserId);
                        MySqlParameter paramName = new MySqlParameter("@Name", favoritesCategoryInfo.Name);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, UPDATE, paramFavoritesCategoryInfoId, paramUserId, paramName);
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
        public int Update(FavoritesCategoryInfo favoritesCategoryInfo)
        {
            int result = 0;
            if(favoritesCategoryInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramFavoritesCategoryInfoId = new MySqlParameter("@FavoritesCategoryInfoId", favoritesCategoryInfo.FavoritesCategoryInfoId);
            MySqlParameter paramUserId = new MySqlParameter("@UserId", favoritesCategoryInfo.UserId);
            MySqlParameter paramName = new MySqlParameter("@Name", favoritesCategoryInfo.Name);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(UPDATE,paramFavoritesCategoryInfoId, paramUserId, paramName);
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
        public int Delete(string favoritesCategoryInfoId)
        {
            int result = 0;
            
            MySqlParameter paramFavoritesCategoryInfoId = new MySqlParameter("@FavoritesCategoryInfoId", favoritesCategoryInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(DELETE, paramFavoritesCategoryInfoId);
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
        public int Delete(IList<string> favoritesCategoryInfoIdLists)
        {
             int result = 0;
            if (favoritesCategoryInfoIdLists == null || favoritesCategoryInfoIdLists.Count == 0)
                return result;

            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            try
            {
                foreach (string favoritesCategoryInfoId in favoritesCategoryInfoIdLists)
                {
                    MySqlParameter paramFavoritesCategoryInfoId = new MySqlParameter("@FavoritesCategoryInfoId", favoritesCategoryInfoId);
                    result += MySqlHelper.ExecuteNonQuery(DELETE, paramFavoritesCategoryInfoId);
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
        public List<FavoritesCategoryInfo> Select(string where, string order = "")
        {
            List<FavoritesCategoryInfo> result = new List<FavoritesCategoryInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`FavoritesCategoryInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<FavoritesCategoryInfo>(sql);
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
        public List<FavoritesCategoryInfo> Select(string where, int offset, int limit, string order = "")
        {
            List<FavoritesCategoryInfo> result = new List<FavoritesCategoryInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`FavoritesCategoryInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<FavoritesCategoryInfo>(sql);
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
        public List<FavoritesCategoryInfo> SelectAll(string order = "")
        {
            List<FavoritesCategoryInfo> result = new List<FavoritesCategoryInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`FavoritesCategoryInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<FavoritesCategoryInfo>(sql);
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
        public List<FavoritesCategoryInfo> SelectAll(int offset, int limit, string order = "")
        {
            List<FavoritesCategoryInfo> result = new List<FavoritesCategoryInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`FavoritesCategoryInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<FavoritesCategoryInfo>(sql);
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
        /// <param name="favoritesCategoryInfoId">favoritesCategoryInfoId</param>
        /// <returns>查询的结果</returns>
        public FavoritesCategoryInfo Get(string favoritesCategoryInfoId)
        {
            FavoritesCategoryInfo result = null;
            
            MySqlParameter paramFavoritesCategoryInfoId = new MySqlParameter("@FavoritesCategoryInfoId", favoritesCategoryInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteEntity<FavoritesCategoryInfo>(GET, paramFavoritesCategoryInfoId);
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