using BankingSystemPET.BL.Model;
using BankingSystemPET.BL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystemPET.BL.Controller
{
    public class BankAccountController : BaseController
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("BankingSystemPET.BL.Localization.UserControllerMessages", typeof(UserController).Assembly);
        public BankAccount BankAccount {  get; set; }

        public BankAccountController(int indef)
        {
            BankAccount = GetAccount(indef);
            if(BankAccount == null)
            {
                CreateNewAccount(indef);
                Save();
            }
        }

        private void CreateNewAccount(int indef)
        {
            var userController = new UserController(indef);
            var user = userController.User;

            TypeBankAccount typeBankAccount = ParseTypeAccount();
            CreateNewAccount(user, typeBankAccount);
        }

        private void CreateNewAccount(User user, TypeBankAccount typeBankAccount)
        {
            if(user == null) throw new ArgumentNullException("User cant be null", nameof(user));
            if (!Enum.IsDefined(typeof(TypeBankAccount), typeBankAccount)) throw new ArgumentNullException("Type bank account cant be null", nameof(typeBankAccount));

            BankAccount = new BankAccount(user, typeBankAccount: typeBankAccount);
        }

        private TypeBankAccount ParseTypeAccount()
        {
            Console.WriteLine(LocalizationManager.GetString(_resourceManager, "ChoseTypeAccount"));
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int chose))
                {
                    if (Enum.IsDefined(typeof(TypeBankAccount), chose))
                        return (TypeBankAccount)chose;
                    else
                        Console.WriteLine(LocalizationManager.GetString(_resourceManager, "ParseTypeAccountNotFound"));
                }
                else
                    Console.WriteLine(LocalizationManager.GetString(_resourceManager, "ParseNotCorrectData"));
            }
        }

        private BankAccount? GetAccount(int indef)
        {
            if(indef  <= 0) throw new ArgumentException("Indef cant be less or equal 0");

            var bankAccount = Load<BankAccount>();
            return bankAccount.FirstOrDefault(a => a.User.Indef == indef);
        }

        private void Save()
        {
            List<BankAccount> bankAccounts = Load<BankAccount>();
            if(bankAccounts == null || bankAccounts.Count == 0)
                bankAccounts = new List<BankAccount>();

            bankAccounts.Add(BankAccount);
            Save(bankAccounts);
        }
    }
}
