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
        public UserProfile CurrentUser { get; set; }
        public BasicAccount CurrentAccount { get; set; }

        private IBankAccountRepo repo;
        public AccountManager(IBankAccountRepo repo, int userProfileID)
        {
            this.repo = repo;
            CurrentUser = repo.UserProfiles.Where(up => up.UserProfileID == 1).Single();
        }

        public BasicAccount CreateBankAccount()
        {
            if (CurrentUser == null || CurrentUser.UserProfileID == 0)
            {
                throw new ArgumentNullException("User Profile is not setup.");
            }

            return new BasicAccount
            {
                UserProfileID = CurrentUser.UserProfileID,
                Balance = 0.0m,
                InterestRate = 0,
                StatusOfAccount = BasicAccount.AccountStatus.Open
            };
        }

        public BasicAccount GetBankAccount(int id)
        {
            CurrentAccount = CurrentUser.Accounts.Where(acct => acct.BasicAccountID == id).SingleOrDefault();

            return CurrentAccount;
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
            repo.UpdateBasicAccount(CurrentAccount);

            repo.SaveChanges();
        }
       
    }
}
