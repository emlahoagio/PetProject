using Data.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Service.Define
{
    public interface IAccountService
    {
        void Create(Account entity);
        void Update(Account entity);
        void Delete(Account entity);
        Account Get(Expression<Func<Account, bool>> exp, string[] include); //use include to query mulple table like subquery or Join
        IQueryable<Account> GetAll(Expression<Func<Account, bool>> exp, int numPage, int perPage, string[] include);
    }
}
