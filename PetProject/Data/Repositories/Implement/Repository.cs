using Data.Context;
using Data.Repositories.Define;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Repositories.Implement
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly PetContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(PetContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }
        public void Create(T entity)
        {
            _dbContext.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }

        public T Get(Expression<Func<T, bool>> exp, string[] include)
        {
            if (include == null)
            return _dbSet.Where(exp).FirstOrDefault();
            else
            {
                IQueryable<T> query = _dbSet; 
                // tạo ra 1 mảng gồm tất cả DbSet
                query = include.Aggregate(query, (current, inc) => current.Include(inc));
                // Aggregate thực hiện 1 thao tác tổng hợp tùy chỉnh trên các giá trị trong danh sách
                return query.Where(exp).FirstOrDefault();
            }
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> exp, int numPage, int perPage, string[] include)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }
    }
}
