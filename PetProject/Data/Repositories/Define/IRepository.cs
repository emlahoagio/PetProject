using System;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repositories.Define
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(Expression<Func<T, bool>> exp, string[] include); //use include to query mulple table like subquery or Join
        IQueryable<T> GetAll(Expression<Func<T, bool>> exp, int numPage, int perPage, string[] include);
    }
}
