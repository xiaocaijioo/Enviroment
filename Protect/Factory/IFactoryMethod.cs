using DAL;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    public interface IFactoryMethod
    {
       IUsersDAL UsersDal { get; set; }
       //IUsersDAL PostDal { get; set; }
    }
}
