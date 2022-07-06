using DAL;  //如果另一个项目也要调用它的UsersDAL，就必须修改命名空间
using IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    public class FactoryMethod:IFactoryMethod //工厂方法
    {
        private IUsersDAL _usersDal;
        public IUsersDAL UsersDal 
        {
            get
            {
                if (_usersDal == null)
                {
                    _usersDal = new UsersDAL();
                }
                return _usersDal;
            }
            set
            {
                _usersDal = value;
            }
        }

        public IUsersDAL CreateUsersDal()  //通过配置文件与反射创建DAL类的实例
        {
            string dalAssemblyPath = ConfigurationManager.AppSettings["DalAssemblyPath"];
            string dalNameSpace = ConfigurationManager.AppSettings["DalNameSpace"];
            string fullClassName = dalAssemblyPath + ".UsersDAL";

            UsersDAL usersDAL = (UsersDAL)Assembly.Load(dalAssemblyPath).CreateInstance(fullClassName);
            return usersDAL;
        }
    }
}
