using LegacyApp.Common;
using System;
using System.Collections.Generic;

namespace LegacyApp.Service
{
    public interface ICreditVerifierService
    {
        int Verify(string clientName, string firstname, string surname, DateTime dateOfBirth);
    }

    public class CreditVerifierService : ICreditVerifierService
    {
        private readonly IUserCreditService _userCreditService;
        public CreditVerifierService(IUserCreditService userCreditService)
        {
            _userCreditService = userCreditService;
        }

        public int Verify(string clientName, string firstname, string surname, DateTime dateOfBirth)
        {
            ClientUserMapper clientMapper = null;


            switch (clientName)
            {
                case var value when value == typeof(VeryImportantClient).Name:
                    clientMapper = new VeryImportantClient(_userCreditService);
                    break;
                case var value when value == typeof(ImportantClient).Name:
                    clientMapper = new ImportantClient(_userCreditService);
                    break;
                default:
                    clientMapper = new RegularClient(_userCreditService);
                    break;
            }

            var creditLimit = clientMapper.GetCreditLimit(firstname, surname, dateOfBirth);

            return creditLimit;
        }
    }
}
