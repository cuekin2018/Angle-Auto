using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyApp.Model;

namespace LegacyApp.UnitTests.Helpers
{
    public static class UserServiceTestData
    {
        public static User GetValidUser() 
        { 
            return new User
            {
                DateOfBirth = new DateTime(year: 1990, month: 6, day: 1),
                EmailAddress = "ricky.davis@gmail.com",
                Firstname = "Ricky",
                Surname = "Davis",
                ClientId = 10
            }; 
        }

        public static User GetUserNotInAdulthood()
        {
            return new User
            {
                DateOfBirth = new DateTime(year: 2008, month: 6, day: 1),
                EmailAddress = "ricky.davis@gmail.com",
                Firstname = "Ricky",
                Surname = "Davis",
                ClientId = 10
            };
        }

        public static User GetUserWithInvalidEmail()
        {
            return new User
            {
                DateOfBirth = new DateTime(year: 1990, month: 6, day: 1),
                EmailAddress = "rickydavis-gmailcom",
                Firstname = "Ricky",
                Surname = "Davis",
                ClientId = 10
            };
        }

        public static User GetUserWithInvalidName()
        {
            return new User
            {
                DateOfBirth = new DateTime(year: 1990, month: 6, day: 1),
                EmailAddress = "ricky.davis@gmail.com",
                Firstname = "",
                Surname = "Davis",
                ClientId = 10
            };
        }
    }
}
