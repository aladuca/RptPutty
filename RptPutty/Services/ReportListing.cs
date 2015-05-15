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
                OdbcCommand ocomm = new OdbcCommand("SELECT DISTINCT X_FILE_NAME, CLARITY_RPT.REPORT_NAME " +
                    "FROM X_CLARITY_SUBSCRIB "+
                    "INNER JOIN CLARITY_EMP ON X_CLARITY_SUBSCRIB.USER_NUMBER_ID=CLARITY_EMP.USER_ID "+
                    "LEFT JOIN REPORT_INFO ON X_CLARITY_SUBSCRIB.SUBSCRIBED_REPT_ID = REPORT_INFO.REPORT_INFO_ID " +
                    "LEFT JOIN TEMPLATE_INFO ON REPORT_INFO.REPORT_ID  = TEMPLATE_INFO.REPORT_ID " +
                    "LEFT JOIN CLARITY_RPT ON REPORT_INFO.REPORT_ID = CLARITY_RPT.ASSOC_REPORT_ID " +
                    "WHERE CLARITY_EMP.SYSTEM_LOGIN=? AND X_FILE_NAME IS NOT NULL AND CLARITY_RPT.REPORT_NAME IS NOT NULL AND TEMPLATE_INFO.STATUS_C = 0");
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
    }
}