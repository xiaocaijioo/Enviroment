using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AdmBLL : BaseBLL<Administrator>
    {

        public override void SetDal()
        {
            dal = factory.CreateAdmDal();
        }

        public Administrator LoadEntity(int id)
        {
            AdmDAL admDAL = new AdmDAL();
            return admDAL.LoadEntity(id);
        }
        public SqlDataReader AdmLogin(Administrator administrator)
        {
            return AdmDAL.AdmLogin(administrator);
        }

    }
}

