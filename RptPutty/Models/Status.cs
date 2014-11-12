using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RptPutty.Models
{
    public class JobStatus
    {
        public Guid jobID { get; set; }
        public Status status { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public String filename { get; set; }
        public String requestor { get; set; }
        public String worker { get; set; }
        public Int32 processID { get; set; }
    }
    public enum Status
    {
        Queued = 0,
        Processing = 1,
        Completed = 2,
        Failed = 3
    }

}