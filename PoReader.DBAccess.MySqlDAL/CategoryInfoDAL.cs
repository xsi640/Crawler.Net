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
    /// CategoryInfo数据库操作类
    /// </summary>
    public class CategoryInfoDAL : ICategoryInfoDAL
    {
        #region 常量
        public const string INSERT = "INSERT INTO `CategoryInfo`(`CategoryInfoId`, `Name`, `Photo`, `ParentId`, `Count`, `Layer`, `SubCount`, `CreateTime`, `Creator`) VALUES(@CategoryInfoId, @Name, @Photo, @ParentId, @Count, @Layer, @SubCount, @CreateTime, @Creator)";
        public const string UPDATE = "UPDATE `CategoryInfo` SET `Name`=@Name ,`Photo`=@Photo ,`ParentId`=@ParentId ,`Count`=@Count ,`Layer`=@Layer ,`SubCount`=@SubCount ,`CreateTime`=@CreateTime ,`Creator`=@Creator WHERE `CategoryInfoId`=@CategoryInfoId";
        public const string DELETE = "DELETE FROM `CategoryInfo` WHERE `CategoryInfoId`=@CategoryInfoId";
        public const string DELETEALL = "DELETE FROM `CategoryInfo`";
        public const string DELETEWHERE = "DELETE FROM `CategoryInfo` WHERE {0}";
        public const string SELECT = "SELECT `CategoryInfoId`, `Name`, `Photo`, `ParentId`, `Count`, `Layer`, `SubCount`, `CreateTime`, `Creator` FROM `CategoryInfo` WHERE {0} ORDER BY {1}";
        public const string SELECTALL = "SELECT `CategoryInfoId`, `Name`, `Photo`, `ParentId`, `Count`, `Layer`, `SubCount`, `CreateTime`, `Creator` FROM `CategoryInfo` ORDER BY {0}";
        public const string GET = "SELECT `CategoryInfoId`, `Name`, `Photo`, `ParentId`, `Count`, `Layer`, `SubCount`, `CreateTime`, `Creator` FROM `CategoryInfo` WHERE `CategoryInfoId`=@CategoryInfoId";
        public const string COUNTALL = "SELECT COUNT(-1) FROM `CategoryInfo`";
        public const string COUNT = "SELECT COUNT(-1) FROM `CategoryInfo` WHERE {0}";
        #endregion
        
        #region 构造函数
        public CategoryInfoDAL()
        { }
        #endregion
        
        #region 方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="categoryInfo"></param>
        /// <returns>受影响行数</returns>
        public int Insert(CategoryInfo categoryInfo)
        {
            int result = 0;
            if(categoryInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramCategoryInfoId = new MySqlParameter("@CategoryInfoId", categoryInfo.CategoryInfoId);
            MySqlParameter paramName = new MySqlParameter("@Name", categoryInfo.Name);
            MySqlParameter paramPhoto = new MySqlParameter("@Photo", categoryInfo.Photo);
            MySqlParameter paramParentId = new MySqlParameter("@ParentId", categoryInfo.ParentId);
            MySqlParameter paramCount = new MySqlParameter("@Count", categoryInfo.Count);
            MySqlParameter paramLayer = new MySqlParameter("@Layer", categoryInfo.Layer);
            MySqlParameter paramSubCount = new MySqlParameter("@SubCount", categoryInfo.SubCount);
            MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", categoryInfo.CreateTime);
            MySqlParameter paramCreator = new MySqlParameter("@Creator", categoryInfo.Creator);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(INSERT, paramCategoryInfoId, paramName, paramPhoto, paramParentId, paramCount, paramLayer, paramSubCount, paramCreateTime, paramCreator);
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
        /// <param name="IList<categoryInfo>"></param>
        /// <returns>受影响行数</returns>
        public int Insert(IList<CategoryInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (CategoryInfo categoryInfo in lists)
                {
                    MySqlParameter paramCategoryInfoId = new MySqlParameter("@CategoryInfoId", categoryInfo.CategoryInfoId);
                    MySqlParameter paramName = new MySqlParameter("@Name", categoryInfo.Name);
                    MySqlParameter paramPhoto = new MySqlParameter("@Photo", categoryInfo.Photo);
                    MySqlParameter paramParentId = new MySqlParameter("@ParentId", categoryInfo.ParentId);
                    MySqlParameter paramCount = new MySqlParameter("@Count", categoryInfo.Count);
                    MySqlParameter paramLayer = new MySqlParameter("@Layer", categoryInfo.Layer);
                    MySqlParameter paramSubCount = new MySqlParameter("@SubCount", categoryInfo.SubCount);
                    MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", categoryInfo.CreateTime);
                    MySqlParameter paramCreator = new MySqlParameter("@Creator", categoryInfo.Creator);
                    
                    result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramCategoryInfoId, paramName, paramPhoto, paramParentId, paramCount, paramLayer, paramSubCount, paramCreateTime, paramCreator);
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
        /// <param name="categoryInfo"></param>
        /// <returns></returns>
        public int InsertOrUpdate(CategoryInfo categoryInfo)
        {
            int result = 0;
            if (Count(string.Format("`CategoryInfoId`='{0}'", categoryInfo.CategoryInfoId)) == 0)
            {
                result = Insert(categoryInfo);
            }
            else
            {
                result = Update(categoryInfo);
            }
            return result;
        }
        
        /// <summary>
        /// 批量增加或更新对象
        /// </summary>
        /// <param name="IList<categoryInfo>"></param>
        /// <returns>受影响行数</returns>
        public int InsertOrUpdate(IList<CategoryInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (CategoryInfo categoryInfo in lists)
                {
                    if (Count(string.Format("`CategoryInfoId`='{0}'", categoryInfo.CategoryInfoId)) == 0)
                    {
                        MySqlParameter paramCategoryInfoId = new MySqlParameter("@CategoryInfoId", categoryInfo.CategoryInfoId);
                        MySqlParameter paramName = new MySqlParameter("@Name", categoryInfo.Name);
                        MySqlParameter paramPhoto = new MySqlParameter("@Photo", categoryInfo.Photo);
                        MySqlParameter paramParentId = new MySqlParameter("@ParentId", categoryInfo.ParentId);
                        MySqlParameter paramCount = new MySqlParameter("@Count", categoryInfo.Count);
                        MySqlParameter paramLayer = new MySqlParameter("@Layer", categoryInfo.Layer);
                        MySqlParameter paramSubCount = new MySqlParameter("@SubCount", categoryInfo.SubCount);
                        MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", categoryInfo.CreateTime);
                        MySqlParameter paramCreator = new MySqlParameter("@Creator", categoryInfo.Creator);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramCategoryInfoId, paramName, paramPhoto, paramParentId, paramCount, paramLayer, paramSubCount, paramCreateTime, paramCreator);
                    }
                    else
                    {
                        MySqlParameter paramCategoryInfoId = new MySqlParameter("@CategoryInfoId", categoryInfo.CategoryInfoId);
                        MySqlParameter paramName = new MySqlParameter("@Name", categoryInfo.Name);
                        MySqlParameter paramPhoto = new MySqlParameter("@Photo", categoryInfo.Photo);
                        MySqlParameter paramParentId = new MySqlParameter("@ParentId", categoryInfo.ParentId);
                        MySqlParameter paramCount = new MySqlParameter("@Count", categoryInfo.Count);
                        MySqlParameter paramLayer = new MySqlParameter("@Layer", categoryInfo.Layer);
                        MySqlParameter paramSubCount = new MySqlParameter("@SubCount", categoryInfo.SubCount);
                        MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", categoryInfo.CreateTime);
                        MySqlParameter paramCreator = new MySqlParameter("@Creator", categoryInfo.Creator);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, UPDATE, paramCategoryInfoId, paramName, paramPhoto, paramParentId, paramCount, paramLayer, paramSubCount, paramCreateTime, paramCreator);
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
        public int Update(CategoryInfo categoryInfo)
        {
            int result = 0;
            if(categoryInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramCategoryInfoId = new MySqlParameter("@CategoryInfoId", categoryInfo.CategoryInfoId);
            MySqlParameter paramName = new MySqlParameter("@Name", categoryInfo.Name);
            MySqlParameter paramPhoto = new MySqlParameter("@Photo", categoryInfo.Photo);
            MySqlParameter paramParentId = new MySqlParameter("@ParentId", categoryInfo.ParentId);
            MySqlParameter paramCount = new MySqlParameter("@Count", categoryInfo.Count);
            MySqlParameter paramLayer = new MySqlParameter("@Layer", categoryInfo.Layer);
            MySqlParameter paramSubCount = new MySqlParameter("@SubCount", categoryInfo.SubCount);
            MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", categoryInfo.CreateTime);
            MySqlParameter paramCreator = new MySqlParameter("@Creator", categoryInfo.Creator);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(UPDATE,paramCategoryInfoId, paramName, paramPhoto, paramParentId, paramCount, paramLayer, paramSubCount, paramCreateTime, paramCreator);
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
        public int Delete(string categoryInfoId)
        {
            int result = 0;
            
            MySqlParameter paramCategoryInfoId = new MySqlParameter("@CategoryInfoId", categoryInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(DELETE, paramCategoryInfoId);
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
        public int Delete(IList<string> categoryInfoIdLists)
        {
             int result = 0;
            if (categoryInfoIdLists == null || categoryInfoIdLists.Count == 0)
                return result;

            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            try
            {
                foreach (string categoryInfoId in categoryInfoIdLists)
                {
                    MySqlParameter paramCategoryInfoId = new MySqlParameter("@CategoryInfoId", categoryInfoId);
                    result += MySqlHelper.ExecuteNonQuery(DELETE, paramCategoryInfoId);
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
        public List<CategoryInfo> Select(string where, string order = "")
        {
            List<CategoryInfo> result = new List<CategoryInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`CategoryInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<CategoryInfo>(sql);
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
        public List<CategoryInfo> Select(string where, int offset, int limit, string order = "")
        {
            List<CategoryInfo> result = new List<CategoryInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`CategoryInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<CategoryInfo>(sql);
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
        public List<CategoryInfo> SelectAll(string order = "")
        {
            List<CategoryInfo> result = new List<CategoryInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`CategoryInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<CategoryInfo>(sql);
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
        public List<CategoryInfo> SelectAll(int offset, int limit, string order = "")
        {
            List<CategoryInfo> result = new List<CategoryInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`CategoryInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<CategoryInfo>(sql);
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
        /// <param name="categoryInfoId">categoryInfoId</param>
        /// <returns>查询的结果</returns>
        public CategoryInfo Get(string categoryInfoId)
        {
            CategoryInfo result = null;
            
            MySqlParameter paramCategoryInfoId = new MySqlParameter("@CategoryInfoId", categoryInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteEntity<CategoryInfo>(GET, paramCategoryInfoId);
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