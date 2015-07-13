using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;
using RptPutty.Models;
using RptPutty.Services;
using System.Configuration;

namespace RptPutty.Services
{
    public class ReportListing
    {
        public List<Listing> getAllReports(string username)
        {
            List<Listing> list = new List<Listing>();
            using (OdbcConnection oconn = new OdbcConnection(ConfigurationManager.ConnectionStrings["UserAccess"].ConnectionString))
            {
                OdbcCommand ocomm = new OdbcCommand(ConfigurationManager.AppSettings["extReportListing"]);
                ocomm.Parameters.Add(new OdbcParameter("username", username));
                ocomm.Connection = oconn;

                oconn.Open();
                OdbcDataReader odr = ocomm.ExecuteReader();


                if (odr.HasRows)
                {
                    while (odr.Read())
                    {
                        enumReport enumRpt = new enumReport();
                        Listing rpt = new Listing();
                        rpt.Filename = odr.GetString(0);
                        rpt.Title = odr.GetString(1);
                        //rpt = enumRpt.getSummary(rpt);
                        list.Add(rpt);
                    }
                }

            }
            return list;
        }
        public Boolean getReportAccess(string username, string fname)
        {
            bool reportCount = false;
            using (OdbcConnection oconn = new OdbcConnection(ConfigurationManager.ConnectionStrings["UserAccess"].ConnectionString))
            {
                OdbcCommand ocomm = new OdbcCommand(ConfigurationManager.AppSettings["extReportAccess"]);
                ocomm.Parameters.Add("login", OdbcType.VarChar).Value = username;
                ocomm.Parameters.Add("filename", OdbcType.VarChar).Value = fname;
                ocomm.Connection = oconn;

                oconn.Open();
                OdbcDataReader odr = ocomm.ExecuteReader();

                reportCount = odr.HasRows;
            }
            return reportCount;
        }
    }
}