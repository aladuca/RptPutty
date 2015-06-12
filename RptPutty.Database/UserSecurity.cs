﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace RptPutty.Database
{
    class UserSecurity
    {
        static Boolean UserAdmin(String userName)
        {
            using (var db = new UsersContext())
            {
                var user = from usr in db.Users where usr.USERNAME == userName select usr.ADMIN_FLAG;
                if (user.Count() == 1) { return user.FirstOrDefault(); }
                return false;
            }
        }
        public class UsersContext : DbContext
        {
            //Ensure SQL Provider is copied : http://stackoverflow.com/questions/21641435/error-no-entity-framework-provider-found-for-the-ado-net-provider-with-invarian
            public UsersContext() : base("name=RptPutty") { var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance; }
            public DbSet<USER> Users { get; set; }
        }
        public class USER
        {
            public String USERNAME { get; set; }
            public Boolean ADMIN_FLAG { get; set; }
        }
    }
}
