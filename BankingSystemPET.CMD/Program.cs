using BankingSystemPET.BL.Controller;
using BankingSystemPET.BL.Utilities;
using System.Resources;

namespace BankingSystemPET.CMD
{
    internal class Program
    {
        //private static ResourceManager _resourceManager = new ResourceManager("BankingSystemPET.BL.Localization.Messages", typeof(Program).Assembly);

        static void Main(string[] args)
        {
            Console.WriteLine("Chose language (en-US/uk-UA)");
            string choseLanguage = Console.ReadLine()?.Trim();

            LocalizationManager.ChoseLocal("BankingSystemPET.BL.Localization.Messages");
            LocalizationManager.SetCulture(choseLanguage ?? "en-US");

            Console.WriteLine(LocalizationManager.GetString("BankingSystemPET.BL.Localization.Messages", "HelloMessages"));

            InterfaceController controller = new InterfaceController();
        }
    }
}
