using BankingSystemPET.BL.Model;
using BankingSystemPET.BL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BankingSystemPET.BL.Controller
{
    public class BankOperationController : BaseController
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("BankingSystemPET.BL.Localization.OperationController", typeof(BankOperationController).Assembly);

        //public int NumberOperation { get; set; }
        public BankAccountController BankAccount { get; set; }
        public BankOperation BankOperation { get; set; }

        public BankOperationController()
        {
            LocalizationManager.ChoseLocal("BankingSystemPET.BL.Localization.OperationControllerMessages");
            BankAccount = new BankAccountController(GetIndef());

            InterfaceOperation();
        }

        private int GetIndef()
        {
            Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.OperationControllerMessages", "EnterYourIndef"));
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int indef) && indef > 0)
                    return indef;
                else
                    Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.OperationControllerMessages", "InvalidData"));
            }
        }

        private void InterfaceOperation()
        {
            int chose = ChoseOperation();

            switch (chose)
            {
                case 1:
                    {
                        ReplenishmentOperation();
                        Save();
                        break;
                    }
                case 2:
                    {
                        RemovalOperation();
                        Save();
                        break;
                    }
                case 3:
                    {
                        TransferOperation();
                        Save();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Not found");
                        break;
                    }
            }
        }

        private int ChoseOperation()
        {
            Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.OperationControllerMessages", "ChoseOperation"));
            Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.OperationControllerMessages", "ReplenishmentOperation"));
            Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.OperationControllerMessages", "RemovalOperation"));
            Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.OperationControllerMessages", "TransferOperation"));

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int chose))
                {
                    if (chose == 1 || chose == 2 || chose == 3)
                        return chose;
                    else
                        Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.OperationControllerMessages", "NotFoundOperation"));
                }
                else
                    Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.OperationControllerMessages", "InvalidData"));
            }
        }

        private string GetFirstWord(string input)
        {
            // Используем регулярное выражение для выделения первой части
            string[] parts = Regex.Split(input, "(?=[A-Z])");

            // Возвращаем первое слово
            return parts.FirstOrDefault(part => !string.IsNullOrEmpty(part)) ?? string.Empty;
        }

        private decimal GetAmount(string nameMethod)
        {
            if (string.IsNullOrWhiteSpace(nameMethod)) throw new ArgumentNullException("Name method cant be null", nameof(nameMethod));

            Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.OperationControllerMessages", $"EnterAmount{nameMethod}"));
            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                    return amount;
                else
                    Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/"BankingSystemPET.BL.Localization.OperationControllerMessages", "ErrorAmountLessNull"));
            }
        }
        private void ReplenishmentOperation()
        {
            decimal amount = GetAmount(GetFirstWord(nameof(ReplenishmentOperation)));
            if (amount < 0)
                Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.OperationControllerMessages", "ErrorAmountLessNull"));

            this.BankAccount.BankAccount.AmountBalance += amount;

            BankAccount.SaveAnotherClass(BankAccount.BankAccount);

            BankOperation = new BankOperation(numberOperation : Load().Count + 1, DateTime.Now, TypeOperation.Replenishment, BankAccount.BankAccount, BankAccount.BankAccount);
        }

        private void RemovalOperation()
        {
            decimal amount = GetAmount(GetFirstWord(nameof(ReplenishmentOperation)));
            if (amount < 0)
                Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/"BankingSystemPET.BL.Localization.OperationControllerMessages", "ErrorAmountLessNull"));

            if (amount > this.BankAccount.BankAccount.AmountBalance)
                Console.WriteLine("The top-up amount is greater than the remaining amount");

            this.BankAccount.BankAccount.AmountBalance -= amount;

            BankAccount.SaveAnotherClass(BankAccount.BankAccount);
            BankOperation = new BankOperation(numberOperation: Load().Count + 1, DateTime.Now, TypeOperation.Removal, BankAccount.BankAccount, BankAccount.BankAccount);
        }

        public void TransferOperation()
        {
            while (true)
            {
                BankAccountController bankAccountTo;
                //in BankAccountController nado sdelat opros hotite sdelat acc ili net
                Console.WriteLine("Enter indef account to transfer");
                int indef = GetIndef();

                try
                {
                    bankAccountTo = new BankAccountController(indef.ToString());
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("Account not found. Try again, please.");
                    continue;
                }

                decimal amount = GetAmount(GetFirstWord(nameof(ReplenishmentOperation)));

                if (amount < 0 || amount > this.BankAccount.BankAccount.AmountBalance)
                {
                    Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.OperationControllerMessages", "ErrorAmountLessNull"));
                    continue;
                }
                
                bankAccountTo.BankAccount.AmountBalance += amount;
                this.BankAccount.BankAccount.AmountBalance -= amount;

                bankAccountTo.SaveAnotherClass(bankAccountTo.BankAccount);
                BankAccount.SaveAnotherClass(BankAccount.BankAccount);

                Console.WriteLine("Transfer completed successfully.");
                BankOperation = new BankOperation(numberOperation: Load().Count + 1, DateTime.Now, TypeOperation.Transfer, BankAccount.BankAccount, bankAccountTo.BankAccount);
                break;
            }  
        }

        private List<BankOperation> Load()
        {
            var list = base.Load<BankOperation>();
            //NumberOperation = list.Count() + 1;

            return base.Load<BankOperation>() ?? new List<BankOperation>();
        }

        private void Save()
        {
            var list = Load();
            list.Add(BankOperation);
            base.Save(list);
        }
    }
}
