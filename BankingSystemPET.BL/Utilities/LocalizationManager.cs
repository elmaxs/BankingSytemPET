using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystemPET.BL.Utilities
{
    public static class LocalizationManager
    {
        //private static ResourceManager _resourceManager = new ResourceManager("BankingSystemPET.BL.Localization.Messages", typeof(LocalizationManager).Assembly);
        //private static Dictionary<string, ResourceManager> _resourceManagers = new Dictionary<string, ResourceManager>
        //{
        //    { "Messages", new ResourceManager("BankingSystemPET.BL.Localization.Messages", typeof(LocalizationManager).Assembly) },
        //    { "UserControllerMessages", new ResourceManager("BankingSystemPET.BL.Localization.UserControllerMessages", typeof(LocalizationManager).Assembly) },
        //    { "OperationControllerMessages", new ResourceManager("BankingSystemPET.BL.Localization.OperationControllerMessages", typeof(LocalizationManager).Assembly) },
        //    { "BankAccountControllerMessages", new ResourceManager("BankingSystemPET.BL.Localization.BankAccountControllerMessages", typeof(LocalizationManager).Assembly) }
        //};
        //работал
        //private static ResourceManager _rm;
        private static readonly Dictionary<string, ResourceManager> _rm = new Dictionary<string, ResourceManager>();
        //static LocalizationManager()
        //{
        //    _rm = new ResourceManager(ChoseLocal(), typeof(LocalizationManager).Assembly);
        //}
        public static void ChoseLocal(string name)
        {
            if(!_rm.ContainsKey(name))
            {
                _rm[name] = new ResourceManager(name, typeof(LocalizationManager).Assembly);
            }
            //workalo _rm = new ResourceManager(name, typeof(LocalizationManager).Assembly);
        }
        public static string GetString(string resourceName, string key/*ResourceManager resourceManager, string key*/)
        {
            if (_rm.TryGetValue(resourceName, out var resourceManager))
            {
                return resourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? $"[{key}]";
            }
            return $"[{key}]";

            //workalo return _rm.GetString( name );
            //return resourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? $"[{key}]";
        }
        
        public static void SetCulture(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            //CultureInfo.CurrentUICulture = new CultureInfo(cultureName);
            //CultureInfo.CurrentCulture = new CultureInfo(cultureName);
        }
    }
}
