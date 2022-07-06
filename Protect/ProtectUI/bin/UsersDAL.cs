using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsersDAL : BaseDAL<Users>,IUsersDAL
    {
        DbContext db = BaseContext.CreateInatance();
        public static SqlDataReader UsersLogin(Users users)
        {
            string sql = "[Proc_LoginByPassword]";

            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("Account",users.Account),
                new SqlParameter("Password",users.Password)
            };

            return SqlHelper.ExecuteReader(sql, CommandType.StoredProcedure, pars);
        }
    }
}
