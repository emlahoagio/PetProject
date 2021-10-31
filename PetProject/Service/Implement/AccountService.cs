using Data.Models;
using Data.Repositories.Define;
using Service.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Service.Implement
{
    public class AccountService : BaseService<Account>, IAccountService
    {
        public AccountService(IUnitOfWork unit) : base(unit)
        {

        }
    }
}
