using Crawler.Log;
using Crawler.Net.Collection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Crawler.Capture
{
    public class CapturerManager
    {
        private Queue<CapturerItem> _Queue = new Queue<CapturerItem>();
        private int MAX_TASK = 1;
        private int _CurrentTaskCount = 0;

        public void Add(string url)
        {
            if (this._Queue.SingleOrDefault(item => item.Url == url) == null)
            {
                CapturerItem item = new CapturerItem(url);
                this._Queue.Enqueue(item);
                this.Dispatch();
            }
        }

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CapturerItem item = sender as CapturerItem;
            item.PropertyChanged -= item_PropertyChanged;
            if (item.Status == ECapturerStatus.Finish)
            {
                this.Handle(item);
                this._CurrentTaskCount--;
                this.Dispatch();
            }
            else if (item.Status == ECapturerStatus.Error)
            {
                this._CurrentTaskCount--;
                this.Dispatch();
            }
        }

        private void Dispatch()
        {
            while (this._CurrentTaskCount < MAX_TASK && this._Queue.Count > 0)
            {
                CapturerItem item = this._Queue.Dequeue();
                item.PropertyChanged += item_PropertyChanged;
                item.Start();
                this._CurrentTaskCount++;
            }
        }

        private void Handle(CapturerItem item)
        {
            foreach (string url in item.Urls)
                this.Add(url);
        }
    }
}
