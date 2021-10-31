using Data.Repositories.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;

namespace Service.Implement
{
    //Thực hiện các phương thức chung của Service
    public class BaseService<T> where T : class
    {
        private readonly IUnitOfWork _unit;
        private readonly IRepository<T> _repo;

        public BaseService(IUnitOfWork unit)
        {
            _unit = unit;
            _repo = unit.GetRepository<T>();
        }

        public void Create(T entity)
        {
            try
            {
                //Xài Transaction để rollback data
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    _repo.Create(entity);
                    _unit.SaveChange();
                    scope.Complete();
                    //scope : cho biết trans đã hoàn thành
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void Update(T entity)
        {
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    _repo.Update(entity);
                    _unit.SaveChange();
                    scope.Complete();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void Delete(T entity)
        {
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    _repo.Delete(entity);
                    _unit.SaveChange();
                    scope.Complete();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public T Get(Expression<Func<T, bool>> exp, string[] include) => _repo.Get(exp, include);
        public IQueryable<T> GetAll(Expression<Func<T, bool>> exp, int numPage, int perPage, string[] include) => _repo.GetAll(exp, numPage, perPage, include);
    }
}
