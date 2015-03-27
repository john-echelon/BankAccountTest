using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountDB.Concrete.Entities
{
    public class BasicAccount
    {
        public int BasicAccountID { set; get; }

        public decimal Balance { set; get; }
        public double InterestRate { set; get; }
        public int UserProfileID { set; get; }

        public AccountStatus StatusOfAccount { get; set; }
        public BankType AccountType { get; set; }

        public enum AccountStatus
        {
            Closed = 0,
            Open = 1,
            Frozen = 2
        }

        public enum BankType
        {
            RegularChecking = 0,
            PremiumChecking = 1,
            RegularSavings = 2,
            PremiumSavings = 3,
        }
    }
}
