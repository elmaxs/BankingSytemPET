using BankingSystemPET.BL.Model;
using BankingSystemPET.BL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BankingSystemPET.BL.Controller
{
    public class BankOperationController : BaseController
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("BankingSystemPET.BL.Localization.OperationController", typeof(BankOperationController).Assembly);

        public BankAccountController BankAccount { get; set; }
        public BankOperation BankOperation { get; set; }

        public BankOperationController()
        {
            BankAccount = new BankAccountController(GetIndef());

            InterfaceOperation();
        }

        private int GetIndef()
        {
            Console.WriteLine(LocalizationManager.GetString(_resourceManager, "EnterYourIndef"));
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int indef) && indef > 0)
                    return indef;
                else
                    Console.WriteLine(LocalizationManager.GetString(_resourceManager, "InvalidData"));
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
            Console.WriteLine(LocalizationManager.GetString(_resourceManager, "ChoseOperation"));
            Console.WriteLine(LocalizationManager.GetString(_resourceManager, "ReplenishmentOperation"));
            Console.WriteLine(LocalizationManager.GetString(_resourceManager, "RemovalOperation"));
            Console.WriteLine(LocalizationManager.GetString(_resourceManager, "TransferOperation"));

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int chose))
                {
                    if (chose == 1 || chose == 2 || chose == 3)
                        return chose;
                    else
                        Console.WriteLine(LocalizationManager.GetString(_resourceManager, "NotFoundOperation"));
                }
                else
                    Console.WriteLine(LocalizationManager.GetString(_resourceManager, "InvalidData"));
            }
        }

        private decimal GetAmount()
        {
            Console.WriteLine("Enter amount for replenishment");
            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                    return amount;
                else
                    Console.WriteLine(LocalizationManager.GetString(_resourceManager, "ErrorAmountLessNull"));
            }
        }
        private void ReplenishmentOperation()
        {
            decimal amount = GetAmount();
            if (amount < 0)
                Console.WriteLine(LocalizationManager.GetString(_resourceManager, "ErrorAmountLessNull"));

            this.BankAccount.BankAccount.AmountBalance += amount;
            BankAccount.SaveAnotherClass();
        }

        private void RemovalOperation()
        {
            decimal amount = GetAmount();
            if (amount < 0)
                Console.WriteLine(LocalizationManager.GetString(_resourceManager, "ErrorAmountLessNull"));

            if (amount > this.BankAccount.BankAccount.AmountBalance)
                Console.WriteLine("The top-up amount is greater than the remaining amount");

            this.BankAccount.BankAccount.AmountBalance -= amount;
            BankAccount.SaveAnotherClass();
        }

        private void TransferOperation()
        {
            //in BankAccountController nado sdelat opros hotite sdelat acc ili net
            Console.WriteLine("Enter indef account to transfer");
            int indef = GetIndef();
            BankAccountController bankAccountTo = new BankAccountController(indef);
            decimal amount = GetAmount();
            if (amount < 0 || amount > this.BankAccount.BankAccount.AmountBalance)
                Console.WriteLine(LocalizationManager.GetString(_resourceManager, "ErrorAmountLessNull"));

            bankAccountTo.BankAccount.AmountBalance += amount;
            this.BankAccount.BankAccount.AmountBalance -= amount;
            bankAccountTo.SaveAnotherClass();
            BankAccount.SaveAnotherClass();
        }

        private List<BankOperation> Load()
        {
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
