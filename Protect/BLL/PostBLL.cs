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
    public class PostBLL : BaseBLL<Post>
    {
        public override void SetDal()
        {
            dal = factory.CreatePostDal();
            /*dal = factory.UsersDal; */     //工厂方法创建类的实例
            /* dal = new UsersDAL(); */       //每次都new一个很麻烦，所以使用工厂方法创建DAL实例
        }
      
        public Post LoadEntity(int id)
        {
            PostDAL postDAL = new PostDAL();
            return postDAL.LoadEntity(id);
        }
    }
}
