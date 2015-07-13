using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RptPutty.Models
{
    public class JobStatus
    {
        public Guid ID { get; set; }
        public int STATUS_C { get; set; }
        public Nullable<DateTime> PROCESS_START { get; set; }
        public Nullable<DateTime> PROCESS_END { get; set; }
        public String FILENAME { get; set; }
        public String REQUESTOR { get; set; }
        public String WORKER { get; set; }
        public Nullable<Int32> PROCESS_ID { get; set; }

        //public Guid ID { get; set; }
        public Nullable<DateTime> REQUEST_TIME { get; set; }
        //public int STATUS_C { get; set; }
        //public String FILENAME { get; set; }
        //public String REQUESTOR { get; set; }
        //public String WORKER { get; set; }
        //public String PROCESS_ID { get; set; }
        //public Nullable<DateTime> PROCESS_START { get; set; }
        //public Nullable<DateTime> PROCESS_END { get; set; }
        //public String PARAMETERS { get; set; }
    }
    public enum Status
    {
        Queued = 0,
        Processing = 1,
        Completed = 2,
        Failed = 3
    }

}