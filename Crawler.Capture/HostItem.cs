using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Capture
{
    public class HostItem : BindableBase
    {
        #region 变量
        private string _Url = string.Empty;
        private EHostStatus _Status = EHostStatus.Queue;
        private int _Layer = 0;
        private int _Count = 0;
        private IList<CapturerItem> _CapturerLists = new List<CapturerItem>();
        #endregion

        #region 构造函数
        public HostItem()
        { }
        #endregion

        #region 属性
        public string Url
        {
            get { return this._Url; }
            set { base.SetProperty<string>(ref this._Url, value, "Url"); }
        }
        public EHostStatus Status
        {
            get { return this._Status; }
            set { base.SetProperty<EHostStatus>(ref this._Status, value, "Status"); }
        }
        public int Layer
        {
            get { return this._Layer; }
            set { base.SetProperty<int>(ref this._Layer, value, "Layer"); }
        }
        public int Count
        {
            get { return this._Count; }
            set { base.SetProperty<int>(ref this._Count, value, "Count"); }
        }
        #endregion

        public void Start()
        {
            CapturerItem item = new CapturerItem(this.Url);
            item.PropertyChanged += item_PropertyChanged;
            this._CapturerLists.Add(item);
            item.Start();
        }

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        public void Pause()
        {

        }
    }
}