
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LifeTypeBLL : BaseBLL<LifeType>
    {
        public override void SetDal()
        {
            dal = factory.CreateLifeTypeDal();
            /*dal = factory.UsersDal; */     //工厂方法创建类的实例
            /* dal = new UsersDAL(); */       //每次都new一个很麻烦，所以使用工厂方法创建DAL实例
        }

        public LifeType LoadEntity(int id)
        {
            LifeTypeDAL lifetypeDAL = new LifeTypeDAL();
            return lifetypeDAL.LoadEntity(id);
        }
    }
}
