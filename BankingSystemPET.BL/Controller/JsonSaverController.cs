using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankingSystemPET.BL.Controller
{
    public class JsonSaverController : IDataSaver
    {
        public List<T> Load<T>() where T : class
        {
            string fileName = typeof(T).Name;
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                var item = JsonSerializer.Deserialize<List<T>>(fs);
                return item;
            }
        }

        public void Save<T>(List<T> item) where T : class
        {
            string fileName = typeof(T).Name;
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, item);
            }
        }
    }
}
