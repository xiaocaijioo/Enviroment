using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LifeBLL : BaseBLL<Life>
    {
        public override void SetDal()
        {
            dal = factory.CreateLifeDal();
            /*dal = factory.UsersDal; */     //工厂方法创建类的实例
            /* dal = new UsersDAL(); */       //每次都new一个很麻烦，所以使用工厂方法创建DAL实例
        }

        public Life LoadEntity(int id)
        {
            LifeDAL lifeDAL = new LifeDAL();
            return lifeDAL.LoadEntity(id);
        }
    }
}
