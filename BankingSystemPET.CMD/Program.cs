using BankingSystemPET.BL.Utilities;
using System.Resources;

namespace BankingSystemPET.CMD
{
    internal class Program
    {
        private static ResourceManager _resourceManager = new ResourceManager("BankingSystemPET.BL.Localization.Messages", typeof(Program).Assembly);

        static void Main(string[] args)
        {
            Console.WriteLine("Chose language (en/uk)");
            string choseLanguage = Console.ReadLine()?.Trim();

            LocalizationManager.SetCulture(choseLanguage ?? "en");

            Console.WriteLine(LocalizationManager.GetString(_resourceManager, "HelloMessages"));
        }
    }
}
