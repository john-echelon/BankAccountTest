using System;
using System.Linq;
using BankAccountDB.Concrete.Entities;
using System.Collections.Generic;
namespace BankAccountDB.Abstract
{
    public interface IBankAccountRepo
    {
        List<UserProfile> UserProfiles { get; }
        UserProfile UserProfileFindByID(int entityID);
        void UpdateUserProfile(UserProfile entity);
        UserProfile DeleteUserProfile(int entityID);
        List<BasicAccount> BasicAccounts { get; }
        BasicAccount BasicAccountFindByID(int entityID);
        void UpdateBasicAccount(BasicAccount entity);
        BasicAccount DeleteBasicAccount(int entityID);

        void SaveChanges();
    }
}
