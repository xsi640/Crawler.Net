using Crawler.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace Crawler.Capture
{
    public class CapturerItem : BindableBase
    {
        private string _Url = string.Empty;
        private ECapturerStatus _Status = ECapturerStatus.Stop;
        private HttpDownloader _Downloader = null;
        private HtmlParser _Parser = null;
        private object _Tag = null;

        public CapturerItem(string url)
        {
            this._Url = url;
        }

        public string Url
        {
            get { return this._Url; }
        }
        public ECapturerStatus Status
        {
            get { return this._Status; }
            set { base.SetProperty<ECapturerStatus>(ref this._Status, value, "Status"); }
        }
        public string HtmlContent
        {
            get
            {
                if (this._Downloader == null)
                    return string.Empty;
                else
                    return this._Downloader.HtmlContent;
            }
        }
        public IList<string> Urls
        {
            get
            {
                if (this._Parser == null)
                    return null;
                else
                    return this._Parser.Urls;
            }
        }
        public IList<string> RssUrls
        {
            get
            {
                if (this._Parser == null)
                    return null;
                else
                    return this._Parser.RssUrls;
            }
        }
        public object Tag
        {
            get { return this._Tag; }
        }

        public void Start()
        {
            if (this.Status == ECapturerStatus.Queue ||
                this.Status == ECapturerStatus.Stop ||
                this.Status == ECapturerStatus.Paused ||
                this.Status == ECapturerStatus.Finish)
            {
                this.Status = ECapturerStatus.Initialise;
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.BeginStart));
            }
        }

        private void BeginStart(object obj)
        {
            try
            {
                if (this._Downloader == null)
                    this._Downloader = new HttpDownloader(this._Url);

                this.Status = ECapturerStatus.Downloading;
                this._Downloader.Download();

                if (this.Status == ECapturerStatus.Pausing ||
                    this.Status == ECapturerStatus.Paused)
                    this.Status = ECapturerStatus.Paused;

                this.Status = ECapturerStatus.Parser;
                if (this._Parser == null)
                    this._Parser = new HtmlParser(this._Url, this._Downloader.HtmlContent);

                if (this.Status == ECapturerStatus.Pausing ||
                    this.Status == ECapturerStatus.Paused)
                    this.Status = ECapturerStatus.Paused;
                this._Parser.GetUrls();
                this._Parser.GetRssUrls();

                this.Status = ECapturerStatus.Finish;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ELogLevel.Error, typeof(CapturerItem), ex.ToString());
                this.Status = ECapturerStatus.Error;
            }
        }

        public void Stop()
        {
            this._Status = ECapturerStatus.Stop;
        }

        public void Pause()
        {
            this._Status = ECapturerStatus.Pausing;
        }
    }
}
