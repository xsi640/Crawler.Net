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
	public class FavoritesCategoryInfo
	{
		#region 变量定义
        private string _FavoritesCategoryInfoId = string.Empty;
        private string _UserId = string.Empty;
        private string _Name = string.Empty;
        
		private UserInfo _UserInfo = null;
		private IList<FavoritesInfo> _FavoritesInfo = null;
        #endregion
        
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public FavoritesCategoryInfo()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        public FavoritesCategoryInfo
        (
			string favoritesCategoryInfoId,
			string userId,
			string name
        )
        {
            this._FavoritesCategoryInfoId = favoritesCategoryInfoId;
            this._UserId = userId;
            this._Name = name;
        }
        #endregion
        
        #region 公共属性
		///<summary>
		///
		///</summary>
		public string FavoritesCategoryInfoId
		{
			get{ return this._FavoritesCategoryInfoId; }
			set{ this._FavoritesCategoryInfoId = value; }
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
		public UserInfo UserInfo
		{
			get{ return this._UserInfo; }
			set{ this._UserInfo=value; }
		}
		///<summary>
		///
		///</summary>
		public IList<FavoritesInfo> FavoritesInfo
		{
			get{ return this._FavoritesInfo; }
			set{ this._FavoritesInfo = value; }
		}
        #endregion
        
        #region 方法
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is FavoritesCategoryInfo)
            {
                result = (obj as FavoritesCategoryInfo).FavoritesCategoryInfoId == this.FavoritesCategoryInfoId;
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