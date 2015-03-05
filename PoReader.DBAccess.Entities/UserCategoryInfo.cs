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
	public class UserCategoryInfo
	{
		#region 变量定义
        private string _UserCategoryInfoId = string.Empty;
        private string _UserId = string.Empty;
        private string _Name = string.Empty;
        private string _ParentId = string.Empty;
        private int _Count = 0;
        private int _SubCount = 0;
        private int _Layer = 0;
        private string _RssHostIds = string.Empty;
        private DateTime _CreateTime = new DateTime(1900,1,1);
        
		private UserInfo _UserInfo = null;
        #endregion
        
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public UserCategoryInfo()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        public UserCategoryInfo
        (
			string userCategoryInfoId,
			string userId,
			string name,
			string parentId,
			int count,
			int subCount,
			int layer,
			string rssHostIds,
			DateTime createTime
        )
        {
            this._UserCategoryInfoId = userCategoryInfoId;
            this._UserId = userId;
            this._Name = name;
            this._ParentId = parentId;
            this._Count = count;
            this._SubCount = subCount;
            this._Layer = layer;
            this._RssHostIds = rssHostIds;
            this._CreateTime = createTime;
        }
        #endregion
        
        #region 公共属性
		///<summary>
		///
		///</summary>
		public string UserCategoryInfoId
		{
			get{ return this._UserCategoryInfoId; }
			set{ this._UserCategoryInfoId = value; }
		}
		///<summary>
		///
		///</summary>
		public string UserId
		{
			get{ return this._UserId; }
			set{ this._UserId = value; }
		}
		///<summary>
		///
		///</summary>
		public string Name
		{
			get{ return this._Name; }
			set{ this._Name = value; }
		}
		///<summary>
		///
		///</summary>
		public string ParentId
		{
			get{ return this._ParentId; }
			set{ this._ParentId = value; }
		}
		///<summary>
		///
		///</summary>
		public int Count
		{
			get{ return this._Count; }
			set{ this._Count = value; }
		}
		///<summary>
		///
		///</summary>
		public int SubCount
		{
			get{ return this._SubCount; }
			set{ this._SubCount = value; }
		}
		///<summary>
		///
		///</summary>
		public int Layer
		{
			get{ return this._Layer; }
			set{ this._Layer = value; }
		}
		///<summary>
		///
		///</summary>
		public string RssHostIds
		{
			get{ return this._RssHostIds; }
			set{ this._RssHostIds = value; }
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
		public UserInfo UserInfo
		{
			get{ return this._UserInfo; }
			set{ this._UserInfo=value; }
		}
        #endregion
        
        #region 方法
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is UserCategoryInfo)
            {
                result = (obj as UserCategoryInfo).UserCategoryInfoId == this.UserCategoryInfoId;
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