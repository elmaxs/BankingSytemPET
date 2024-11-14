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

        public bool IsNewUser { get; set; } = false;

        public string? UserName { get; set; }

        public int Indef { get; set; }

        public string? Password { get; set; }

        public string? PlaceResidence { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        public int CurrectAge { get { return DateTime.Now.Year - BirthDate.Value.Year; } }

        public User(string userName, int indef, string password, string placeResidence, string phoneNumber, DateTime? birthDate)
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
            Indef = indef;
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
