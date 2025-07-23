using System;
using System.Linq;
using System.Threading.Tasks;
using LegacyApp.Repository;
using LegacyApp.Model;
using LegacyApp.Common;

namespace LegacyApp.Service
{
    public class UserService
    {
        private readonly IClientRepository _clientRepository;
        private readonly UserValidator _userValidator;
        private readonly ICreditVerifierService _creditVerifierService;
        private readonly IUserDataAccessWrapper _userDataAccessWrapper;

        public UserService(IClientRepository clientRepository, ICreditVerifierService creditVerifierService, IUserDataAccessWrapper userDataAccessWrapper)
        {
            _clientRepository = clientRepository;
            _creditVerifierService = creditVerifierService;
            _userValidator = new UserValidator();
            _userDataAccessWrapper = userDataAccessWrapper;
        }

        public async Task<User> AddUser(string firstName, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            // create temp user
            var tempUser = new User
            {
                Firstname = firstName,
                Surname = surname,
                EmailAddress = email,
                DateOfBirth = dateOfBirth
            };

            // validate user
            var result = await _userValidator.ValidateAsync(tempUser);

            if (!result.IsValid)
            {
                var validationEntry = result.Errors.FirstOrDefault();
                throw new InvalidOperationException(validationEntry.ErrorMessage);
            }

            // get client
            var client = await _clientRepository.GetByIdAsync(clientId);

            // create user
            var user = new User
            {
                Client = client,
                DateOfBirth = tempUser.DateOfBirth,
                EmailAddress = tempUser.EmailAddress,
                Firstname = tempUser.Firstname,
                Surname = tempUser.Surname
            };


            var creditLimit = _creditVerifierService.Verify(client.Name, user.Firstname, user.Surname, user.DateOfBirth);
            user.CreditLimit = creditLimit;

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                throw new InvalidOperationException(ValidationMessage.InsufficientCredit);
            }

            _userDataAccessWrapper.AddUser(user);

            return user;
        }
    }
}
