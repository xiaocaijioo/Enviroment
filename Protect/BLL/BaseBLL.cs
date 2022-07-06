using Factory;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class BaseBLL<T> where T:class,new()         //使用泛型对业务逻辑类进行封装,使用抽象方法解决
    {
        public AbstractFactory factory = new AbstractFactory();  //使用抽象工厂创建DAL类的实例
        //public FactoryMethod factory = new FactoryMethod();    //使用工厂方法创建DAL类的实例
        // UserDAL dal = new UserDAL();
        public IBaseDAL<T> dal;                                  //解决封装类中具体DAL类的实例化问题
        public abstract void SetDal();
        public BaseBLL()                                        //构造函数
        {
            SetDal();
        }
        public string AddEntity(T entity)
        {
            return dal.AddEntity(entity);
        }
        public bool DeleteEntity(T entity)
        {
            return dal.DeleteEntity(entity);
        }
        public bool EditEntity(T entity)
        {
            return dal.EditEntity(entity);
        }
        public IQueryable<T> LoadEntities(Expression<Func<T,bool>> expression)
        {
            return dal.LoadEntities(expression);
        }
    }
}
