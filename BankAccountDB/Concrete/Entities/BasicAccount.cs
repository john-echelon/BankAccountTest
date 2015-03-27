using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BankAccountDB.Concrete.Entities
{
    public class BasicAccount
    {
        [HiddenInput(DisplayValue = true)]
        public int BasicAccountID { set; get; }

        public decimal Balance { set; get; }
        public double InterestRate { set; get; }
        [HiddenInput(DisplayValue = false)]
        public int UserProfileID { set; get; }
        [UIHint("Enum")]
        public AccountStatus StatusOfAccount { get; set; }
        [UIHint("Enum")]
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
