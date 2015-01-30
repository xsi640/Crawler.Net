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
    /// Capture数据库操作类
    /// </summary>
    public class CaptureDAL : ICaptureDAL
    {
        #region 常量
        public const string INSERT = "INSERT INTO `Capture`(`CaptureId`, `Url`, `Status`, `Html`) VALUES(@CaptureId, @Url, @Status, @Html)";
        public const string UPDATE = "UPDATE `Capture` SET `Url`=@Url ,`Status`=@Status ,`Html`=@Html WHERE CaptureId=@CaptureId";
        public const string DELETE = "DELETE FROM `Capture` WHERE CaptureId=@CaptureId";
        public const string DELETEALL = "DELETE FROM `Capture`";
        public const string DELETEWHERE = "DELETE FROM `Capture` WHERE {0}";
        public const string SELECT = "SELECT `CaptureId`, `Url`, `Status`, `Html` FROM `Capture` WHERE {0} ORDER BY {1}";
        public const string SELECTALL = "SELECT `CaptureId`, `Url`, `Status`, `Html` FROM `Capture` ORDER BY {0}";
        public const string GET = "SELECT `CaptureId`, `Url`, `Status`, `Html` FROM `Capture` WHERE CaptureId=@CaptureId";
        public const string COUNTALL = "SELECT COUNT(-1) FROM `Capture`";
        public const string COUNT = "SELECT COUNT(-1) FROM `Capture` WHERE {0}";
        #endregion
        
        #region 构造函数
        public CaptureDAL()
        { }
        #endregion
        
        #region 方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="capture"></param>
        /// <returns>受影响行数</returns>
        public int Insert(Capture capture)
        {
            int result = 0;
            if(capture == null)
            {
                return result;
            }
            
            MySqlParameter paramCaptureId = new MySqlParameter("@CaptureId", capture.CaptureId);
            MySqlParameter paramUrl = new MySqlParameter("@Url", capture.Url);
            MySqlParameter paramStatus = new MySqlParameter("@Status", capture.Status);
            MySqlParameter paramHtml = new MySqlParameter("@Html", capture.Html);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(INSERT, paramCaptureId, paramUrl, paramStatus, paramHtml);
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
        /// <param name="IList<capture>"></param>
        /// <returns>受影响行数</returns>
        public int Insert(IList<Capture> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (Capture capture in lists)
                {
                    MySqlParameter paramCaptureId = new MySqlParameter("@CaptureId", capture.CaptureId);
                    MySqlParameter paramUrl = new MySqlParameter("@Url", capture.Url);
                    MySqlParameter paramStatus = new MySqlParameter("@Status", capture.Status);
                    MySqlParameter paramHtml = new MySqlParameter("@Html", capture.Html);
                    
                    result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramCaptureId, paramUrl, paramStatus, paramHtml);
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
        /// <param name="capture"></param>
        /// <returns></returns>
        public int InsertOrUpdate(Capture capture)
        {
            int result = 0;
            if (Count(string.Format("`CaptureId`='{0}'", capture.CaptureId)) == 0)
            {
                result = Insert(capture);
            }
            else
            {
                result = Update(capture);
            }
            return result;
        }
        
        /// <summary>
        /// 批量增加或更新对象
        /// </summary>
        /// <param name="IList<capture>"></param>
        /// <returns>受影响行数</returns>
        public int InsertOrUpdate(IList<Capture> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (Capture capture in lists)
                {
                    if (Count(string.Format("`CaptureId`='{0}'", capture.CaptureId)) == 0)
                    {
                        MySqlParameter paramCaptureId = new MySqlParameter("@CaptureId", capture.CaptureId);
                        MySqlParameter paramUrl = new MySqlParameter("@Url", capture.Url);
                        MySqlParameter paramStatus = new MySqlParameter("@Status", capture.Status);
                        MySqlParameter paramHtml = new MySqlParameter("@Html", capture.Html);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramCaptureId, paramUrl, paramStatus, paramHtml);
                    }
                    else
                    {
                        MySqlParameter paramCaptureId = new MySqlParameter("@CaptureId", capture.CaptureId);
                        MySqlParameter paramUrl = new MySqlParameter("@Url", capture.Url);
                        MySqlParameter paramStatus = new MySqlParameter("@Status", capture.Status);
                        MySqlParameter paramHtml = new MySqlParameter("@Html", capture.Html);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, UPDATE, paramCaptureId, paramUrl, paramStatus, paramHtml);
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
        public int Update(Capture capture)
        {
            int result = 0;
            if(capture == null)
            {
                return result;
            }
            
            MySqlParameter paramCaptureId = new MySqlParameter("@CaptureId", capture.CaptureId);
            MySqlParameter paramUrl = new MySqlParameter("@Url", capture.Url);
            MySqlParameter paramStatus = new MySqlParameter("@Status", capture.Status);
            MySqlParameter paramHtml = new MySqlParameter("@Html", capture.Html);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(UPDATE,paramCaptureId, paramUrl, paramStatus, paramHtml);
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
        public int Delete(long captureId)
        {
            int result = 0;
            
            MySqlParameter paramCaptureId = new MySqlParameter("@CaptureId", captureId);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(DELETE, paramCaptureId);
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
        public int Delete(IList<long> captureIdLists)
        {
             int result = 0;
            if (captureIdLists == null || captureIdLists.Count == 0)
                return result;

            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            try
            {
                foreach (long captureId in captureIdLists)
                {
                    MySqlParameter paramCaptureId = new MySqlParameter("@CaptureId", captureId);
                    result += MySqlHelper.ExecuteNonQuery(DELETE, paramCaptureId);
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
        public List<Capture> Select(string where, string order = "")
        {
            List<Capture> result = new List<Capture>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`CaptureId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<Capture>(sql);
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
        public List<Capture> SelectAll(string order = "")
        {
            List<Capture> result = new List<Capture>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`CaptureId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<Capture>(sql);
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
        /// <param name="captureId">captureId</param>
        /// <returns>查询的结果</returns>
        public Capture Get(long captureId)
        {
            Capture result = null;
            
            MySqlParameter paramCaptureId = new MySqlParameter("@CaptureId", captureId);
            
            try
            {
                result = MySqlHelper.ExecuteEntity<Capture>(GET, paramCaptureId);
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