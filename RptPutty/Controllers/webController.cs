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
        private Security security;
        public webController()
        {
            this.enumRpt = new enumReport();
            this.dispatcher = new dispatchJob();
            this.svcListing = new ReportListing();
            this.statusSvc = new StatusTracker();
            this.security = new Security();
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
                string pattern = "^.*\\\\";
                string replacement = "";
                System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(pattern);
                string result = rgx.Replace(User.Identity.Name, replacement);

                if (security.getAdmin(User.Identity.Name) || svcListing.getReportAccess(result, id))
                {
                    return View(enumRpt.RunDefinition(id));
                }
                else
                {
                    return View("Denial");
                }
            }
            else
            {
                return View(enumRpt.RunDefinition());
            }
        }
        public ActionResult reports(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
                if (security.getAdmin(User.Identity.Name)) { return View(svcListing.getAllReports(id)); }
                else { return RedirectToRoute(new { controller = "web", action = "reports", id="" }); }
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
                    return View(new List<Listing>());
            }
        }
        public ActionResult status()
        {
            var status = statusSvc.getAllJobs();
            if (security.getAdmin(User.Identity.Name)) { return View(status.OrderByDescending(x => x.start).ToList()); }
            else {
                status.RemoveAll(j => !User.Identity.Name.Equals(j.requestor));
                return View(status.OrderByDescending(x => x.start).ToList());
            }
        }
    }
}
