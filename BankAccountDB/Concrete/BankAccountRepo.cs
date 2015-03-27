using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountDB.Abstract;
using BankAccountDB.Concrete.Entities;
using System.Data.Entity;

namespace BankAccountDB.Concrete
{
    public class BankAccountRepo : BankAccountDB.Abstract.IBankAccountRepo //:IBankAccountRepo
    {
        private EFDbContext context = new EFDbContext();
    
        public IQueryable<UserProfile> UserProfiles
        {
            get { return context.UserProfiles.Include(user => user.Accounts); }
        }

        public void UpdateUserProfile(UserProfile entity)
        {
            context.Entry(entity).State = entity.UserProfileID == 0 ? EntityState.Added : EntityState.Modified;
        }

        //public void SaveUserProfile(UserProfile entity)
        //{
        //    if (entity.UserProfileID == 0)
        //    {
        //        context.UserProfiles.Add(entity);
        //    }
        //    else
        //    {
        //        UserProfile dbEntry = context.UserProfiles.Find(entity.UserProfileID);
        //        if (dbEntry != null)
        //        {
        //            dbEntry.UserProfileID = entity.UserProfileID;
        //            dbEntry.UserName = entity.UserName;
        //        }
        //    }
        //}

        public UserProfile DeleteUserProfile(int entityID)
        {
            UserProfile dbEntry = context.UserProfiles.Find(entityID);
            if (dbEntry != null)
            {
                context.UserProfiles.Remove(dbEntry);
            }

            return dbEntry;
        }
       
        public IQueryable<BasicAccount> BasicAccounts
        {
            get { return context.BasicAccounts; }
        }

        public void UpdateBasicAccount(BasicAccount entity)
        {
            context.Entry(entity).State = entity.BasicAccountID == 0 ?EntityState.Added : EntityState.Modified; 
        }

        public BasicAccount DeleteBasicAccount(int entityID)
        {
            BasicAccount dbEntry = context.BasicAccounts.Find(entityID);
            if (dbEntry != null)
            {
                context.BasicAccounts.Remove(dbEntry);
            }

            return dbEntry;
        }

        public void SaveChanges(){  context.SaveChanges();  }
    }
}
