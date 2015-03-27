using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BankAccountDB.Concrete.Entities
{
    [DisplayName("User Profile")]
    public class UserProfile
    {
        public int UserProfileID { get; set; }
        public string UserName { get; set; }
        public virtual ICollection<BasicAccount> Accounts { get; set; }
    }
}
