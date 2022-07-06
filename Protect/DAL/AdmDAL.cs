using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdmDAL : BaseDAL<Administrator>, IAdmDAL
    {
        DbContext db = BaseContext.CreateInatance();
        public static SqlDataReader AdmLogin(Administrator administrator)
        {
            string sql = "[Proc_AdmLoginByPassword]";

            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("Admid",administrator.Admid),
                new SqlParameter("Password",administrator.Password)
            };

            return SqlHelper.ExecuteReader(sql, CommandType.StoredProcedure, pars);
        }
    }
}
