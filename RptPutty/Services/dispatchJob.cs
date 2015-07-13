using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using RptPutty.Models;

namespace RptPutty.Services
{
    public class dispatchJob
    {
        public JobStatus dispatch(ReportJob reportJob, string user)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            reportJob.JobID = Guid.NewGuid();

            String jobFName = ConfigurationManager.AppSettings["rptDynamoJobLocation"] + reportJob.JobID + ".txt";
            File.WriteAllText(jobFName, jss.Serialize(reportJob));

            JobStatus jobStatus = new JobStatus();
            jobStatus.ID = reportJob.JobID;
            jobStatus.FILENAME = Path.GetFileName(reportJob.report.Filename);
            jobStatus.STATUS_C = (int)Status.Queued;
            jobStatus.REQUESTOR = user;

            RptPutty.Database.JobStatus.CreateJobStatus(jobStatus.ID, 0, jobStatus.FILENAME, jobStatus.REQUESTOR, jss.Serialize(reportJob));
            //StatusTracker st = new StatusTracker();
            //try { st.newJob(jobStatus); }
            //catch { }

            Process process = new Process();
            process.StartInfo.FileName = ConfigurationManager.AppSettings["rptDynamoExe"];
            process.StartInfo.Arguments = "-c \"" + ConfigurationManager.AppSettings["rptDynamoConfig"] + "\" -j \"" + jobFName + "\"";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.Dispose();

            return jobStatus;
        }
    }
}