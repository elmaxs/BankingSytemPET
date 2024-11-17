using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystemPET.BL.Model
{
    public class BankAccount
    {
        public int ID { get; set; }

        public decimal AmountBalance { get; set; }

        public User? User { get; set; }

        public TypeBankAccount? TypeBankAccounts { get; set; }

        public BankAccount(User user, decimal amountBalance = 0, TypeBankAccount? typeBankAccount = TypeBankAccount.None)
        {
            if(User == null) throw new ArgumentNullException("User cant be null", nameof(user));
            if(amountBalance < 0) throw new ArgumentException("Amount balance cant be less 0", nameof(amountBalance));
            if (typeBankAccount == null) throw new ArgumentNullException("Bank account type cant be null", nameof(typeBankAccount));

            User = user;
            AmountBalance = amountBalance;
            this.TypeBankAccounts = typeBankAccount;
        }
    }
}
