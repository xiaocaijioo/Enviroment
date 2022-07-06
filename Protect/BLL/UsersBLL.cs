using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class UsersBLL:BaseBLL<Users>
    {
        public override void SetDal()
        {
            dal = factory.CreateUsersDal();
            /*dal = factory.UsersDal; */     //工厂方法创建类的实例
           /* dal = new UsersDAL(); */       //每次都new一个很麻烦，所以使用工厂方法创建DAL实例
        }
       
        public Users LoadEntity(int id)
        {
            UsersDAL usersDAL = new UsersDAL();
            return usersDAL.LoadEntity(id);
        }
        public  SqlDataReader UsersLogin(Users users)
        {
            return UsersDAL.UsersLogin(users);
        }

        //public string UserZanPost(int id,int useris)
        //{
        //    var saveuser=UsersDAL.UserZanPost(id,userid)
        //    return user;
        //}
    }
}
