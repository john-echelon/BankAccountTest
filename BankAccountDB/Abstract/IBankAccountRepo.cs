using System;
using System.Linq;
using BankAccountDB.Concrete.Entities;
namespace BankAccountDB.Abstract
{
    public interface IBankAccountRepo
    {
        BasicAccount DeleteBasicAccount(int entityID);
        UserProfile DeleteUserProfile(int entityID);
        IQueryable<BasicAccount> BasicAccounts { get; }
        IQueryable<UserProfile> UserProfiles { get; }
        void SaveChanges();
        void UpdateBasicAccount(BasicAccount entity);
        void UpdateUserProfile(UserProfile entity);
    }
}
