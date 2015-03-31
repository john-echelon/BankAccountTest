using System;
using BankAccountDB.Concrete.Entities;

namespace BankAccountBL.Abstract
{
    public interface IAccountManager
    {
        BasicAccount CurrentAccount { get; set; }
        UserProfile CurrentUser { get; set; }
        BasicAccount CreateBankAccount();
        UserProfile GetUserProfile(int id);

        BasicAccount GetBankAccount(int id);
        void UpdateBankAccount();
        BasicAccount DeleteBankAccount(int id);
        void UpdateInterest();
        void Withdraw(decimal amount);
        void Deposit(decimal amount);
        void AddInterests();
        void Save();
    }
}
