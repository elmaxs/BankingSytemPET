using BankingSystemPET.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using BankingSystemPET.BL.Utilities;

namespace BankingSystemPET.BL.Controller
{
    public class UserController : BaseController
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("BankingSystemPET.BL.Localization.UserControllerMessages", typeof(UserController).Assembly);
        public bool IsNewUser { get; set; } = false;
        public User User { get; set; }

        public UserController(int indef)
        {
            User = GetUser(indef);
            if (User == null)
            {
                User = new User(indef);
                IsNewUser = true;
                SetNewUserData();
            }
        }

        private void SetNewUserData()
        {
            Console.WriteLine(LocalizationManager.GetString(_resourceManager, "EnterName"));
            string name = Console.ReadLine();
            Console.WriteLine(LocalizationManager.GetString(_resourceManager, "EnterPassword"));
            string password = Console.ReadLine();
            Console.WriteLine(LocalizationManager.GetString(_resourceManager, "EnterResidence"));
            string placeResidence = Console.ReadLine();
            Console.WriteLine(LocalizationManager.GetString(_resourceManager, "EnterPhoneNumber"));
            string phoneNumber = Console.ReadLine();
            DateTime birthDate = DataTimeParse();

            SetNewUserData(name, password, placeResidence, phoneNumber, birthDate);
        }

        private void SetNewUserData(string name, string password, string placeResidence, string phoneNumber, DateTime? birthDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Name cant be null or empty", nameof(name));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Password cant be null or empty", nameof(password));
            if (string.IsNullOrWhiteSpace(placeResidence))
                throw new ArgumentNullException("Place residence cant be null or empty", nameof(placeResidence));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException("Phone number cant be null or empty", nameof(phoneNumber));
            if (birthDate == null || birthDate == DateTime.MinValue)
                throw new ArgumentNullException("Birth date cant be null or minimum value", nameof(birthDate));

            User.UserName = name;
            User.Password = password;
            User.PlaceResidence = placeResidence;
            User.PhoneNumber = phoneNumber;
            User.BirthDate = birthDate;
            Save();
        }

        private DateTime DataTimeParse()
        {
            Console.WriteLine(LocalizationManager.GetString(_resourceManager, "EnterBirthDate"));
            while (true)
            {
                if(DateTime.TryParse(Console.ReadLine(), out var date))
                {
                    return date;
                }
                else
                    Console.WriteLine(LocalizationManager.GetString(_resourceManager, "InvalidDataTimeParse"));
            }
        }

        private User GetUser(int indef)
        {
            List<User> users = Load<User>();
            //затестим че тут вернет если налл или длина 0 без User? в акаунт контролере с ?
            //if (users == null || users.Count == 0)
            //    return null;
            //else
                return users.FirstOrDefault(i => i.Indef == indef);
        }

        private void Save()
        {
            List<User> users = Load<User>();
            User _currentUser = users.FirstOrDefault(i => i.Indef == User.Indef);

            if (_currentUser == null)
                users.Add(User);
            else
                _currentUser = User;
            base.Save(users);
        }
    }
}
