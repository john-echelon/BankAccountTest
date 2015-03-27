using System;
using BankAccountDB.Concrete.Entities;

namespace BankAccountBL.Abstract
{
    public interface IAccountManager
    {
        BasicAccount CurrentAccount { get; set; }
        UserProfile CurrentUser { get; set; }
        BasicAccount CreateBankAccount();
        BasicAccount GetBankAccount(int id);
        void UpdateInterest();
        void Withdraw(decimal amount);
        void Deposit(decimal amount);
        void AddInterests();
        void Save();
    }
}
