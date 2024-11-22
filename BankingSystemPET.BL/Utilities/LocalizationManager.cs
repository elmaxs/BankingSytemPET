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
        private static ResourceManager _resourceManager = new ResourceManager("BankingSystemPET.BL.Localization.Messages", typeof(LocalizationManager).Assembly);

        public static string GetString(ResourceManager resourceManager, string key)
        {
            return resourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? $"[{key}]";
        }

        public static void SetCulture(string cultureName)
        {
            CultureInfo.CurrentUICulture = new CultureInfo(cultureName);
            CultureInfo.CurrentCulture = new CultureInfo(cultureName);
        }
    }
}
