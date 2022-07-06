using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IBaseDAL<T>
    {
        string AddEntity(T entity);
        bool DeleteEntity(T entity);
        bool EditEntity(T entity);
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> expression);
    }
}
