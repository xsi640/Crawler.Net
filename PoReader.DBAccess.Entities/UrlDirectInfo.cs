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
	public class UrlDirectInfo
	{
		#region 变量定义
        private string _UrlDirectInfoId = string.Empty;
        private string _RssHostInfoId = string.Empty;
        private string _Url = string.Empty;
        
		private RssHostInfo _RssHostInfo = null;
        #endregion
        
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public UrlDirectInfo()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        public UrlDirectInfo
        (
			string urlDirectInfoId,
			string rssHostInfoId,
			string url
        )
        {
            this._UrlDirectInfoId = urlDirectInfoId;
            this._RssHostInfoId = rssHostInfoId;
            this._Url = url;
        }
        #endregion
        
        #region 公共属性
		///<summary>
		///
		///</summary>
		public string UrlDirectInfoId
		{
			get{ return this._UrlDirectInfoId; }
			set{ this._UrlDirectInfoId = value; }
		}
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
		public string Url
		{
			get{ return this._Url; }
			set{ this._Url = value; }
		}

		///<summary>
		///
		///</summary>
		public RssHostInfo RssHostInfo
		{
			get{ return this._RssHostInfo; }
			set{ this._RssHostInfo=value; }
		}
        #endregion
        
        #region 方法
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is UrlDirectInfo)
            {
                result = (obj as UrlDirectInfo).UrlDirectInfoId == this.UrlDirectInfoId;
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