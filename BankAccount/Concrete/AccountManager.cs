using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountDB.Abstract;
using BankAccountDB.Concrete.Entities;

namespace BankAccountBL.Concrete
{
    public class AccountManager : BankAccountBL.Abstract.IAccountManager
    {
        public BasicAccount CurrentAccount {get; set;}
        public UserProfile CurrentUser { get; set; }

        private IBankAccountRepo repo;
        public AccountManager(IBankAccountRepo repo)
        {
            this.repo = repo;
        }

        public void CreateBankAccount(UserProfile profile)
        {
            if(profile == null || profile.UserProfileID ==0){
                throw new ArgumentNullException("User Profile is not setup.");
            }

            CurrentAccount = new BasicAccount
            {
                BasicAccountID = CurrentUser.UserProfileID,
                Balance = 0.0m,
                InterestRate = 0,
                StatusOfAccount = BasicAccount.AccountStatus.Open
            };
        }

        public void UpdateBankAccount()
        {
            repo.UpdateBasicAccount(CurrentAccount);
        }

        public void GetBankAccount(UserProfile profile)
        { 
            var result =
            (from existingAccount in repo.BasicAccounts
            where existingAccount.BasicAccountID == profile.UserProfileID
            select existingAccount).SingleOrDefault();

            CurrentAccount = result;
        }

        public void UpdateInterest()
        {
            if (CurrentAccount.Balance < 10000m)
            {
                CurrentAccount.InterestRate = .01f;
            }
            else if (CurrentAccount.Balance >= 10000 && CurrentAccount.Balance < 100000)
            {
                CurrentAccount.InterestRate = .02f;
            }
            else // ( CurrentAccount.Balance >= 100000)
            {
                CurrentAccount.InterestRate = .03f;
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount > CurrentAccount.Balance)
            {
                throw new Exception("Overdraft protection enacted!");
            }
            CurrentAccount.Balance -= amount;
            UpdateInterest();
        }

        public void Deposit(decimal amount)
        {
            CurrentAccount.Balance += amount;
            UpdateInterest();
        }

        public virtual void AddInterests()
        {
            CurrentAccount.Balance += CurrentAccount.Balance * (Decimal)CurrentAccount.InterestRate;
        }

        public void Save()
        {
            repo.SaveChanges();
        }
       
    }
}
