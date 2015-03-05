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
    /// UserInfo数据库操作类
    /// </summary>
    public class UserInfoDAL : IUserInfoDAL
    {
        #region 常量
        public const string INSERT = "INSERT INTO `UserInfo`(`UserInfoId`, `LoginName`, `LoginPwd`, `LoginSalt`, `Mail`, `RegIP`, `LastLoginIP`, `LastLoginTime`, `CreateTime`, `Status`) VALUES(@UserInfoId, @LoginName, @LoginPwd, @LoginSalt, @Mail, @RegIP, @LastLoginIP, @LastLoginTime, @CreateTime, @Status)";
        public const string UPDATE = "UPDATE `UserInfo` SET `LoginName`=@LoginName ,`LoginPwd`=@LoginPwd ,`LoginSalt`=@LoginSalt ,`Mail`=@Mail ,`RegIP`=@RegIP ,`LastLoginIP`=@LastLoginIP ,`LastLoginTime`=@LastLoginTime ,`CreateTime`=@CreateTime ,`Status`=@Status WHERE `UserInfoId`=@UserInfoId";
        public const string DELETE = "DELETE FROM `UserInfo` WHERE `UserInfoId`=@UserInfoId";
        public const string DELETEALL = "DELETE FROM `UserInfo`";
        public const string DELETEWHERE = "DELETE FROM `UserInfo` WHERE {0}";
        public const string SELECT = "SELECT `UserInfoId`, `LoginName`, `LoginPwd`, `LoginSalt`, `Mail`, `RegIP`, `LastLoginIP`, `LastLoginTime`, `CreateTime`, `Status` FROM `UserInfo` WHERE {0} ORDER BY {1}";
        public const string SELECTALL = "SELECT `UserInfoId`, `LoginName`, `LoginPwd`, `LoginSalt`, `Mail`, `RegIP`, `LastLoginIP`, `LastLoginTime`, `CreateTime`, `Status` FROM `UserInfo` ORDER BY {0}";
        public const string GET = "SELECT `UserInfoId`, `LoginName`, `LoginPwd`, `LoginSalt`, `Mail`, `RegIP`, `LastLoginIP`, `LastLoginTime`, `CreateTime`, `Status` FROM `UserInfo` WHERE `UserInfoId`=@UserInfoId";
        public const string COUNTALL = "SELECT COUNT(-1) FROM `UserInfo`";
        public const string COUNT = "SELECT COUNT(-1) FROM `UserInfo` WHERE {0}";
        #endregion
        
        #region 构造函数
        public UserInfoDAL()
        { }
        #endregion
        
        #region 方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>受影响行数</returns>
        public int Insert(UserInfo userInfo)
        {
            int result = 0;
            if(userInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramUserInfoId = new MySqlParameter("@UserInfoId", userInfo.UserInfoId);
            MySqlParameter paramLoginName = new MySqlParameter("@LoginName", userInfo.LoginName);
            MySqlParameter paramLoginPwd = new MySqlParameter("@LoginPwd", userInfo.LoginPwd);
            MySqlParameter paramLoginSalt = new MySqlParameter("@LoginSalt", userInfo.LoginSalt);
            MySqlParameter paramMail = new MySqlParameter("@Mail", userInfo.Mail);
            MySqlParameter paramRegIP = new MySqlParameter("@RegIP", userInfo.RegIP);
            MySqlParameter paramLastLoginIP = new MySqlParameter("@LastLoginIP", userInfo.LastLoginIP);
            MySqlParameter paramLastLoginTime = new MySqlParameter("@LastLoginTime", userInfo.LastLoginTime);
            MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", userInfo.CreateTime);
            MySqlParameter paramStatus = new MySqlParameter("@Status", userInfo.Status);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(INSERT, paramUserInfoId, paramLoginName, paramLoginPwd, paramLoginSalt, paramMail, paramRegIP, paramLastLoginIP, paramLastLoginTime, paramCreateTime, paramStatus);
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
        /// <param name="IList<userInfo>"></param>
        /// <returns>受影响行数</returns>
        public int Insert(IList<UserInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (UserInfo userInfo in lists)
                {
                    MySqlParameter paramUserInfoId = new MySqlParameter("@UserInfoId", userInfo.UserInfoId);
                    MySqlParameter paramLoginName = new MySqlParameter("@LoginName", userInfo.LoginName);
                    MySqlParameter paramLoginPwd = new MySqlParameter("@LoginPwd", userInfo.LoginPwd);
                    MySqlParameter paramLoginSalt = new MySqlParameter("@LoginSalt", userInfo.LoginSalt);
                    MySqlParameter paramMail = new MySqlParameter("@Mail", userInfo.Mail);
                    MySqlParameter paramRegIP = new MySqlParameter("@RegIP", userInfo.RegIP);
                    MySqlParameter paramLastLoginIP = new MySqlParameter("@LastLoginIP", userInfo.LastLoginIP);
                    MySqlParameter paramLastLoginTime = new MySqlParameter("@LastLoginTime", userInfo.LastLoginTime);
                    MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", userInfo.CreateTime);
                    MySqlParameter paramStatus = new MySqlParameter("@Status", userInfo.Status);
                    
                    result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramUserInfoId, paramLoginName, paramLoginPwd, paramLoginSalt, paramMail, paramRegIP, paramLastLoginIP, paramLastLoginTime, paramCreateTime, paramStatus);
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
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public int InsertOrUpdate(UserInfo userInfo)
        {
            int result = 0;
            if (Count(string.Format("`UserInfoId`='{0}'", userInfo.UserInfoId)) == 0)
            {
                result = Insert(userInfo);
            }
            else
            {
                result = Update(userInfo);
            }
            return result;
        }
        
        /// <summary>
        /// 批量增加或更新对象
        /// </summary>
        /// <param name="IList<userInfo>"></param>
        /// <returns>受影响行数</returns>
        public int InsertOrUpdate(IList<UserInfo> lists)
        {
            int result = 0;
            if(lists == null || lists.Count == 0)
            {
                return result;
            }
            
            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            
            try
            {
                foreach (UserInfo userInfo in lists)
                {
                    if (Count(string.Format("`UserInfoId`='{0}'", userInfo.UserInfoId)) == 0)
                    {
                        MySqlParameter paramUserInfoId = new MySqlParameter("@UserInfoId", userInfo.UserInfoId);
                        MySqlParameter paramLoginName = new MySqlParameter("@LoginName", userInfo.LoginName);
                        MySqlParameter paramLoginPwd = new MySqlParameter("@LoginPwd", userInfo.LoginPwd);
                        MySqlParameter paramLoginSalt = new MySqlParameter("@LoginSalt", userInfo.LoginSalt);
                        MySqlParameter paramMail = new MySqlParameter("@Mail", userInfo.Mail);
                        MySqlParameter paramRegIP = new MySqlParameter("@RegIP", userInfo.RegIP);
                        MySqlParameter paramLastLoginIP = new MySqlParameter("@LastLoginIP", userInfo.LastLoginIP);
                        MySqlParameter paramLastLoginTime = new MySqlParameter("@LastLoginTime", userInfo.LastLoginTime);
                        MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", userInfo.CreateTime);
                        MySqlParameter paramStatus = new MySqlParameter("@Status", userInfo.Status);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, INSERT, paramUserInfoId, paramLoginName, paramLoginPwd, paramLoginSalt, paramMail, paramRegIP, paramLastLoginIP, paramLastLoginTime, paramCreateTime, paramStatus);
                    }
                    else
                    {
                        MySqlParameter paramUserInfoId = new MySqlParameter("@UserInfoId", userInfo.UserInfoId);
                        MySqlParameter paramLoginName = new MySqlParameter("@LoginName", userInfo.LoginName);
                        MySqlParameter paramLoginPwd = new MySqlParameter("@LoginPwd", userInfo.LoginPwd);
                        MySqlParameter paramLoginSalt = new MySqlParameter("@LoginSalt", userInfo.LoginSalt);
                        MySqlParameter paramMail = new MySqlParameter("@Mail", userInfo.Mail);
                        MySqlParameter paramRegIP = new MySqlParameter("@RegIP", userInfo.RegIP);
                        MySqlParameter paramLastLoginIP = new MySqlParameter("@LastLoginIP", userInfo.LastLoginIP);
                        MySqlParameter paramLastLoginTime = new MySqlParameter("@LastLoginTime", userInfo.LastLoginTime);
                        MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", userInfo.CreateTime);
                        MySqlParameter paramStatus = new MySqlParameter("@Status", userInfo.Status);
                        
                        result += MySqlHelper.ExecuteNonQuery(tran, UPDATE, paramUserInfoId, paramLoginName, paramLoginPwd, paramLoginSalt, paramMail, paramRegIP, paramLastLoginIP, paramLastLoginTime, paramCreateTime, paramStatus);
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
        public int Update(UserInfo userInfo)
        {
            int result = 0;
            if(userInfo == null)
            {
                return result;
            }
            
            MySqlParameter paramUserInfoId = new MySqlParameter("@UserInfoId", userInfo.UserInfoId);
            MySqlParameter paramLoginName = new MySqlParameter("@LoginName", userInfo.LoginName);
            MySqlParameter paramLoginPwd = new MySqlParameter("@LoginPwd", userInfo.LoginPwd);
            MySqlParameter paramLoginSalt = new MySqlParameter("@LoginSalt", userInfo.LoginSalt);
            MySqlParameter paramMail = new MySqlParameter("@Mail", userInfo.Mail);
            MySqlParameter paramRegIP = new MySqlParameter("@RegIP", userInfo.RegIP);
            MySqlParameter paramLastLoginIP = new MySqlParameter("@LastLoginIP", userInfo.LastLoginIP);
            MySqlParameter paramLastLoginTime = new MySqlParameter("@LastLoginTime", userInfo.LastLoginTime);
            MySqlParameter paramCreateTime = new MySqlParameter("@CreateTime", userInfo.CreateTime);
            MySqlParameter paramStatus = new MySqlParameter("@Status", userInfo.Status);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(UPDATE,paramUserInfoId, paramLoginName, paramLoginPwd, paramLoginSalt, paramMail, paramRegIP, paramLastLoginIP, paramLastLoginTime, paramCreateTime, paramStatus);
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
        public int Delete(string userInfoId)
        {
            int result = 0;
            
            MySqlParameter paramUserInfoId = new MySqlParameter("@UserInfoId", userInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteNonQuery(DELETE, paramUserInfoId);
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
        public int Delete(IList<string> userInfoIdLists)
        {
             int result = 0;
            if (userInfoIdLists == null || userInfoIdLists.Count == 0)
                return result;

            MySqlTransaction tran = MySqlHelper.MySqlConnection.BeginTransaction();
            try
            {
                foreach (string userInfoId in userInfoIdLists)
                {
                    MySqlParameter paramUserInfoId = new MySqlParameter("@UserInfoId", userInfoId);
                    result += MySqlHelper.ExecuteNonQuery(DELETE, paramUserInfoId);
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
        public List<UserInfo> Select(string where, string order = "")
        {
            List<UserInfo> result = new List<UserInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`UserInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<UserInfo>(sql);
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
        public List<UserInfo> Select(string where, int offset, int limit, string order = "")
        {
            List<UserInfo> result = new List<UserInfo>();
            if(string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }
            if(string.IsNullOrEmpty(order))
            {
                order = "`UserInfoId` DESC";    
            }
            
            string sql = string.Format(SELECT, where, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<UserInfo>(sql);
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
        public List<UserInfo> SelectAll(string order = "")
        {
            List<UserInfo> result = new List<UserInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`UserInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order);
            
            try
            {
                result = MySqlHelper.ExecuteList<UserInfo>(sql);
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
        public List<UserInfo> SelectAll(int offset, int limit, string order = "")
        {
            List<UserInfo> result = new List<UserInfo>();
            if(string.IsNullOrEmpty(order))
            {
                order = "`UserInfoId` DESC";    
            }
        
            string sql = string.Format(SELECTALL, order) + string.Format(" limit {0},{1}", offset, limit);
            
            try
            {
                result = MySqlHelper.ExecuteList<UserInfo>(sql);
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
        /// <param name="userInfoId">userInfoId</param>
        /// <returns>查询的结果</returns>
        public UserInfo Get(string userInfoId)
        {
            UserInfo result = null;
            
            MySqlParameter paramUserInfoId = new MySqlParameter("@UserInfoId", userInfoId);
            
            try
            {
                result = MySqlHelper.ExecuteEntity<UserInfo>(GET, paramUserInfoId);
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