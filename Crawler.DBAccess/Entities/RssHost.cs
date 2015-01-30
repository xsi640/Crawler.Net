using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crawler.DBAccess.Entities
{
    /// <summary>
    /// 
    /// </summary>
	[Serializable]
	public class RssHost
	{
		#region 变量定义
        private long _RssHostId = 0;
        private string _Title = string.Empty;
        private string _SubTitle = string.Empty;
        private string _Url = string.Empty;
        private DateTime _LastUpdateTime = new DateTime(1900,1,1);
        
        #endregion
        
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public RssHost()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        public RssHost
        (
			long rssHostId,
			string title,
			string subTitle,
			string url,
			DateTime lastUpdateTime
        )
        {
            this._RssHostId = rssHostId;
            this._Title = title;
            this._SubTitle = subTitle;
            this._Url = url;
            this._LastUpdateTime = lastUpdateTime;
        }
        #endregion
        
        #region 公共属性
		///<summary>
		///
		///</summary>
		public long RssHostId
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
		public string SubTitle
		{
			get{ return this._SubTitle; }
			set{ this._SubTitle = value; }
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

        #endregion
        
        #region 方法
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is RssHost)
            {
                result = (obj as RssHost).RssHostId == this.RssHostId;
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