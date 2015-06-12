using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RptPutty_Test
{
    [TestClass]
    public class JobStatus
    {
        [TestMethod]
        public void DispatchJob()
        {
            var svcJobs = new RptPutty.Services.dispatchJob();
            svcJobs.dispatch(testingJob(), "TESTUSER");
        }
        [TestMethod]
        public void ProcessingJob()
        {
            var svcJobs = new RptPutty.Services.dispatchJob();
            var rptJob = testingJob();
            svcJobs.dispatch(rptJob, "TESTUSER");

            var svcStatus = new RptPutty.Services.StatusTracker();
            svcStatus.getJob(rptJob.JobID);
            RptPutty.Models.JobStatus statJob = svcStatus.getJob(rptJob.JobID);
            statJob.status = RptPutty.Models.Status.Processing;
            statJob.worker = "TestBoxA";
            statJob.start = DateTime.Now;
            statJob.end = DateTime.Parse("1753-01-01T00:00:00");
            svcStatus.updateJob(statJob);
        }
        [TestMethod]
        public void CompletedJob()
        {
            var svcJobs = new RptPutty.Services.dispatchJob();
            var rptJob = testingJob();
            svcJobs.dispatch(rptJob, "TESTUSER");
            System.Threading.Thread.Sleep(1000);
            
            var svcStatus = new RptPutty.Services.StatusTracker();           
            var statJob = svcStatus.getJob(rptJob.JobID);
            statJob.status = RptPutty.Models.Status.Processing;
            statJob.worker = "TestBoxA";
            statJob.start = DateTime.Now;
            svcStatus.updateJob(statJob);

            statJob.status = RptPutty.Models.Status.Completed;
            statJob.end = DateTime.Now.Add(new TimeSpan(0, 5, 0));
            svcStatus.updateJob(statJob);
        }
        [TestMethod]
        public void FailedJob()
        {
            var svcJobs = new RptPutty.Services.dispatchJob();
            var rptJob = testingJob();
            svcJobs.dispatch(rptJob, "TESTUSER");

            var svcStatus = new RptPutty.Services.StatusTracker();
            var statJob = svcStatus.getJob(rptJob.JobID);
            statJob.status = RptPutty.Models.Status.Processing;
            statJob.worker = "TestBoxA";
            statJob.start = DateTime.Now;
            svcStatus.updateJob(statJob);

            statJob.status = RptPutty.Models.Status.Failed;
            statJob.end = DateTime.Now.Add(new TimeSpan(0, 5, 0));
            svcStatus.updateJob(statJob);
        }
        public RptPutty.Models.ReportJob testingJob()
        {
            var rptjob = new RptPutty.Models.ReportJob
            {
                email = new RptPutty.Models.Email
                {
                    to = new System.Collections.Generic.List<string>(new string[] { "testuser@testdomain.local" })
                },
                report = new RptPutty.Models.ReportBase
                {
                    Filename = "testing",
                    Parameters = new System.Collections.Generic.List<RptPutty.Models.ParametersBase>(new RptPutty.Models.ParametersBase[]{
                    new RptPutty.Models.ParametersBase{
                        Name = "param1",
                        SelectedValues = new System.Collections.Generic.List<string>(new string[] {"p1value1"})
                    },
                    new RptPutty.Models.ParametersBase{
                        Name = "param2",
                        SelectedValues = new System.Collections.Generic.List<string>(new string[] {"p2value1"})
                    }
                    }),
                    SelectedOutput = RptPutty.Models.Output.PortableDocFormat,
                },
                JobID = Guid.NewGuid()
            };
            return rptjob;
        }
    }
}
