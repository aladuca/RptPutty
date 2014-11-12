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
        public JobStatus dispatch(ReportJob reportJob)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            reportJob.JobID = Guid.NewGuid();

            String jobFName = ConfigurationManager.AppSettings["rptDynamoJobLocation"] + reportJob.JobID + ".txt";
            File.WriteAllText(jobFName, jss.Serialize(reportJob));

            JobStatus jobStatus = new JobStatus();
            jobStatus.jobID = reportJob.JobID;
            jobStatus.status = Status.Queued;

            Process process = new Process();
            process.StartInfo.FileName = ConfigurationManager.AppSettings["rptDynamoExe"];
            process.StartInfo.Arguments = "-c " + ConfigurationManager.AppSettings["rptDynamoExe"] + " -j " + jobFName;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.Dispose();

            return jobStatus;
        }
    }
}