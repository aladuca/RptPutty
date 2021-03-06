﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RptPutty.Models;
using RptPutty.Services;

namespace RptPutty.Controllers
{
    public class generatorController : ApiController
    {
        private enumReport enumRpt;
        private dispatchJob dispatcher;
        public generatorController()
        {
            this.enumRpt = new enumReport();
            this.dispatcher = new dispatchJob();
        }
        // GET : /api/generator.json
        // Used to get JSON object of report parameters for generation
        public ReportDetail get()
        {
            return enumRpt.RunDefinition();
        }
        public ReportDetail get(string filename)
        {
            return enumRpt.RunDefinition(filename);
        }

        // POST: /api/generator.json
        // Receieve report parameter inputs for use in generation
        public JobStatus post ([FromBody] ReportJob reportJob)
        {
            return dispatcher.dispatch(reportJob, (User.Identity).Name);
        }
    }
    public class statusController : ApiController
    {
        private StatusTracker statusSvc;
        public statusController()
        {
            this.statusSvc = new StatusTracker();
        }
        // GET : /api/status.json
        // Used to get report generation statuses
        public List<JobStatus> get()
        {
            return statusSvc.getAllJobs();
        }
        public JobStatus get(Guid id)
        {
            return statusSvc.getJob(id);
        }
        // POST: /api/status.json
        // Used to post report generation statuses
        public void post([FromBody] JobStatus job)
        {
            statusSvc.updateJob(job);
        }

    }
    public class reportsController : ApiController
    {
        private ReportListing svcListing;
        public reportsController()
        {
            this.svcListing = new ReportListing();
        }
        // GET: /api/reports.json
        public List<Listing> get()
        {
            return svcListing.getAllReports(User.Identity.Name);
        }
        public List<Listing> get(string id)
        {
            return svcListing.getAllReports(id);
        }
        // POST: /api/reports.json
    }

}
