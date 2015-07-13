using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace RptPutty.Services
{
    public class Security
    {
        public bool getAdmin(String userName)
        {
            bool? isAdmin = false;
            if (string.IsNullOrEmpty(userName)) { return (bool) isAdmin; }
            //using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["RptPutty"].ConnectionString))
            //{
            //    SqlCommand sqlcmd = new SqlCommand("SELECT [ADMIN] FROM [USERS] WHERE USERNAME = @USER", sqlcon);
            //    sqlcmd.Parameters.Add(new SqlParameter("@USER", userName));
                
            //    try
            //    {
            //        sqlcon.Open();
            //        isAdmin = (bool)sqlcmd.ExecuteScalar();
            //    }
            //    catch { }
            //}
            isAdmin = RptPutty.Database.UserSecurity.UserAdmin(userName);
            //if (!isAdmin.HasValue) { isAdmin = false; }
            return (bool) isAdmin;
        }
    }
}