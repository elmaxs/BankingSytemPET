using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystemPET.BL.Controller
{
    public class InterfaceController
    {
        private static readonly ResourceManager _resourceManager =
           new ResourceManager("BankingSystemPET.BL.Localization.InterfaceControllerMessages", typeof(UserController).Assembly);

        public BankAccountController BankAccountController { get; set; }
        public UserController UserController { get; set; }
        public BankOperationController BankOperationController { get; set; }

        public InterfaceController()
        {
            switch (GetDo())
            {
                case 1:
                    {
                        UserController = new UserController();
                        break;
                    }
                case 2:
                    {
                        BankAccountController = new BankAccountController();
                        break;
                    }
                case 3:
                    {
                        BankOperationController = new BankOperationController();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid data try again");
                        break;
                    }
            }
        }

        private int GetDo()
        {
            Console.WriteLine("Select do ");
            Console.WriteLine("1.UserController");
            Console.WriteLine("2.BankAccountController");
            Console.WriteLine("3.BankOperationController");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int chose) && (chose > 0 && chose <= 3)) 
                    return chose;
                else
                    Console.WriteLine("Invalid data try again");
            }
        }
    }
}
