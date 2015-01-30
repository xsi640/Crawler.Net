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
	public class Url
	{
		#region 变量定义
        private long _UrlId = 0;
        private string _Address = string.Empty;
        private int _Count = 0;
        
        #endregion
        
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public Url()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        public Url
        (
			long urlId,
			string address,
			int count
        )
        {
            this._UrlId = urlId;
            this._Address = address;
            this._Count = count;
        }
        #endregion
        
        #region 公共属性
		///<summary>
		///
		///</summary>
		public long UrlId
		{
			get{ return this._UrlId; }
			set{ this._UrlId = value; }
		}
		///<summary>
		///
		///</summary>
		public string Address
		{
			get{ return this._Address; }
			set{ this._Address = value; }
		}
		///<summary>
		///
		///</summary>
		public int Count
		{
			get{ return this._Count; }
			set{ this._Count = value; }
		}

        #endregion
        
        #region 方法
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is Url)
            {
                result = (obj as Url).UrlId == this.UrlId;
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