using BankingSystemPET.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystemPET.BL.Controller
{
    public class UserController : BaseController
    {
        public bool IsNewUser { get; set; } = false;
        public User User { get; set; }

        public UserController(int indef)
        {
            User = GetUser(indef);
            if(User == null)
            {
                User = new User(indef);
                IsNewUser = true;
                SetNewUserData();
            }
        }

        private void SetNewUserData()
        {
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your new password");
            string password = Console.ReadLine();
        }

        private User GetUser(int indef)
        {
            List<User> users = Load<User>();

            if (users == null || users.Count == 0)
                return null;
            else
                return users.FirstOrDefault(i => i.Indef == indef);
        }

        private void Save()
        {
            List<User> users = Load<User>();
            users.Add(User);
            base.Save(users);
        }
    }
}
