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
	public class ArticleInfo
	{
		#region 变量定义
        private string _ArticleInfoId = string.Empty;
        private string _RssHostId = string.Empty;
        private string _Title = string.Empty;
        private string _Summary = string.Empty;
        private string _Author = string.Empty;
        private DateTime _CreateTime = new DateTime(1900,1,1);
        private DateTime _UpdateTime = new DateTime(1900,1,1);
        private string _Url = string.Empty;
        private string _Content = string.Empty;
        private string _Photos = string.Empty;
        
		private RssHostInfo _RssHostInfo = null;
		private IList<FavoritesInfo> _FavoritesInfo = null;
        #endregion
        
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public ArticleInfo()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        public ArticleInfo
        (
			string articleInfoId,
			string rssHostId,
			string title,
			string summary,
			string author,
			DateTime createTime,
			DateTime updateTime,
			string url,
			string content,
			string photos
        )
        {
            this._ArticleInfoId = articleInfoId;
            this._RssHostId = rssHostId;
            this._Title = title;
            this._Summary = summary;
            this._Author = author;
            this._CreateTime = createTime;
            this._UpdateTime = updateTime;
            this._Url = url;
            this._Content = content;
            this._Photos = photos;
        }
        #endregion
        
        #region 公共属性
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
		public string RssHostId
		{
			get{ return this._RssHostId; }
			set{ this._RssHostId = value; }
		}
		///<summary>
		///
		///</summary>
		public string Title
		{
			get{ return this._Title; }
			set{ this._Title = value; }
		}
		///<summary>
		///
		///</summary>
		public string Summary
		{
			get{ return this._Summary; }
			set{ this._Summary = value; }
		}
		///<summary>
		///
		///</summary>
		public string Author
		{
			get{ return this._Author; }
			set{ this._Author = value; }
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
		public DateTime UpdateTime
		{
			get{ return this._UpdateTime; }
			set{ this._UpdateTime = value; }
		}
		///<summary>
		///
		///</summary>
		public string Url
		{
			get{ return this._Url; }
			set{ this._Url = value; }
		}
		///<summary>
		///
		///</summary>
		public string Content
		{
			get{ return this._Content; }
			set{ this._Content = value; }
		}
		///<summary>
		///
		///</summary>
		public string Photos
		{
			get{ return this._Photos; }
			set{ this._Photos = value; }
		}

		///<summary>
		///
		///</summary>
		public RssHostInfo RssHostInfo
		{
			get{ return this._RssHostInfo; }
			set{ this._RssHostInfo=value; }
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
            if (obj is ArticleInfo)
            {
                result = (obj as ArticleInfo).ArticleInfoId == this.ArticleInfoId;
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