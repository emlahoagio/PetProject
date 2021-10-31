using Data.Context;
using Data.Repositories.Define;
using System;
using System.Collections.Generic;

namespace Data.Repositories.Implement
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDictionary<Type, object> _repository;
        //Dictionary is a BAG contain Repos
        //Type is anonymous data type
        private readonly PetContext _dbContext;
        private bool IDisposed = false;
        public UnitOfWork(IEntityContext entityContext)
        {
            _dbContext = entityContext.GetContext as PetContext;
            _repository = new Dictionary<Type, object>();
            //
        }
        //Dispose để giải phóng bộ nhớ khi thực hiện xong 1 API
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.IDisposed && disposing)
            {
                this._dbContext.Dispose();
            }
            this.IDisposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            IRepository<T> repo;
            Type key = typeof(T);
            //typeof(T) return tree folder ex: Data.Models.Account
            if (!this._repository.ContainsKey(key))
            {
                //Nếu không có thì tạo mới rồi add vào túi
                _repository.Add(key, repo = new Repository<T>(_dbContext));
            }
            else
            {
                repo = _repository[key] as Repository<T>;
            }
            return repo;
        }

        public void SaveChange()
        {
            _dbContext.SaveChanges();
        }
    }
}
