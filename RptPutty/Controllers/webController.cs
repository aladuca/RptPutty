using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RptPutty.Models;
using RptPutty.Services;

namespace RptPutty.Controllers
{
    public class webController : Controller
    {
        private enumReport enumRpt;
        private dispatchJob dispatcher;
        private ReportListing svcListing;
        public webController()
        {
            this.enumRpt = new enumReport();
            this.dispatcher = new dispatchJob();
            this.svcListing = new ReportListing();
        }

        //
        // GET: /web/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult generator(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                return View(enumRpt.RunDefinition(id));
            }
            else
            {
                return View(enumRpt.RunDefinition());
            }
        }
        public ActionResult reports(string id)
        {
            return View(svcListing.getAllJobs(id));
        }
    }
}
