using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoReader.DBAccess.Entities
{
    /// <summary>
    /// 
    /// </summary>
	[Serializable]
	public class AdminInfo
	{
		#region 变量定义
        private string _AdminInfoId = string.Empty;
        private string _LoginName = string.Empty;
        private string _LoginPwd = string.Empty;
        private string _LoginSalt = string.Empty;
        private string _LastLoginIP = string.Empty;
        private DateTime _LastLoginTime = new DateTime(1900,1,1);
        
        #endregion
        
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public AdminInfo()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        public AdminInfo
        (
			string adminInfoId,
			string loginName,
			string loginPwd,
			string loginSalt,
			string lastLoginIP,
			DateTime lastLoginTime
        )
        {
            this._AdminInfoId = adminInfoId;
            this._LoginName = loginName;
            this._LoginPwd = loginPwd;
            this._LoginSalt = loginSalt;
            this._LastLoginIP = lastLoginIP;
            this._LastLoginTime = lastLoginTime;
        }
        #endregion
        
        #region 公共属性
		///<summary>
		///
		///</summary>
		public string AdminInfoId
		{
			get{ return this._AdminInfoId; }
			set{ this._AdminInfoId = value; }
		}
		///<summary>
		///
		///</summary>
		public string LoginName
		{
			get{ return this._LoginName; }
			set{ this._LoginName = value; }
		}
		///<summary>
		///
		///</summary>
		public string LoginPwd
		{
			get{ return this._LoginPwd; }
			set{ this._LoginPwd = value; }
		}
		///<summary>
		///
		///</summary>
		public string LoginSalt
		{
			get{ return this._LoginSalt; }
			set{ this._LoginSalt = value; }
		}
		///<summary>
		///
		///</summary>
		public string LastLoginIP
		{
			get{ return this._LastLoginIP; }
			set{ this._LastLoginIP = value; }
		}
		///<summary>
		///
		///</summary>
		public DateTime LastLoginTime
		{
			get{ return this._LastLoginTime; }
			set{ this._LastLoginTime = value; }
		}

        #endregion
        
        #region 方法
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is AdminInfo)
            {
                result = (obj as AdminInfo).AdminInfoId == this.AdminInfoId;
            }
            return result;
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
	}
}