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

        public BankAccountController()
        {
            Console.WriteLine("Enter your indef");

            new BankAccountController(int.Parse(Console.ReadLine()));
        }

        public BankAccountController(string indef)
        {
            if (string.IsNullOrWhiteSpace(indef)) throw new ArgumentNullException("Invalid data", nameof(indef));
            if (int.Parse(indef) <= 0) throw new ArgumentException("Indef cant be less or equal 0", nameof(indef));

            BankAccount = GetAccount(int.Parse(indef));

            if (BankAccount == null)
            {
                throw new ArgumentNullException("Bank account not found");
            }
        }

        public BankAccountController(int indef)
        {
            if (indef <= 0) throw new ArgumentException("Indef cant be less or equal 0", nameof(indef));

            BankAccount = GetAccount(indef);

            if(BankAccount == null)
            {
                //nado придумать как делать правильно проверку для операций контроллера при перекидке денег 
                //2 stroki nize dobavil for test
                Console.WriteLine("Account not found, wanna create new?\n 1.Yes \t2.No");
                if (int.Parse(Console.ReadLine()) == 1)
                {
                    CreateNewAccount(indef);
                    Save();
                }
            }
        }

        private void CreateNewAccount(int indef)
        {
            var userController = new UserController(indef);

            TypeBankAccount typeBankAccount = ParseTypeAccount();

            CreateNewAccount(userController.User, typeBankAccount);
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
        public void SaveAnotherClass(BankAccount bankAccount)
        {
            List<BankAccount> bankAccounts = Load<BankAccount>();
            if (bankAccount == null || bankAccounts == null || bankAccounts.Count == 0)
            {
                bankAccounts.Add(BankAccount);
            }
            else
            {
                BankAccount b = bankAccounts.FirstOrDefault(a => a.User.Indef == bankAccount.User.Indef);
                if (b != null)
                {
                    b.AmountBalance = bankAccount.AmountBalance;
                }
            }
            Save(bankAccounts);
        }
    }
}
