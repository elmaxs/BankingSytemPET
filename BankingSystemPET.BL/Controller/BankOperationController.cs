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
    public class BankOperationController : BaseController
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("BankingSystemPET.BL.Localization.OperationController", typeof(BankOperationController).Assembly);

        public BankOperation BankOperation { get; set; }

        public BankOperationController()
        {
            InterfaceOperation();
        }

        private void InterfaceOperation()
        {
            int chose = ChoseOperation();

            switch (chose)
            {
                case 1:
                    {
                        ReplenishmentOperation();
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                    {
                        break;
                    }
                default:
                    {
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

        private void ReplenishmentOperation()
        {
            throw new NotImplementedException();
        }

        private void RemovalOperation()
        {
            throw new NotImplementedException();
        }

        private void TransferOperation()
        {
            throw new NotImplementedException();
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
