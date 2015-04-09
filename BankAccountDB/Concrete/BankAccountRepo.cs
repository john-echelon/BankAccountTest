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
    public class BankAccountRepo : IBankAccountRepo
    {
        private EFDbContext context;

        public BankAccountRepo(EFDbContext context)
        {
            this.context = context;
        }

        public List<UserProfile> UserProfiles
        {
            get { return context.UserProfiles.Include("Accounts").ToList(); }
        }

        public UserProfile UserProfileFindByID(int entityID)
        {
            return context.UserProfiles.Where(entity => entity.UserProfileID == entityID).SingleOrDefault();
        }

        public void UpdateUserProfile(UserProfile entity)
        {
            if (entity.UserProfileID == 0)
            {
                context.UserProfiles.Add(entity);
            }
            else
            {
                UserProfile dbEntry = context.UserProfiles.Find(entity.UserProfileID);
                if (dbEntry != null)
                {
                    dbEntry.UserProfileID = entity.UserProfileID;
                    dbEntry.UserName = entity.UserName;
                }
            }
        }

        public UserProfile DeleteUserProfile(int entityID)
        {
            UserProfile dbEntry = context.UserProfiles.Find(entityID);
            if (dbEntry != null)
            {
                context.UserProfiles.Remove(dbEntry);
            }

            return dbEntry;
        }

        public List<BasicAccount> BasicAccounts
        {
            get { return context.BasicAccounts.ToList(); }
        }

        public BasicAccount BasicAccountFindByID(int entityID)
        {
            return context.BasicAccounts.Where(entity => entity.BasicAccountID == entityID).SingleOrDefault();
        }

        public void UpdateBasicAccount(BasicAccount entity)
        {
            if (entity.BasicAccountID == 0)
            {
                context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                var dbEntry = context.BasicAccounts.Find(entity.BasicAccountID);
                if (dbEntry != null)
                {
                    dbEntry.AccountType = entity.AccountType;
                    dbEntry.Balance = entity.Balance;
                    dbEntry.InterestRate = entity.InterestRate;
                    dbEntry.StatusOfAccount = entity.StatusOfAccount;
                }
            }
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

        public void SaveChanges() { context.SaveChanges(); }
    }
}
