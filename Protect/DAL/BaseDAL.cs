using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class BaseDAL<T> where T:class,new()
    {
        //EnvironmentProtectEntities db = new EnvironmentProtectEntities();
        DbContext db = BaseContext.CreateInatance();    //应用单例模式创建数据库上下文类的实例
        public string AddEntity(T entity)
        {
            db.Set<T>().Add(entity);   //添加
            if( db.SaveChanges() > 0)
            {
                return "success";
            }
            else
            {
                return "fail";
            }
        }

        public bool DeleteEntity(T entity)
        {
            db.Set<T>().Remove(entity); //删除
            return db.SaveChanges() > 0;
        }

        public bool EditEntity(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Modified;  //编辑
            return db.SaveChanges() > 0;
        }

        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> expression)   //查询
        {
            return db.Set<T>().Where<T>(expression);
        }
        public T LoadEntity(int id)
        {
            return db.Set<T>().Find(id);
        }
    }
}
