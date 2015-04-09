using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BankAccountDB.Concrete.Entities;
namespace BankAccountDB.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("BankAccount") { }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<BasicAccount> BasicAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
