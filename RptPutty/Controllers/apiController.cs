using System;
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
        public ReportForm get()
        {
            return enumRpt.RunDefinition();
        }
        public ReportForm get(string filename)
        {
            return enumRpt.RunDefinition(filename);
        }

        // POST: /api/generator.json
        // Receieve report parameter inputs for use in generation
        public JobStatus post(ReportJob reportJob)
        {
            return dispatcher.dispatch(reportJob);
        }
    }
    public class statusController : ApiController
    {
        // GET : /api/status.json
        // Used to get report generation statuses

        // POST: /api/status.json
        // Used to post report generation statuses

    }
    public class reportsController : ApiController
    {
        // GET: /api/reports.json
        // Used to get listing of all available reports

        // POST: /api/reports.json
    }

}
