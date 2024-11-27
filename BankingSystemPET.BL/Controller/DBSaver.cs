using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystemPET.BL.Controller
{
    public static class DBSaver 
    {
        public static List<T> Load<T>() where T : class
        {
            using(var db = new BankingSystemContext())
            {
                var result = db.Set<T>().Where(t => true).ToList();
                return result;
            }
        }

        public static void Save<T>(T item) where T : class
        {
            using(var db = new BankingSystemContext())
            {
                db.Set<T>().AddRange(item);
                db.SaveChanges();
            }
        }
    }
}
