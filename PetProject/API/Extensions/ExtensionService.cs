using Data.Context;
using Data.Repositories.Define;
using Data.Repositories.Implement;
using Microsoft.Extensions.DependencyInjection;
using Service.Define;
using Service.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    //Config  Swagger, Cors, gọi là DI nhưng config method IOC, 
    public static class ExtensionService
    {
        #region DI
        public static void ConfigIOC(this IServiceCollection services)
        {
            //SingleTon chạy lúc runtime
            //Scope chạy xuyên suốt
            //Trans 
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEntityContext, PetContext>();
        }
        #endregion
    }
}
