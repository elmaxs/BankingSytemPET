using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystemPET.BL.Model
{
    public class User
    {
        public int ID { get; set; }

        public string? UserName { get; set; }

        public int Indef { get; set; }

        public string? Password { get; set; }

        public string? PlaceResidence { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        public int CurrectAge { get { return DateTime.Now.Year - BirthDate.Value.Year; } }

        public User(int indef)
        {
            if (Indef <= 0)
                throw new ArgumentException("Indef cant be equal or less 0");
            Indef = indef;
        }

        public User(string userName, string password, string placeResidence, string phoneNumber, DateTime? birthDate)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("User name cant be null or empty", nameof(userName));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Password cant be null or empty", nameof(password));
            if (string.IsNullOrWhiteSpace(placeResidence))
                throw new ArgumentNullException("Place residence cant be null or empty", nameof(placeResidence));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentNullException("Phone number cant be null or empty", nameof(phoneNumber));
            if (birthDate == null || birthDate == DateTime.MinValue)
                throw new ArgumentNullException("Birth date cant be null or minimum value", nameof(birthDate));

            UserName = userName;
            Password = password;
            PlaceResidence = placeResidence;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
        }

        public override string ToString()
        {
            return $"{UserName}, {Indef}, {CurrectAge}";
        }
    }
}
