using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace RptPutty.Database
{
    public class JobStatus
    {
        public static IList<JOB_STATUS> GetJobStatuses(int hours)
        {
            using (var db = new JobStatusesContext())
            {
                DateTime limithours = DateTime.Now.AddHours(hours);
                var jobStatuses = from jobs in db.JobStatuses where jobs.PROCESS_END == null || jobs.PROCESS_END >= limithours select jobs;
                return jobStatuses.ToList();
            }
        }
        public static JOB_STATUS GetJobStatus(Guid ID)
        {
            using (var db = new JobStatusesContext())
            {
                DateTime limithours = DateTime.Now.AddHours(-48);
                var jobStatuses = from jobs in db.JobStatuses where jobs.ID == ID select jobs;
                return jobStatuses.FirstOrDefault();
            }
        }
        public static void CreateJobStatus(Guid id, int status, string filename, string requestor, string parameters)
        {
            using (var db = new JobStatusesContext())
            {
                var jStatus = new JOB_STATUS
                {
                    ID = id,
                    STATUS_C = status,
                    FILENAME = filename,
                    REQUESTOR = requestor,
                    //Worker = null,
                    //Process_ID = null,
                    //Start_Time = null,
                    //End_Time = null,
                    PARAMETERS = parameters
                };
                db.JobStatuses.Add(jStatus);
                try { db.SaveChanges(); }
                catch { System.Threading.Thread.Sleep(300); db.SaveChanges(); }
            }
        }
        public static void UpdateJobStatus(Guid id, int status, string worker, string process, Nullable<DateTime> start, Nullable<DateTime> end){
            using (var db = new JobStatusesContext())
            {
                var job = db.JobStatuses.SingleOrDefault(j => j.ID == id);
                if (job != null)
                {
                    job.WORKER = worker;
                    job.PROCESS_ID = process;
                    job.STATUS_C = status;
                    job.PROCESS_START = start;
                    job.PROCESS_END = end;
                    try { db.SaveChanges(); }
                    catch { System.Threading.Thread.Sleep(300); db.SaveChanges(); }
                }
            }
        }
    }
    public class JobStatusesContext : DbContext
    {
        public JobStatusesContext() : base("name=RptPutty") { }
        public DbSet<JOB_STATUS> JobStatuses { get; set; }
    }
    public class JOB_STATUS
    {
        public Guid ID { get; set; }
        public Nullable<DateTime> REQUEST_TIME { get; set; }
        public int STATUS_C { get; set; }
        public String FILENAME { get; set; }
        public String REQUESTOR { get; set; }
        public String WORKER { get; set; }
        public String PROCESS_ID { get; set; }
        public Nullable<DateTime> PROCESS_START { get; set; }
        public Nullable<DateTime> PROCESS_END { get; set; }
        public String PARAMETERS { get; set; }
    }
}
