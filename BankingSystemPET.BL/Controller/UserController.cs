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

        public UserController()
        {
            LocalizationManager.ChoseLocal("BankingSystemPET.BL.Localization.UserControllerMessages");
            Console.WriteLine("Enter your indef");
            new UserController(int.Parse(Console.ReadLine()));
        }

        public UserController(int indef)
        {
            if (indef <= 0)
                throw new ArgumentException("Indef cant be less or equal 0", nameof(indef));

            User = GetUser(indef);
            if (User == null)
            {
                User = new User(indef);
                IsNewUser = true;
                SetNewUserData();
                Save();
            }
            else
                Console.WriteLine(User);
        }

        private void SetNewUserData()
        {
            Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.UserControllerMessages", "NotRegistered"));
            Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.UserControllerMessages", "EnterName"));
            string name = Console.ReadLine();
            Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.UserControllerMessages", "EnterPassword"));
            string password = Console.ReadLine();
            Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.UserControllerMessages", "EnterResidence"));
            string placeResidence = Console.ReadLine();
            Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/"BankingSystemPET.BL.Localization.UserControllerMessages", "EnterPhoneNumber"));
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
        }

        private DateTime DataTimeParse()
        {
            Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.UserControllerMessages", "EnterBirthDate"));
            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out var date))
                {
                    return date;
                }
                else
                    Console.WriteLine(LocalizationManager.GetString(/*_resourceManager,*/ "BankingSystemPET.BL.Localization.UserControllerMessages", "InvalidDataTimeParse"));
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
            // Загружаем список пользователей
            List<User> users = Load<User>() ?? new List<User>();

            // Пытаемся найти пользователя с таким же Indef
            int index = users.FindIndex(i => i.Indef == User.Indef);

            if (index == -1)
            {
                // Если пользователь не найден, добавляем нового
                users.Add(User);
            }
            else
            {
                // Если пользователь найден, заменяем его новым объектом
                users[index] = User;
            }

            // Сохраняем обновленный список пользователей
            base.Save(users);         
        }
    }
}
