using EDriveRent.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public class User : IUser
    {
        private string firstName;
        private string lastName;
        private string drivingLicenseNumber;

        public User(string firstName, string lastName, string drivingLicenseNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DrivingLicenseNumber = drivingLicenseNumber;
        }

        public string FirstName 
        {
            get => firstName;
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("FirstName cannot be null or whitespace!");
                }
                firstName = value; 
            } 
        }

        public string LastName 
        { 
            get => lastName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("LastName cannot be null or whitespace!");
                }
                lastName = value;
            }
        }

        public string DrivingLicenseNumber 
        { 
            get => drivingLicenseNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Driving license number is required!");
                }
                drivingLicenseNumber = value;
            }
        }

        public double Rating { get; private set; }

        public bool IsBlocked { get; private set; }

        public void IncreaseRating()
        {
            Rating += 0.5;
            if (Rating > 10)
            {
                Rating = 10;
            }
        }

        public void DecreaseRating()
        {
            Rating -= 2;
            if (Rating < 0)
            {
                Rating = 0;
                IsBlocked = true;
            }
        }

        public override string ToString() => $"{FirstName} {LastName} Driving license: {DrivingLicenseNumber} Rating: {Rating}";
    }
}
