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
	public class CategoryInfo
	{
		#region 变量定义
        private string _CategoryInfoId = string.Empty;
        private string _Name = string.Empty;
        private string _Photo = string.Empty;
        private string _ParentId = string.Empty;
        private int _Count = 0;
        private int _Layer = 0;
        private int _SubCount = 0;
        private DateTime _CreateTime = new DateTime(1900,1,1);
        private string _Creator = string.Empty;
        
		private IList<RssHostInfo> _RssHostInfo = null;
        #endregion
        
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public CategoryInfo()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        public CategoryInfo
        (
			string categoryInfoId,
			string name,
			string photo,
			string parentId,
			int count,
			int layer,
			int subCount,
			DateTime createTime,
			string creator
        )
        {
            this._CategoryInfoId = categoryInfoId;
            this._Name = name;
            this._Photo = photo;
            this._ParentId = parentId;
            this._Count = count;
            this._Layer = layer;
            this._SubCount = subCount;
            this._CreateTime = createTime;
            this._Creator = creator;
        }
        #endregion
        
        #region 公共属性
		///<summary>
		///
		///</summary>
		public string CategoryInfoId
		{
			get{ return this._CategoryInfoId; }
			set{ this._CategoryInfoId = value; }
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
		public string Photo
		{
			get{ return this._Photo; }
			set{ this._Photo = value; }
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
		public int Layer
		{
			get{ return this._Layer; }
			set{ this._Layer = value; }
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
		public DateTime CreateTime
		{
			get{ return this._CreateTime; }
			set{ this._CreateTime = value; }
		}
		///<summary>
		///
		///</summary>
		public string Creator
		{
			get{ return this._Creator; }
			set{ this._Creator = value; }
		}

		///<summary>
		///
		///</summary>
		public IList<RssHostInfo> RssHostInfo
		{
			get{ return this._RssHostInfo; }
			set{ this._RssHostInfo = value; }
		}
        #endregion
        
        #region 方法
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is CategoryInfo)
            {
                result = (obj as CategoryInfo).CategoryInfoId == this.CategoryInfoId;
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