using System;
using System.ComponentModel.DataAnnotations;

namespace LegacyApp.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string EmailAddress { get; set; }

        public int ClientId { get; set; }

        public bool HasCreditLimit
        {
            get
            {
                return CreditLimit > 0;
            }

        }

        public int CreditLimit { get; set; }

        public Client Client { get; set; }
    }
}
