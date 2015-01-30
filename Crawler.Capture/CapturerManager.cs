using Crawler.DBAccess.Entities;
using Crawler.DBAccess.Factory;
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
        private ThreadSafeQueue<CapturerItem> _Queue = new ThreadSafeQueue<CapturerItem>();
        private ThreadSafeList<CapturerItem> _RunningLists = new ThreadSafeList<CapturerItem>();
        private int _MaxWorkingCount = 5;
        private int _MaxWorkQueueCount = 1000;

        public event Action<CapturerItem> CapturerAdded;
        public event Action<CapturerItem> CapturerLeaked;
        public event Action<CapturerItem> CapturerStatusChanged;
        public event Action CapturerIdled;

        public CapturerManager()
        { }

        public int MaxWorkingCount
        {
            get { return this._MaxWorkingCount; }
            set { this._MaxWorkingCount = value; }
        }

        public void Enqueue(string url)
        {
            if (!this._Queue.Contains(new Match<CapturerItem>((w) =>
            {
                return url == w.Url;
            })))
            {
                CapturerItem capturer = new CapturerItem(url);
                capturer.Status = ECapturerStatus.Queue;
                if (this._Queue.Count > this._MaxWorkQueueCount)
                {
                    this.OnCapturerLeaked(capturer);
                    return;
                }
                this._Queue.Enqueue(capturer);
                this.OnCapturerAdded(capturer);
                this.Scheduling();
            }
        }

        public void Enqueue(CapturerItem capturer)
        {
            if (capturer == null)
                return;

            if (!this._Queue.Contains(new Match<CapturerItem>((w) =>
            {
                return capturer.Url == w.Url;
            })))
            {
                capturer.Status = ECapturerStatus.Queue;
                if (this._Queue.Count > this._MaxWorkQueueCount)
                {
                    this.OnCapturerLeaked(capturer);
                    return;
                }
                this._Queue.Enqueue(capturer);
                this.OnCapturerAdded(capturer);
                this.Scheduling();
            }
        }

        private void Scheduling()
        {
            while (this._RunningLists.Count < this._MaxWorkingCount && this._Queue.Count > 0)
            {
                CapturerItem capturer = this._Queue.Dequeue();
                capturer.PropertyChanged += capturer_PropertyChanged;
                capturer.Start();
            }
            if (this._RunningLists.Count == 0 && this._Queue.Count == 0)
                this.OnCapturerIdled();
        }

        void capturer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Status")
            {
                CapturerItem item = sender as CapturerItem;
                this.OnCapturerStatusChanged(item);
                if (item.Status == ECapturerStatus.Finish ||
                    item.Status == ECapturerStatus.Error ||
                    item.Status == ECapturerStatus.Paused ||
                    item.Status == ECapturerStatus.Stop)
                {
                    this.Scheduling();
                }
            }
        }

        private void OnCapturerAdded(CapturerItem item)
        {
            if (this.CapturerAdded != null)
                this.CapturerAdded(item);
        }
        private void OnCapturerStatusChanged(CapturerItem item)
        {
            if (this.CapturerStatusChanged != null)
                this.CapturerStatusChanged(item);
        }
        private void OnCapturerLeaked(CapturerItem item)
        {
            if (this.CapturerLeaked != null)
                this.OnCapturerLeaked(item);
        }
        private void OnCapturerIdled()
        {
            if (this.CapturerIdled != null)
                this.CapturerIdled();
        }
    }
}
