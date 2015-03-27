using System;
using BankAccountDB.Concrete.Entities;

namespace BankAccountBL.Abstract
{
    public interface IAccountManager
    {
        BasicAccount CurrentAccount { get; set; }
        UserProfile CurrentUser { get; set; }
        void CreateBankAccount(UserProfile profile);
        void UpdateBankAccount();
        void GetBankAccount(UserProfile profile);
        void UpdateInterest();
        void Withdraw(decimal amount);
        void Deposit(decimal amount);
        void AddInterests();
        void Save();
    }
}
