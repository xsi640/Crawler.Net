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
	public class UserInfo
	{
		#region 变量定义
        private string _UserInfoId = string.Empty;
        private string _LoginName = string.Empty;
        private string _LoginPwd = string.Empty;
        private string _LoginSalt = string.Empty;
        private string _Mail = string.Empty;
        private string _RegIP = string.Empty;
        private string _LastLoginIP = string.Empty;
        private DateTime _LastLoginTime = new DateTime(1900,1,1);
        private DateTime _CreateTime = new DateTime(1900,1,1);
        private int _Status = 0;
        
		private IList<FavoritesCategoryInfo> _FavoritesCategoryInfo = null;
		private IList<UserCategoryInfo> _UserCategoryInfo = null;
        #endregion
        
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public UserInfo()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        public UserInfo
        (
			string userInfoId,
			string loginName,
			string loginPwd,
			string loginSalt,
			string mail,
			string regIP,
			string lastLoginIP,
			DateTime lastLoginTime,
			DateTime createTime,
			int status
        )
        {
            this._UserInfoId = userInfoId;
            this._LoginName = loginName;
            this._LoginPwd = loginPwd;
            this._LoginSalt = loginSalt;
            this._Mail = mail;
            this._RegIP = regIP;
            this._LastLoginIP = lastLoginIP;
            this._LastLoginTime = lastLoginTime;
            this._CreateTime = createTime;
            this._Status = status;
        }
        #endregion
        
        #region 公共属性
		///<summary>
		///
		///</summary>
		public string UserInfoId
		{
			get{ return this._UserInfoId; }
			set{ this._UserInfoId = value; }
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
		public string Mail
		{
			get{ return this._Mail; }
			set{ this._Mail = value; }
		}
		///<summary>
		///
		///</summary>
		public string RegIP
		{
			get{ return this._RegIP; }
			set{ this._RegIP = value; }
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
		///<summary>
		///
		///</summary>
		public DateTime CreateTime
		{
			get{ return this._CreateTime; }
			set{ this._CreateTime = value; }
		}
		///<summary>
		///
		///</summary>
		public int Status
		{
			get{ return this._Status; }
			set{ this._Status = value; }
		}

		///<summary>
		///
		///</summary>
		public IList<FavoritesCategoryInfo> FavoritesCategoryInfo
		{
			get{ return this._FavoritesCategoryInfo; }
			set{ this._FavoritesCategoryInfo = value; }
		}
		///<summary>
		///
		///</summary>
		public IList<UserCategoryInfo> UserCategoryInfo
		{
			get{ return this._UserCategoryInfo; }
			set{ this._UserCategoryInfo = value; }
		}
        #endregion
        
        #region 方法
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is UserInfo)
            {
                result = (obj as UserInfo).UserInfoId == this.UserInfoId;
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