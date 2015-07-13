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
        //public JobStatus newJob(JobStatus job)
        //{
        //    using (OdbcConnection oconn = new OdbcConnection(ConfigurationManager.ConnectionStrings["StatusTracker"].ConnectionString))
        //    {
        //        OdbcCommand ocomm = new OdbcCommand("INSERT INTO JOB_STATUS (ID, STATUS_C, FILENAME) VALUES (?, ?, ?)");
        //        ocomm.Parameters.Add(new OdbcParameter("id", job.ID.ToString()));
        //        ocomm.Parameters.Add(new OdbcParameter("status", "0"));
        //        ocomm.Parameters.Add(new OdbcParameter("filename", job.filename));
        //        if (job.requestor != "")
        //        {
        //            ocomm.CommandText = "INSERT INTO JOB_STATUS (ID, STATUS_C, FILENAME, REQUESTOR) VALUES (?, ?, ?, ?)";
        //            ocomm.Parameters.Add(new OdbcParameter("requestor", job.requestor));
        //        }
        //        ocomm.Connection = oconn;

        //        oconn.Open();
        //        ocomm.ExecuteNonQuery();
        //    }
        //    return new JobStatus();
        //}
        public JobStatus getJob(Guid ID)
        {
            JobStatus jStatus = new JobStatus();
            //using (OdbcConnection oconn = new OdbcConnection(ConfigurationManager.ConnectionStrings["StatusTracker"].ConnectionString))
            //{
            //    OdbcCommand ocomm = new OdbcCommand("SELECT ID, STATUS_C, FILENAME, REQUESTOR, WORKER, PROCESS_ID, PROCESS_START, PROCESS_END  FROM JOB_STATUS WHERE ID=?");
            //    ocomm.Parameters.Add(new OdbcParameter("id", ID.ToString()));
            //    ocomm.Connection = oconn;

            //    oconn.Open();
            //    OdbcDataReader odr = ocomm.ExecuteReader();


            //    if (odr.HasRows)
            //    {
            //        jStatus.ID = Guid.Parse(odr.GetString(0));
            //        //jStatus.STATUS_C = (Status)Enum.Parse(typeof(Status), odr.GetString(1));
            //        jStatus.FILENAME = odr.GetString(2);
            //        if (!odr.IsDBNull(3)) { jStatus.REQUESTOR = odr.GetString(3); } else { jStatus.REQUESTOR = null; }
            //        if (!odr.IsDBNull(4)) { jStatus.WORKER = odr.GetString(4); } else { jStatus.WORKER = null; }
            //        if (!odr.IsDBNull(5)) { jStatus.PROCESS_ID = odr.GetInt32(5); } else { jStatus.PROCESS_ID = 0; }
            //        if (!odr.IsDBNull(6)) { jStatus.PROCESS_START = odr.GetDateTime(6); } else { jStatus.PROCESS_START = DateTime.Parse("1753-01-01T00:00:00"); }
            //        if (!odr.IsDBNull(7)) { jStatus.PROCESS_END = odr.GetDateTime(7); } else { jStatus.PROCESS_END = DateTime.Parse("1753-01-01T00:00:00"); }

            //    }
            jStatus = Newtonsoft.Json.JsonConvert.DeserializeObject<JobStatus>(Newtonsoft.Json.JsonConvert.SerializeObject(RptPutty.Database.JobStatus.GetJobStatus(ID)));
            return jStatus;
            //}
        }
        public List<JobStatus> getAllJobs()
        {
            List<JobStatus> jobs = new List<JobStatus>();
            //using (OdbcConnection oconn = new OdbcConnection(ConfigurationManager.ConnectionStrings["StatusTracker"].ConnectionString))
            //{
            //    OdbcCommand ocomm = new OdbcCommand("SELECT ID, STATUS_C, FILENAME, REQUESTOR, WORKER, PROCESS_ID, PROCESS_START, PROCESS_END FROM JOB_STATUS WHERE PROCESS_END = ? OR PROCESS_END >= ? OR PROCESS_END IS NULL");
            //    ocomm.Parameters.Add(new OdbcParameter("nulldatefill", DateTime.Parse("1753-01-01T00:00:00")));
            //    ocomm.Parameters.Add(new OdbcParameter("nulldatefill", DateTime.Now.AddHours(-24)));
            //    ocomm.Connection = oconn;

            //    oconn.Open();
            //    OdbcDataReader odr = ocomm.ExecuteReader();

            //    if (odr.HasRows)
            //    {
            //        while (odr.Read())
            //        {
            //            JobStatus jStatus = new JobStatus();
            //            jStatus.ID = Guid.Parse(odr.GetString(0));
            //            jStatus.STATUS_C = (Status)Enum.Parse(typeof(Status), odr.GetString(1));
            //            jStatus.FILENAME = odr.GetString(2);
            //            if (!odr.IsDBNull(3)) { jStatus.REQUESTOR = odr.GetString(3); } else { jStatus.REQUESTOR = null; }
            //            if (!odr.IsDBNull(4)) { jStatus.WORKER = odr.GetString(4); } else { jStatus.WORKER = null; }
            //            if (!odr.IsDBNull(5)) { jStatus.PROCESS_ID = int.Parse(odr.GetString(5)); } else { jStatus.PROCESS_ID = 0; }
            //            if (!odr.IsDBNull(6)) { jStatus.PROCESS_START = odr.GetDateTime(6); } else { jStatus.PROCESS_START = DateTime.Parse("1753-01-01T00:00:00"); }
            //            if (!odr.IsDBNull(7)) { jStatus.PROCESS_END = odr.GetDateTime(7); } else { jStatus.PROCESS_END = DateTime.Parse("1753-01-01T00:00:00"); }
            //            jobs.Add(jStatus);
            //        } 
            //    }

            //}
            jobs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JobStatus>>(Newtonsoft.Json.JsonConvert.SerializeObject(RptPutty.Database.JobStatus.GetJobStatuses(-48)));
            return jobs;
        }
        public JobStatus updateJob(JobStatus job)
        {
            RptPutty.Database.JobStatus.UpdateJobStatus(job.ID, (int) job.STATUS_C, job.WORKER, job.PROCESS_ID.ToString(), job.PROCESS_START, job.PROCESS_END);
            //using (OdbcConnection oconn = new OdbcConnection(ConfigurationManager.ConnectionStrings["StatusTracker"].ConnectionString))
            //{
            //    OdbcCommand ocomm = new OdbcCommand("UPDATE JOB_STATUS SET STATUS_C=?, WORKER=?, PROCESS_ID=?, START_TIME=?, END_TIME=? WHERE ID=?");
            //    ocomm.Parameters.Add(new OdbcParameter("status", job.status));
            //    ocomm.Parameters.Add(new OdbcParameter("worker", job.worker));
            //    ocomm.Parameters.Add(new OdbcParameter("pid", job.processID));
            //    ocomm.Parameters.Add(new OdbcParameter("stime", job.start));
            //    ocomm.Parameters.Add(new OdbcParameter("etime", job.end));
            //    ocomm.Parameters.Add(new OdbcParameter("id", job.ID.ToString()));
            //    ocomm.Connection = oconn;

            //    oconn.Open();
            //    ocomm.ExecuteNonQuery();
            //}
            return new JobStatus();
        }
    }
}