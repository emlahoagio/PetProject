using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Define
{
    public interface IUnitOfWork 
    {
        IRepository<T> GetRepository<T>() where T : class;
        void SaveChange();
    }
}
