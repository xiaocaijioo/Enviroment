using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BaseContext
    {
        public static DbContext CreateInatance()
        {
            DbContext db = (DbContext)CallContext.GetData("db");
            if (db == null)
            {
                db = new EnvironmentProtectEntities();
                CallContext.SetData("db", db);
            }
            return db;
        }
    }
}
