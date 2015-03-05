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
	public class FavoritesInfo
	{
		#region 变量定义
        private string _FavoritesInfoId = string.Empty;
        private string _FavoritesCategoryId = string.Empty;
        private string _ArticleInfoId = string.Empty;
        private string _Name = string.Empty;
        
		private FavoritesCategoryInfo _FavoritesCategoryInfo = null;
		private ArticleInfo _ArticleInfo = null;
        #endregion
        
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public FavoritesInfo()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        public FavoritesInfo
        (
			string favoritesInfoId,
			string favoritesCategoryId,
			string articleInfoId,
			string name
        )
        {
            this._FavoritesInfoId = favoritesInfoId;
            this._FavoritesCategoryId = favoritesCategoryId;
            this._ArticleInfoId = articleInfoId;
            this._Name = name;
        }
        #endregion
        
        #region 公共属性
		///<summary>
		///
		///</summary>
		public string FavoritesInfoId
		{
			get{ return this._FavoritesInfoId; }
			set{ this._FavoritesInfoId = value; }
		}
		///<summary>
		///
		///</summary>
		public string FavoritesCategoryId
		{
			get{ return this._FavoritesCategoryId; }
			set{ this._FavoritesCategoryId = value; }
		}
		///<summary>
		///
		///</summary>
		public string ArticleInfoId
		{
			get{ return this._ArticleInfoId; }
			set{ this._ArticleInfoId = value; }
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
		public FavoritesCategoryInfo FavoritesCategoryInfo
		{
			get{ return this._FavoritesCategoryInfo; }
			set{ this._FavoritesCategoryInfo=value; }
		}
		///<summary>
		///
		///</summary>
		public ArticleInfo ArticleInfo
		{
			get{ return this._ArticleInfo; }
			set{ this._ArticleInfo=value; }
		}
        #endregion
        
        #region 方法
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is FavoritesInfo)
            {
                result = (obj as FavoritesInfo).FavoritesInfoId == this.FavoritesInfoId;
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