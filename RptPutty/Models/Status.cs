using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RptPutty.Models
{
    public class JobStatus
    {
        Guid jobID { get; set; }
        Status status { get; set; }
        DateTime start { get; set; }
        DateTime end { get; set; }
        String filename { get; set; }
        String requestor { get; set; }
        String worker { get; set; }
        Int32 processID { get; set; }
    }
    public enum Status
    {
        Queued = 0,
        Processing = 1,
        Completed = 2,
        Failed = 3
    }

}