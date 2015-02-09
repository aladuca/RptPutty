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
        private StatusTracker statusSvc;
        public webController()
        {
            this.enumRpt = new enumReport();
            this.dispatcher = new dispatchJob();
            this.svcListing = new ReportListing();
            this.statusSvc = new StatusTracker();
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
            if (!string.IsNullOrWhiteSpace(id))
                return View(svcListing.getAllReports(id));
            else
            {
            if (!string.IsNullOrWhiteSpace(User.Identity.Name))
                {
                    string pattern = "^.*\\\\";
                    string replacement = "";
                    System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(pattern);
                    string result = rgx.Replace(User.Identity.Name, replacement);
                    return View(svcListing.getAllReports(result));
                }
                else
                    return View(new ReportListing());
            }
        }
        public ActionResult status ()
        {
            return View(statusSvc.getAllJobs());
        }
    }
}
