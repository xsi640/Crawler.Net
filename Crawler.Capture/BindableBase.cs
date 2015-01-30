using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Crawler.Capture
{
    /// <summary>
    /// 支持属性变更通知的基类
    /// </summary>
    [Serializable]
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 设置对象的属性
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="storage">对象属性</param>
        /// <param name="value">要设置的值</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>是否设置了属性，如果属性原有的值和要设置的值相同，则不会赋值</returns>
        protected bool SetProperty<T>(ref T storage, T value, string propertyName)
        {
            if (object.Equals(storage, value))
                return false;

            storage = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
