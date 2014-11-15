using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.Odbc;
using RptPutty.Models;

namespace RptPutty.Services
{
    public class StatusTracker
    {
        public JobStatus newJob(JobStatus job)
        {
            using (OdbcConnection oconn = new OdbcConnection(ConfigurationManager.ConnectionStrings["StatusTracker"].ConnectionString))
            {
                OdbcCommand ocomm = new OdbcCommand("INSERT INTO JOB_STATUS (ID, STATUS_C, FILENAME) VALUES (?, ?, ?)");
                ocomm.Parameters.Add(new OdbcParameter("id", job.ID));
                ocomm.Parameters.Add(new OdbcParameter("status", "0"));
                ocomm.Parameters.Add(new OdbcParameter("filename", job.filename));
                ocomm.Connection = oconn;

                oconn.Open();
                ocomm.ExecuteNonQuery();
            }
            return new JobStatus();
        }
        public JobStatus getJob(Guid ID)
        {
            JobStatus jStatus = new JobStatus();
            using (OdbcConnection oconn = new OdbcConnection(ConfigurationManager.ConnectionStrings["StatusTracker"].ConnectionString))
            {
                OdbcCommand ocomm = new OdbcCommand("SELECT * FROM JOB_STATUS WHERE ID=?");
                ocomm.Parameters.Add(new OdbcParameter("id", ID.ToString()));
                ocomm.Connection = oconn;

                oconn.Open();
                OdbcDataReader odr = ocomm.ExecuteReader();

                

                if (odr.HasRows)
                {
                    jStatus.ID = odr.GetGuid(0);
                    jStatus.status = (Status)Enum.Parse(typeof(Status), odr.GetString(1));
                    jStatus.filename = odr.GetString(2);
                    if (!odr.IsDBNull(3)) { jStatus.requestor = odr.GetString(3); } else { jStatus.requestor = null; }
                    if (!odr.IsDBNull(4)) { jStatus.worker = odr.GetString(4); } else { jStatus.worker = null; }
                    if (!odr.IsDBNull(5)) { jStatus.processID = odr.GetInt32(5); } else { jStatus.processID = 0; }
                    if (!odr.IsDBNull(6)) { jStatus.start = odr.GetDateTime(6); } else { jStatus.start = DateTime.Parse("1753-01-01T00:00:00"); }
                    if (!odr.IsDBNull(7)) { jStatus.end = odr.GetDateTime(6); } else { jStatus.end = DateTime.Parse("1753-01-01T00:00:00"); }

                }
                return jStatus;
            }
        }
        public List<JobStatus> getAllJobs()
        {
            List<JobStatus> jobs = new List<JobStatus>();
            using (OdbcConnection oconn = new OdbcConnection(ConfigurationManager.ConnectionStrings["StatusTracker"].ConnectionString))
            {
                OdbcCommand ocomm = new OdbcCommand("SELECT * FROM JOB_STATUS WHERE END_TIME = ? OR END_TIME >= ? OR END_TIME IS NULL");
                ocomm.Parameters.Add(new OdbcParameter("nulldatefill", DateTime.Parse("1753-01-01T00:00:00")));
                ocomm.Parameters.Add(new OdbcParameter("nulldatefill", DateTime.Now.AddHours(-24)));
                ocomm.Connection = oconn;

                oconn.Open();
                OdbcDataReader odr = ocomm.ExecuteReader();

                if (odr.HasRows)
                {
                    do
                    {
                        JobStatus jStatus = new JobStatus();
                        jStatus.ID = odr.GetGuid(0);
                        jStatus.status = (Status)Enum.Parse(typeof(Status), odr.GetString(1));
                        jStatus.filename = odr.GetString(2);
                        if (!odr.IsDBNull(3)) { jStatus.requestor = odr.GetString(3); } else { jStatus.requestor = null; }
                        if (!odr.IsDBNull(4)) { jStatus.worker = odr.GetString(4); } else { jStatus.worker = null; }
                        if (!odr.IsDBNull(5)) { jStatus.processID = odr.GetInt32(5); } else { jStatus.processID = 0; }
                        if (!odr.IsDBNull(6)) { jStatus.start = odr.GetDateTime(6); } else { jStatus.start = DateTime.Parse("1753-01-01T00:00:00"); }
                        if (!odr.IsDBNull(7)) { jStatus.end = odr.GetDateTime(7); } else { jStatus.end = DateTime.Parse("1753-01-01T00:00:00"); }
                        jobs.Add(jStatus);
                    } while (odr.Read());
                }

            }
            return jobs;
        }
        public JobStatus updateJob(JobStatus job)
        {
            using (OdbcConnection oconn = new OdbcConnection(ConfigurationManager.ConnectionStrings["StatusTracker"].ConnectionString))
            {
                OdbcCommand ocomm = new OdbcCommand("UPDATE JOB_STATUS SET STATUS_C=?, WORKER=?, PROCESS_ID=?, START_TIME=?, END_TIME=? WHERE ID=?");
                ocomm.Parameters.Add(new OdbcParameter("status", job.status));
                ocomm.Parameters.Add(new OdbcParameter("worker", job.worker));
                ocomm.Parameters.Add(new OdbcParameter("pid", job.processID));
                ocomm.Parameters.Add(new OdbcParameter("stime", job.start));
                ocomm.Parameters.Add(new OdbcParameter("etime", job.end));
                ocomm.Parameters.Add(new OdbcParameter("id", job.ID));
                ocomm.Connection = oconn;

                oconn.Open();
                ocomm.ExecuteNonQuery();
            }
            return new JobStatus();
        }
    }
}