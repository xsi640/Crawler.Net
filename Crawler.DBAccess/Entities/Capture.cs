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
	public class Capture
	{
		#region 变量定义
        private long _CaptureId = 0;
        private string _Url = string.Empty;
        private int _Status = 0;
        private string _Html = string.Empty;
        
        #endregion
        
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public Capture()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        public Capture
        (
			long captureId,
			string url,
			int status,
			string html
        )
        {
            this._CaptureId = captureId;
            this._Url = url;
            this._Status = status;
            this._Html = html;
        }
        #endregion
        
        #region 公共属性
		///<summary>
		///
		///</summary>
		public long CaptureId
		{
			get{ return this._CaptureId; }
			set{ this._CaptureId = value; }
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
		public int Status
		{
			get{ return this._Status; }
			set{ this._Status = value; }
		}
		///<summary>
		///
		///</summary>
		public string Html
		{
			get{ return this._Html; }
			set{ this._Html = value; }
		}

        #endregion
        
        #region 方法
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is Capture)
            {
                result = (obj as Capture).CaptureId == this.CaptureId;
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