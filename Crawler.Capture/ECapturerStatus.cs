using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crawler.Capture
{
    public enum ECapturerStatus
    {
        Queue,
        Initialise,
        Downloading,
        Parser,
        Error,
        Finish,
        Stop,
    }
}
