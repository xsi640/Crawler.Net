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
    /// FavoritesInfo数据库操作类
    /// </summary>
    public class FavoritesInfoDAL : IFavoritesInfoDAL
    {
        #region 常量
        public const string INSERT = "INSERT INTO `FavoritesInfo`(`FavoritesInfoId`, `FavoritesCategoryId`, `ArticleInfoId`, `Name`) VALUES(@FavoritesInfoId, @FavoritesCategoryId, @ArticleInfoId, @Name)";
        public const string UPDATE = "UPDATE `FavoritesInfo` SET `FavoritesCategoryId`=@FavoritesCategoryId ,`ArticleInfoId`=@ArticleInfoId ,`Name`=@Name WHERE `FavoritesInfoId`=@FavoritesInfoId";
        public const string DELETE = "DELETE FROM `FavoritesInfo` WHERE `FavoritesInfoId`=@FavoritesInfoId";
        public const string DELETEALL = "DELETE FROM `FavoritesInfo`";
        public const string DELETEWHERE = "DELETE FROM `FavoritesInfo` WHERE {0}";
        public const string SELECT = "SELECT `FavoritesInfoId`, `FavoritesCategoryId`, `ArticleInfoId`, `Name` FROM `FavoritesInfo` WHERE {0} ORDER BY {1}";
        public const string SELECTALL = "SELECT `FavoritesInfoId`, `FavoritesCategoryId`, `ArticleInfoId`, `Name` FROM `FavoritesInfo` ORDER BY {0}";
        public const string GET = "SELECT `FavoritesInfoId`, `FavoritesCategoryId`, `ArticleInfoId`, `Name` FROM `FavoritesInfo` WHERE `FavoritesInfoId`=@FavoritesInfoId";
        public const string COUNTALL = "SELECT COUNT(-1) FROM `FavoritesInfo`";
        public const string COUNT = "SELECT COUNT(-1) FROM `FavoritesInfo` WHERE {0}";
        #endregion
        
        #region 构造函数
        public FavoritesInfoDAL()
        { }
        #endregion
        
        #region 方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="favoritesInfo"></param>
        /// <returns>受影响行数</returns>
        public int Insert(FavoritesInfo favoritesInfo)
        {
            int result = 0;
            if(favoritesInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramFavoritesInfoId = new MySqlParameter("@FavoritesInfoId", favoritesInfo.FavoritesInfoId);
            MySqlParameter paramFavoritesCategoryId = new MySqlParameter("@FavoritesCategoryId", favoritesInfo.FavoritesCategoryId);
            MySqlParameter paramArticleInfoId = new MySqlParameter("@ArticleInfoId", favoritesInfo.ArticleInfoId);
            MySqlParameter paramName = new MySqlParameter("@Name", favoritesInfo.Name);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(INSERT, paramFavoritesInfoId, paramFavoritesCategoryId, paramArticleInfoId, paramName);
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
        /// <param name="IList<favoritesInfo>"></param>
        /// <returns>受影响行数</returns>
        public int Insert(IList<FavoritesInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (FavoritesInfo favoritesInfo in lists)
                {
                    MySqlParameter paramFavoritesInfoId = new MySqlParameter("@FavoritesInfoId", favoritesInfo.FavoritesInfoId);
                    MySqlParameter paramFavoritesCategoryId = new MySqlParameter("@FavoritesCategoryId", favoritesInfo.FavoritesCategoryId);
                    MySqlParameter paramArticleInfoId = new MySqlParameter("@ArticleInfoId", favoritesInfo.ArticleInfoId);
                    MySqlParameter paramName = new MySqlParameter("@Name", favoritesInfo.Name);
                    
                    result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramFavoritesInfoId, paramFavoritesCategoryId, paramArticleInfoId, paramName);
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
        /// <param name="favoritesInfo"></param>
        /// <returns></returns>
        public int InsertOrUpdate(FavoritesInfo favoritesInfo)
        {
            int result = 0;
            if (Count(string.Format("`FavoritesInfoId`='{0}'", favoritesInfo.FavoritesInfoId)) == 0)
            {
                result = Insert(favoritesInfo);
            }
            else
            {
                result = Update(favoritesInfo);
            }
            return result;
        }
        
        /// <summary>
        /// 批量增加或更新对象
        /// </summary>
        /// <param name="IList<favoritesInfo>"></param>
        /// <returns>受影响行数</returns>
        public int InsertOrUpdate(IList<FavoritesInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (FavoritesInfo favoritesInfo in lists)
                {
                    if (Count(string.Format("`FavoritesInfoId`='{0}'", favoritesInfo.FavoritesInfoId)) == 0)
                    {
                        MySqlParameter paramFavoritesInfoId = new MySqlParameter("@FavoritesInfoId", favoritesInfo.FavoritesInfoId);
                        MySqlParameter paramFavoritesCategoryId = new MySqlParameter("@FavoritesCategoryId", favoritesInfo.FavoritesCategoryId);
                        MySqlParameter paramArticleInfoId = new MySqlParameter("@ArticleInfoId", favoritesInfo.ArticleInfoId);
                        MySqlParameter paramName = new MySqlParameter("@Name", favoritesInfo.Name);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramFavoritesInfoId, paramFavoritesCategoryId, paramArticleInfoId, paramName);
                    }
                    else
                    {
                        MySqlParameter paramFavoritesInfoId = new MySqlParameter("@FavoritesInfoId", favoritesInfo.FavoritesInfoId);
                        MySqlParameter paramFavoritesCategoryId = new MySqlParameter("@FavoritesCategoryId", favoritesInfo.FavoritesCategoryId);
                        MySqlParameter paramArticleInfoId = new MySqlParameter("@ArticleInfoId", favoritesInfo.ArticleInfoId);
                        MySqlParameter paramName = new MySqlParameter("@Name", favoritesInfo.Name);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, UPDATE, paramFavoritesInfoId, paramFavoritesCategoryId, paramArticleInfoId, paramName);
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
        public int Update(FavoritesInfo favoritesInfo)
        {
            int result = 0;
            if(favoritesInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramFavoritesInfoId = new MySqlParameter("@FavoritesInfoId", favoritesInfo.FavoritesInfoId);
            MySqlParameter paramFavoritesCategoryId = new MySqlParameter("@FavoritesCategoryId", favoritesInfo.FavoritesCategoryId);
            MySqlParameter paramArticleInfoId = new MySqlParameter("@ArticleInfoId", favoritesInfo.ArticleInfoId);
            MySqlParameter paramName = new MySqlParameter("@Name", favoritesInfo.Name);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(UPDATE,paramFavoritesInfoId, paramFavoritesCategoryId, paramArticleInfoId, paramName);
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
        public int Delete(string favoritesInfoId)
        {
            int result = 0;
            
            MySqlParameter paramFavoritesInfoId = new MySqlParameter("@FavoritesInfoId", favoritesInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(DELETE, paramFavoritesInfoId);
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
        public int Delete(IList<string> favoritesInfoIdLists)
        {
             int result = 0;
            if (favoritesInfoIdLists == null || favoritesInfoIdLists.Count == 0)
                return result;

            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            try
            {
                foreach (string favoritesInfoId in favoritesInfoIdLists)
                {
                    MySqlParameter paramFavoritesInfoId = new MySqlParameter("@FavoritesInfoId", favoritesInfoId);
                    result += MySqlHelper.ExecuteNonQuery(DELETE, paramFavoritesInfoId);
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
        public List<FavoritesInfo> Select(string where, string order = "")
        {
            List<FavoritesInfo> result = new List<FavoritesInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`FavoritesInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<FavoritesInfo>(sql);
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
        public List<FavoritesInfo> Select(string where, int offset, int limit, string order = "")
        {
            List<FavoritesInfo> result = new List<FavoritesInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`FavoritesInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<FavoritesInfo>(sql);
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
        public List<FavoritesInfo> SelectAll(string order = "")
        {
            List<FavoritesInfo> result = new List<FavoritesInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`FavoritesInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<FavoritesInfo>(sql);
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
        public List<FavoritesInfo> SelectAll(int offset, int limit, string order = "")
        {
            List<FavoritesInfo> result = new List<FavoritesInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`FavoritesInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<FavoritesInfo>(sql);
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
        /// <param name="favoritesInfoId">favoritesInfoId</param>
        /// <returns>查询的结果</returns>
        public FavoritesInfo Get(string favoritesInfoId)
        {
            FavoritesInfo result = null;
            
            MySqlParameter paramFavoritesInfoId = new MySqlParameter("@FavoritesInfoId", favoritesInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteEntity<FavoritesInfo>(GET, paramFavoritesInfoId);
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