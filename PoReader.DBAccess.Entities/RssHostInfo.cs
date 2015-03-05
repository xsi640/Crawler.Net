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
	public class RssHostInfo
	{
		#region 变量定义
        private string _RssHostInfoId = string.Empty;
        private string _CategoryId = string.Empty;
        private string _Title = string.Empty;
        private string _Url = string.Empty;
        private DateTime _LastUpdateTime = new DateTime(1900,1,1);
        private int _Status = 0;
        private string _CheckedId = string.Empty;
        private string _CreateUserId = string.Empty;
        private string _Tags = string.Empty;
        private DateTime _ReadedTime = new DateTime(1900,1,1);
        private DateTime _NextReadTime = new DateTime(1900,1,1);
        
		private CategoryInfo _CategoryInfo = null;
		private IList<ArticleInfo> _ArticleInfo = null;
		private IList<UrlDirectInfo> _UrlDirectInfo = null;
        #endregion
        
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public RssHostInfo()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        public RssHostInfo
        (
			string rssHostInfoId,
			string categoryId,
			string title,
			string url,
			DateTime lastUpdateTime,
			int status,
			string checkedId,
			string createUserId,
			string tags,
			DateTime readedTime,
			DateTime nextReadTime
        )
        {
            this._RssHostInfoId = rssHostInfoId;
            this._CategoryId = categoryId;
            this._Title = title;
            this._Url = url;
            this._LastUpdateTime = lastUpdateTime;
            this._Status = status;
            this._CheckedId = checkedId;
            this._CreateUserId = createUserId;
            this._Tags = tags;
            this._ReadedTime = readedTime;
            this._NextReadTime = nextReadTime;
        }
        #endregion
        
        #region 公共属性
		///<summary>
		///
		///</summary>
		public string RssHostInfoId
		{
			get{ return this._RssHostInfoId; }
			set{ this._RssHostInfoId = value; }
		}
		///<summary>
		///
		///</summary>
		public string CategoryId
		{
			get{ return this._CategoryId; }
			set{ this._CategoryId = value; }
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
		public string Url
		{
			get{ return this._Url; }
			set{ this._Url = value; }
		}
		///<summary>
		///
		///</summary>
		public DateTime LastUpdateTime
		{
			get{ return this._LastUpdateTime; }
			set{ this._LastUpdateTime = value; }
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
		public string CheckedId
		{
			get{ return this._CheckedId; }
			set{ this._CheckedId = value; }
		}
		///<summary>
		///
		///</summary>
		public string CreateUserId
		{
			get{ return this._CreateUserId; }
			set{ this._CreateUserId = value; }
		}
		///<summary>
		///
		///</summary>
		public string Tags
		{
			get{ return this._Tags; }
			set{ this._Tags = value; }
		}
		///<summary>
		///
		///</summary>
		public DateTime ReadedTime
		{
			get{ return this._ReadedTime; }
			set{ this._ReadedTime = value; }
		}
		///<summary>
		///
		///</summary>
		public DateTime NextReadTime
		{
			get{ return this._NextReadTime; }
			set{ this._NextReadTime = value; }
		}

		///<summary>
		///
		///</summary>
		public CategoryInfo CategoryInfo
		{
			get{ return this._CategoryInfo; }
			set{ this._CategoryInfo=value; }
		}
		///<summary>
		///
		///</summary>
		public IList<ArticleInfo> ArticleInfo
		{
			get{ return this._ArticleInfo; }
			set{ this._ArticleInfo = value; }
		}
		///<summary>
		///
		///</summary>
		public IList<UrlDirectInfo> UrlDirectInfo
		{
			get{ return this._UrlDirectInfo; }
			set{ this._UrlDirectInfo = value; }
		}
        #endregion
        
        #region 方法
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is RssHostInfo)
            {
                result = (obj as RssHostInfo).RssHostInfoId == this.RssHostInfoId;
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