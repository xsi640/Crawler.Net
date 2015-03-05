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
    /// AdminInfo数据库操作类
    /// </summary>
    public class AdminInfoDAL : IAdminInfoDAL
    {
        #region 常量
        public const string INSERT = "INSERT INTO `AdminInfo`(`AdminInfoId`, `LoginName`, `LoginPwd`, `LoginSalt`, `LastLoginIP`, `LastLoginTime`) VALUES(@AdminInfoId, @LoginName, @LoginPwd, @LoginSalt, @LastLoginIP, @LastLoginTime)";
        public const string UPDATE = "UPDATE `AdminInfo` SET `LoginName`=@LoginName ,`LoginPwd`=@LoginPwd ,`LoginSalt`=@LoginSalt ,`LastLoginIP`=@LastLoginIP ,`LastLoginTime`=@LastLoginTime WHERE `AdminInfoId`=@AdminInfoId";
        public const string DELETE = "DELETE FROM `AdminInfo` WHERE `AdminInfoId`=@AdminInfoId";
        public const string DELETEALL = "DELETE FROM `AdminInfo`";
        public const string DELETEWHERE = "DELETE FROM `AdminInfo` WHERE {0}";
        public const string SELECT = "SELECT `AdminInfoId`, `LoginName`, `LoginPwd`, `LoginSalt`, `LastLoginIP`, `LastLoginTime` FROM `AdminInfo` WHERE {0} ORDER BY {1}";
        public const string SELECTALL = "SELECT `AdminInfoId`, `LoginName`, `LoginPwd`, `LoginSalt`, `LastLoginIP`, `LastLoginTime` FROM `AdminInfo` ORDER BY {0}";
        public const string GET = "SELECT `AdminInfoId`, `LoginName`, `LoginPwd`, `LoginSalt`, `LastLoginIP`, `LastLoginTime` FROM `AdminInfo` WHERE `AdminInfoId`=@AdminInfoId";
        public const string COUNTALL = "SELECT COUNT(-1) FROM `AdminInfo`";
        public const string COUNT = "SELECT COUNT(-1) FROM `AdminInfo` WHERE {0}";
        #endregion
        
        #region 构造函数
        public AdminInfoDAL()
        { }
        #endregion
        
        #region 方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="adminInfo"></param>
        /// <returns>受影响行数</returns>
        public int Insert(AdminInfo adminInfo)
        {
            int result = 0;
            if(adminInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramAdminInfoId = new MySqlParameter("@AdminInfoId", adminInfo.AdminInfoId);
            MySqlParameter paramLoginName = new MySqlParameter("@LoginName", adminInfo.LoginName);
            MySqlParameter paramLoginPwd = new MySqlParameter("@LoginPwd", adminInfo.LoginPwd);
            MySqlParameter paramLoginSalt = new MySqlParameter("@LoginSalt", adminInfo.LoginSalt);
            MySqlParameter paramLastLoginIP = new MySqlParameter("@LastLoginIP", adminInfo.LastLoginIP);
            MySqlParameter paramLastLoginTime = new MySqlParameter("@LastLoginTime", adminInfo.LastLoginTime);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(INSERT, paramAdminInfoId, paramLoginName, paramLoginPwd, paramLoginSalt, paramLastLoginIP, paramLastLoginTime);
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
        /// <param name="IList<adminInfo>"></param>
        /// <returns>受影响行数</returns>
        public int Insert(IList<AdminInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (AdminInfo adminInfo in lists)
                {
                    MySqlParameter paramAdminInfoId = new MySqlParameter("@AdminInfoId", adminInfo.AdminInfoId);
                    MySqlParameter paramLoginName = new MySqlParameter("@LoginName", adminInfo.LoginName);
                    MySqlParameter paramLoginPwd = new MySqlParameter("@LoginPwd", adminInfo.LoginPwd);
                    MySqlParameter paramLoginSalt = new MySqlParameter("@LoginSalt", adminInfo.LoginSalt);
                    MySqlParameter paramLastLoginIP = new MySqlParameter("@LastLoginIP", adminInfo.LastLoginIP);
                    MySqlParameter paramLastLoginTime = new MySqlParameter("@LastLoginTime", adminInfo.LastLoginTime);
                    
                    result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramAdminInfoId, paramLoginName, paramLoginPwd, paramLoginSalt, paramLastLoginIP, paramLastLoginTime);
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
        /// <param name="adminInfo"></param>
        /// <returns></returns>
        public int InsertOrUpdate(AdminInfo adminInfo)
        {
            int result = 0;
            if (Count(string.Format("`AdminInfoId`='{0}'", adminInfo.AdminInfoId)) == 0)
            {
                result = Insert(adminInfo);
            }
            else
            {
                result = Update(adminInfo);
            }
            return result;
        }
        
        /// <summary>
        /// 批量增加或更新对象
        /// </summary>
        /// <param name="IList<adminInfo>"></param>
        /// <returns>受影响行数</returns>
        public int InsertOrUpdate(IList<AdminInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (AdminInfo adminInfo in lists)
                {
                    if (Count(string.Format("`AdminInfoId`='{0}'", adminInfo.AdminInfoId)) == 0)
                    {
                        MySqlParameter paramAdminInfoId = new MySqlParameter("@AdminInfoId", adminInfo.AdminInfoId);
                        MySqlParameter paramLoginName = new MySqlParameter("@LoginName", adminInfo.LoginName);
                        MySqlParameter paramLoginPwd = new MySqlParameter("@LoginPwd", adminInfo.LoginPwd);
                        MySqlParameter paramLoginSalt = new MySqlParameter("@LoginSalt", adminInfo.LoginSalt);
                        MySqlParameter paramLastLoginIP = new MySqlParameter("@LastLoginIP", adminInfo.LastLoginIP);
                        MySqlParameter paramLastLoginTime = new MySqlParameter("@LastLoginTime", adminInfo.LastLoginTime);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramAdminInfoId, paramLoginName, paramLoginPwd, paramLoginSalt, paramLastLoginIP, paramLastLoginTime);
                    }
                    else
                    {
                        MySqlParameter paramAdminInfoId = new MySqlParameter("@AdminInfoId", adminInfo.AdminInfoId);
                        MySqlParameter paramLoginName = new MySqlParameter("@LoginName", adminInfo.LoginName);
                        MySqlParameter paramLoginPwd = new MySqlParameter("@LoginPwd", adminInfo.LoginPwd);
                        MySqlParameter paramLoginSalt = new MySqlParameter("@LoginSalt", adminInfo.LoginSalt);
                        MySqlParameter paramLastLoginIP = new MySqlParameter("@LastLoginIP", adminInfo.LastLoginIP);
                        MySqlParameter paramLastLoginTime = new MySqlParameter("@LastLoginTime", adminInfo.LastLoginTime);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, UPDATE, paramAdminInfoId, paramLoginName, paramLoginPwd, paramLoginSalt, paramLastLoginIP, paramLastLoginTime);
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
        public int Update(AdminInfo adminInfo)
        {
            int result = 0;
            if(adminInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramAdminInfoId = new MySqlParameter("@AdminInfoId", adminInfo.AdminInfoId);
            MySqlParameter paramLoginName = new MySqlParameter("@LoginName", adminInfo.LoginName);
            MySqlParameter paramLoginPwd = new MySqlParameter("@LoginPwd", adminInfo.LoginPwd);
            MySqlParameter paramLoginSalt = new MySqlParameter("@LoginSalt", adminInfo.LoginSalt);
            MySqlParameter paramLastLoginIP = new MySqlParameter("@LastLoginIP", adminInfo.LastLoginIP);
            MySqlParameter paramLastLoginTime = new MySqlParameter("@LastLoginTime", adminInfo.LastLoginTime);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(UPDATE,paramAdminInfoId, paramLoginName, paramLoginPwd, paramLoginSalt, paramLastLoginIP, paramLastLoginTime);
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
        public int Delete(string adminInfoId)
        {
            int result = 0;
            
            MySqlParameter paramAdminInfoId = new MySqlParameter("@AdminInfoId", adminInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(DELETE, paramAdminInfoId);
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
        public int Delete(IList<string> adminInfoIdLists)
        {
             int result = 0;
            if (adminInfoIdLists == null || adminInfoIdLists.Count == 0)
                return result;

            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            try
            {
                foreach (string adminInfoId in adminInfoIdLists)
                {
                    MySqlParameter paramAdminInfoId = new MySqlParameter("@AdminInfoId", adminInfoId);
                    result += MySqlHelper.ExecuteNonQuery(DELETE, paramAdminInfoId);
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
        public List<AdminInfo> Select(string where, string order = "")
        {
            List<AdminInfo> result = new List<AdminInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`AdminInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<AdminInfo>(sql);
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
        public List<AdminInfo> Select(string where, int offset, int limit, string order = "")
        {
            List<AdminInfo> result = new List<AdminInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`AdminInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<AdminInfo>(sql);
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
        public List<AdminInfo> SelectAll(string order = "")
        {
            List<AdminInfo> result = new List<AdminInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`AdminInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<AdminInfo>(sql);
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
        public List<AdminInfo> SelectAll(int offset, int limit, string order = "")
        {
            List<AdminInfo> result = new List<AdminInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`AdminInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<AdminInfo>(sql);
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
        /// <param name="adminInfoId">adminInfoId</param>
        /// <returns>查询的结果</returns>
        public AdminInfo Get(string adminInfoId)
        {
            AdminInfo result = null;
            
            MySqlParameter paramAdminInfoId = new MySqlParameter("@AdminInfoId", adminInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteEntity<AdminInfo>(GET, paramAdminInfoId);
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