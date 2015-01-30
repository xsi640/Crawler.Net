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
                if (this.IsCheck(url))
                    return;
                CapturerItem capturer = new CapturerItem(url);
                capturer.Status = ECapturerStatus.Queue;
                this._Queue.Enqueue(capturer);
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
                if (this.IsCheck(capturer.Url))
                    return;
                capturer.Status = ECapturerStatus.Queue;
                this._Queue.Enqueue(capturer);
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
        }

        void capturer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Status")
            {
                CapturerItem item = sender as CapturerItem;
                if (item.Status == ECapturerStatus.Finish ||
                    item.Status == ECapturerStatus.Error ||
                    item.Status == ECapturerStatus.Paused ||
                    item.Status == ECapturerStatus.Stop)
                {
                    if (item.Status == ECapturerStatus.Finish)
                    {
                        if (item.Urls != null)
                        {
                            foreach (string url in item.Urls)
                            {
                                this.Enqueue(url);
                            }
                        }
                        if (item.RssUrls != null)
                        {
                            foreach (string rssUrl in item.RssUrls)
                            {
                                Console.WriteLine(rssUrl);
                            }
                        }
                    }

                    this.Scheduling();
                }
            }
        }

        private bool IsCheck(string url)
        {
            return DALFactory.CaptureDAL.Count(string.Format("Url='{0}'", url)) > 0;
        }

        private void Insert(CapturerItem item)
        {
            DBAccess.Entities.Capture capture = new DBAccess.Entities.Capture();
            capture.Status = (int)item.Status;
            capture.Url = item.Url;
            capture.Html = item.HtmlContent;
            DALFactory.CaptureDAL.Insert(capture);
        }
    }
}
