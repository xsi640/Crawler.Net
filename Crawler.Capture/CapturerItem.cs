using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Crawler.Capture
{
    public class CapturerItem
    {
        private string _Url = string.Empty;
        private ECapturerStatus _Status = ECapturerStatus.Queue;
        private HttpDownloader _Downloader = null;
        private HtmlParser _Parser = null;

        public CapturerItem(string url)
        {
            this._Url = url;
        }

        public string Url
        {
            get { return this._Url; }
        }

        public void Start()
        {
            if (this._Status == ECapturerStatus.Queue || 
                this._Status == ECapturerStatus.Stop || 
                this._Status == ECapturerStatus.Finish)
            {
                this._Status = ECapturerStatus.Initialise;
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.BeginStart));
            }
        }

        private void BeginStart(object obj)
        {
            if (this._Downloader == null)
                this._Downloader = new HttpDownloader(this._Url);

            this._Status = ECapturerStatus.Downloading;
             this._Downloader.GetContent();
        }

        public void Stop()
        {

        }
    }
}
