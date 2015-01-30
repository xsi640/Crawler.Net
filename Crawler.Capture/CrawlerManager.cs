using Crawler.DBAccess.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crawler.Capture
{
    public class CrawlerManager
    {
        private CapturerManager _Mananger = new CapturerManager();

        public CrawlerManager()
        {
            this._Mananger.CapturerAdded += mananger_CapturerAdded;
            this._Mananger.CapturerIdled += mananger_CapturerIdled;
            this._Mananger.CapturerLeaked += mananger_CapturerLeaked;
            this._Mananger.CapturerStatusChanged += mananger_CapturerStatusChanged;
        }

        private void AddCapturer(CapturerItem item)
        {
            if (DALFactory.CaptureDAL.Count(string.Format("Url='{0}'", item.Url)) == 0)
                this._Mananger.Enqueue(item);
        }

        void mananger_CapturerStatusChanged(CapturerItem item)
        {
            if (item.Status == ECapturerStatus.Finish)
            {

            }
            else if (item.Status == ECapturerStatus.Error)
            {

            }
        }

        void mananger_CapturerLeaked(CapturerItem item)
        {
            if (item.Tag is DBAccess.Entities.Capture)
            {
                DBAccess.Entities.Capture capture = item.Tag as DBAccess.Entities.Capture;
                capture.Status = (int)EStatus.Idle;
                DALFactory.CaptureDAL.Update(capture);
            }
        }

        void mananger_CapturerIdled()
        {
            IList<DBAccess.Entities.Capture> lists = DALFactory.CaptureDAL.Select(string.Format("Status={0}", (int)EStatus.Idle));
        }

        void mananger_CapturerAdded(CapturerItem item)
        {
            DBAccess.Entities.Capture c = new DBAccess.Entities.Capture();
            c.Status = (int)EStatus.Queue;
            c.Url = item.Url;
            DALFactory.CaptureDAL.Insert(c);
        }
    }
}
