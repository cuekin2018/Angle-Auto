using LegacyApp.Service;
using System;
using System.Collections.Generic;

namespace LegacyApp.Common
{
    public abstract class ClientUserMapper : IDisposable
    {
        protected IUserCreditService _userCreditService;

        public ClientUserMapper(IUserCreditService userCreditService)
        {
            _userCreditService = userCreditService;
        }

        public void Dispose()
        {
            _userCreditService = null;
        }

        public virtual int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth)
        {
            return -1;
        }
    }

    public class VeryImportantClient : ClientUserMapper
    {
        public VeryImportantClient(IUserCreditService userCreditService)
            : base(userCreditService)
        {
        }

        public override int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth)
        {
            // Skip credit check
            return base.GetCreditLimit(firstname, surname, dateOfBirth);
        }
    }

    public class ImportantClient : ClientUserMapper
    {
        public ImportantClient(IUserCreditService userCreditService)
           : base(userCreditService)
        {
        }

        public override int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth)
        {
            // Do credit check and double credit limit
            var creditLimit = _userCreditService.GetCreditLimit(firstname, surname, dateOfBirth);
            creditLimit = creditLimit * 2;

            return creditLimit;
        }
    }

    public class RegularClient : ClientUserMapper
    {
        public RegularClient(IUserCreditService userCreditService)
          : base(userCreditService)
        {
        }

        public override int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth)
        {
            // Do credit check
            var creditLimit = _userCreditService.GetCreditLimit(firstname, surname, dateOfBirth);

            return creditLimit;
        }
    }
}
