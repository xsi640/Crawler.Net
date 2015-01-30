using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crawler.DBAccess.Entities;

namespace Crawler.DBAccess.IDAL
{
    public interface ICaptureDAL: IBaseDAL<long, Capture>
    {
    }
}
