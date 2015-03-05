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
    /// ArticleInfo数据库操作类
    /// </summary>
    public class ArticleInfoDAL : IArticleInfoDAL
    {
        #region 常量
        public const string INSERT = "INSERT INTO `ArticleInfo`(`ArticleInfoId`, `RssHostId`, `Title`, `Summary`, `Author`, `CreateTime`, `UpdateTime`, `Url`, `Content`, `Photos`) VALUES(@ArticleInfoId, @RssHostId, @Title, @Summary, @Author, @CreateTime, @UpdateTime, @Url, @Content, @Photos)";
        public const string UPDATE = "UPDATE `ArticleInfo` SET `RssHostId`=@RssHostId ,`Title`=@Title ,`Summary`=@Summary ,`Author`=@Author ,`CreateTime`=@CreateTime ,`UpdateTime`=@UpdateTime ,`Url`=@Url ,`Content`=@Content ,`Photos`=@Photos WHERE `ArticleInfoId`=@ArticleInfoId";
        public const string DELETE = "DELETE FROM `ArticleInfo` WHERE `ArticleInfoId`=@ArticleInfoId";
        public const string DELETEALL = "DELETE FROM `ArticleInfo`";
        public const string DELETEWHERE = "DELETE FROM `ArticleInfo` WHERE {0}";
        public const string SELECT = "SELECT `ArticleInfoId`, `RssHostId`, `Title`, `Summary`, `Author`, `CreateTime`, `UpdateTime`, `Url`, `Content`, `Photos` FROM `ArticleInfo` WHERE {0} ORDER BY {1}";
        public const string SELECTALL = "SELECT `ArticleInfoId`, `RssHostId`, `Title`, `Summary`, `Author`, `CreateTime`, `UpdateTime`, `Url`, `Content`, `Photos` FROM `ArticleInfo` ORDER BY {0}";
        public const string GET = "SELECT `ArticleInfoId`, `RssHostId`, `Title`, `Summary`, `Author`, `CreateTime`, `UpdateTime`, `Url`, `Content`, `Photos` FROM `ArticleInfo` WHERE `ArticleInfoId`=@ArticleInfoId";
        public const string COUNTALL = "SELECT COUNT(-1) FROM `ArticleInfo`";
        public const string COUNT = "SELECT COUNT(-1) FROM `ArticleInfo` WHERE {0}";
        #endregion
        
        #region 构造函数
        public ArticleInfoDAL()
        { }
        #endregion
        
        #region 方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="articleInfo"></param>
        /// <returns>受影响行数</returns>
        public int Insert(ArticleInfo articleInfo)
        {
            int result = 0;
            if(articleInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramArticleInfoId = new MySqlParameter("@ArticleInfoId", articleInfo.ArticleInfoId);
            MySqlParameter paramRssHostId = new MySqlParameter("@RssHostId", articleInfo.RssHostId);
            MySqlParameter paramTitle = new MySqlParameter("@Title", articleInfo.Title);
            MySqlParameter paramSummary = new MySqlParameter("@Summary", articleInfo.Summary);
            MySqlParameter paramAuthor = new MySqlParameter("@Author", articleInfo.Author);
            MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", articleInfo.CreateTime);
            MySqlParameter paramUpdateTime = new MySqlParameter("@UpdateTime", articleInfo.UpdateTime);
            MySqlParameter paramUrl = new MySqlParameter("@Url", articleInfo.Url);
            MySqlParameter paramContent = new MySqlParameter("@Content", articleInfo.Content);
            MySqlParameter paramPhotos = new MySqlParameter("@Photos", articleInfo.Photos);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(INSERT, paramArticleInfoId, paramRssHostId, paramTitle, paramSummary, paramAuthor, paramCreateTime, paramUpdateTime, paramUrl, paramContent, paramPhotos);
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
        /// <param name="IList<articleInfo>"></param>
        /// <returns>受影响行数</returns>
        public int Insert(IList<ArticleInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (ArticleInfo articleInfo in lists)
                {
                    MySqlParameter paramArticleInfoId = new MySqlParameter("@ArticleInfoId", articleInfo.ArticleInfoId);
                    MySqlParameter paramRssHostId = new MySqlParameter("@RssHostId", articleInfo.RssHostId);
                    MySqlParameter paramTitle = new MySqlParameter("@Title", articleInfo.Title);
                    MySqlParameter paramSummary = new MySqlParameter("@Summary", articleInfo.Summary);
                    MySqlParameter paramAuthor = new MySqlParameter("@Author", articleInfo.Author);
                    MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", articleInfo.CreateTime);
                    MySqlParameter paramUpdateTime = new MySqlParameter("@UpdateTime", articleInfo.UpdateTime);
                    MySqlParameter paramUrl = new MySqlParameter("@Url", articleInfo.Url);
                    MySqlParameter paramContent = new MySqlParameter("@Content", articleInfo.Content);
                    MySqlParameter paramPhotos = new MySqlParameter("@Photos", articleInfo.Photos);
                    
                    result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramArticleInfoId, paramRssHostId, paramTitle, paramSummary, paramAuthor, paramCreateTime, paramUpdateTime, paramUrl, paramContent, paramPhotos);
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
        /// <param name="articleInfo"></param>
        /// <returns></returns>
        public int InsertOrUpdate(ArticleInfo articleInfo)
        {
            int result = 0;
            if (Count(string.Format("`ArticleInfoId`='{0}'", articleInfo.ArticleInfoId)) == 0)
            {
                result = Insert(articleInfo);
            }
            else
            {
                result = Update(articleInfo);
            }
            return result;
        }
        
        /// <summary>
        /// 批量增加或更新对象
        /// </summary>
        /// <param name="IList<articleInfo>"></param>
        /// <returns>受影响行数</returns>
        public int InsertOrUpdate(IList<ArticleInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (ArticleInfo articleInfo in lists)
                {
                    if (Count(string.Format("`ArticleInfoId`='{0}'", articleInfo.ArticleInfoId)) == 0)
                    {
                        MySqlParameter paramArticleInfoId = new MySqlParameter("@ArticleInfoId", articleInfo.ArticleInfoId);
                        MySqlParameter paramRssHostId = new MySqlParameter("@RssHostId", articleInfo.RssHostId);
                        MySqlParameter paramTitle = new MySqlParameter("@Title", articleInfo.Title);
                        MySqlParameter paramSummary = new MySqlParameter("@Summary", articleInfo.Summary);
                        MySqlParameter paramAuthor = new MySqlParameter("@Author", articleInfo.Author);
                        MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", articleInfo.CreateTime);
                        MySqlParameter paramUpdateTime = new MySqlParameter("@UpdateTime", articleInfo.UpdateTime);
                        MySqlParameter paramUrl = new MySqlParameter("@Url", articleInfo.Url);
                        MySqlParameter paramContent = new MySqlParameter("@Content", articleInfo.Content);
                        MySqlParameter paramPhotos = new MySqlParameter("@Photos", articleInfo.Photos);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramArticleInfoId, paramRssHostId, paramTitle, paramSummary, paramAuthor, paramCreateTime, paramUpdateTime, paramUrl, paramContent, paramPhotos);
                    }
                    else
                    {
                        MySqlParameter paramArticleInfoId = new MySqlParameter("@ArticleInfoId", articleInfo.ArticleInfoId);
                        MySqlParameter paramRssHostId = new MySqlParameter("@RssHostId", articleInfo.RssHostId);
                        MySqlParameter paramTitle = new MySqlParameter("@Title", articleInfo.Title);
                        MySqlParameter paramSummary = new MySqlParameter("@Summary", articleInfo.Summary);
                        MySqlParameter paramAuthor = new MySqlParameter("@Author", articleInfo.Author);
                        MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", articleInfo.CreateTime);
                        MySqlParameter paramUpdateTime = new MySqlParameter("@UpdateTime", articleInfo.UpdateTime);
                        MySqlParameter paramUrl = new MySqlParameter("@Url", articleInfo.Url);
                        MySqlParameter paramContent = new MySqlParameter("@Content", articleInfo.Content);
                        MySqlParameter paramPhotos = new MySqlParameter("@Photos", articleInfo.Photos);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, UPDATE, paramArticleInfoId, paramRssHostId, paramTitle, paramSummary, paramAuthor, paramCreateTime, paramUpdateTime, paramUrl, paramContent, paramPhotos);
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
        public int Update(ArticleInfo articleInfo)
        {
            int result = 0;
            if(articleInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramArticleInfoId = new MySqlParameter("@ArticleInfoId", articleInfo.ArticleInfoId);
            MySqlParameter paramRssHostId = new MySqlParameter("@RssHostId", articleInfo.RssHostId);
            MySqlParameter paramTitle = new MySqlParameter("@Title", articleInfo.Title);
            MySqlParameter paramSummary = new MySqlParameter("@Summary", articleInfo.Summary);
            MySqlParameter paramAuthor = new MySqlParameter("@Author", articleInfo.Author);
            MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", articleInfo.CreateTime);
            MySqlParameter paramUpdateTime = new MySqlParameter("@UpdateTime", articleInfo.UpdateTime);
            MySqlParameter paramUrl = new MySqlParameter("@Url", articleInfo.Url);
            MySqlParameter paramContent = new MySqlParameter("@Content", articleInfo.Content);
            MySqlParameter paramPhotos = new MySqlParameter("@Photos", articleInfo.Photos);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(UPDATE,paramArticleInfoId, paramRssHostId, paramTitle, paramSummary, paramAuthor, paramCreateTime, paramUpdateTime, paramUrl, paramContent, paramPhotos);
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
        public int Delete(string articleInfoId)
        {
            int result = 0;
            
            MySqlParameter paramArticleInfoId = new MySqlParameter("@ArticleInfoId", articleInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(DELETE, paramArticleInfoId);
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
        public int Delete(IList<string> articleInfoIdLists)
        {
             int result = 0;
            if (articleInfoIdLists == null || articleInfoIdLists.Count == 0)
                return result;

            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            try
            {
                foreach (string articleInfoId in articleInfoIdLists)
                {
                    MySqlParameter paramArticleInfoId = new MySqlParameter("@ArticleInfoId", articleInfoId);
                    result += MySqlHelper.ExecuteNonQuery(DELETE, paramArticleInfoId);
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
        public List<ArticleInfo> Select(string where, string order = "")
        {
            List<ArticleInfo> result = new List<ArticleInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`ArticleInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<ArticleInfo>(sql);
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
        public List<ArticleInfo> Select(string where, int offset, int limit, string order = "")
        {
            List<ArticleInfo> result = new List<ArticleInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`ArticleInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<ArticleInfo>(sql);
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
        public List<ArticleInfo> SelectAll(string order = "")
        {
            List<ArticleInfo> result = new List<ArticleInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`ArticleInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<ArticleInfo>(sql);
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
        public List<ArticleInfo> SelectAll(int offset, int limit, string order = "")
        {
            List<ArticleInfo> result = new List<ArticleInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`ArticleInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<ArticleInfo>(sql);
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
        /// <param name="articleInfoId">articleInfoId</param>
        /// <returns>查询的结果</returns>
        public ArticleInfo Get(string articleInfoId)
        {
            ArticleInfo result = null;
            
            MySqlParameter paramArticleInfoId = new MySqlParameter("@ArticleInfoId", articleInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteEntity<ArticleInfo>(GET, paramArticleInfoId);
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