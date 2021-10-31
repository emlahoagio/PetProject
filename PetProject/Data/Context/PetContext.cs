using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class PetContext : DbContext, IEntityContext
    {
        public PetContext(DbContextOptions<PetContext> option) : base(option)
        {
            // Using DbContextOption to get ConnectString to connect Db SQLserver
        }
        public DbSet<Account> Accounts { get; set; }

        public object GetContext => this; //Get all dbset put it into "this"
    }
}
