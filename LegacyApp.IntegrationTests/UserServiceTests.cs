using LegacyApp.Repository;
using LegacyApp.IntegrationTests.Helpers;
using LegacyApp.Service;
using LegacyApp.Common;

namespace LegacyApp.IntegrationTests
{
	
	// NOTE: This integration tests requires a valid entry for the following:
	//			- ConnectionString for UserDataAccess 
	//			- EndpointConfiguration for UserCreditServiceClient
	//
    public class UserServiceTests
    {
        private readonly UserService _sut;

        public UserServiceTests()
        {
            IClientRepository clientRepository = new ClientRepository();
            var userCreditService = new UserCreditServiceClient();
            ICreditVerifierService creditVerifierService = new CreditVerifierService(userCreditService);
            IUserDataAccessWrapper userDataAccessWrapper = new UserDataAccessWrapper();
            _sut = new UserService(clientRepository, creditVerifierService, userDataAccessWrapper);
        }

        [Fact]
        public async Task GivenValidInput_WhenUserHasSufficientCredit_ThenCreateUser()
        {
            // Arrange
            var expectedUser = UserServiceTestData.GetValidUser();

            // Act
            var actualUser = await _sut.AddUser(expectedUser.Firstname, expectedUser.Surname, expectedUser.EmailAddress, expectedUser.DateOfBirth, expectedUser.ClientId);

            // Assert
            Assert.Equal(expectedUser.Firstname, actualUser.Firstname);
            Assert.Equal(expectedUser.EmailAddress, actualUser.EmailAddress);
            Assert.Equal(expectedUser.DateOfBirth, actualUser.DateOfBirth);
            Assert.NotNull(actualUser.Client);
        }

        [Fact]
        public async Task GivenValidInput_WhenUserNotInAdulthood_ThenThrowError()
        {
            // Arrange
            var user = UserServiceTestData.GetUserNotInAdulthood();
            var exceptionType = typeof(InvalidOperationException);
            var expectedMessage = ValidationMessage.UnderAge;


            // Act
            var ex = await Assert.ThrowsAsync(exceptionType, async () =>
            {
                await _sut.AddUser(user.Firstname, user.Surname, user.EmailAddress, user.DateOfBirth, user.ClientId);
            });

            // Assert
            Assert.Equal(expectedMessage, ex.Message);
            Assert.Equal(exceptionType, ex.GetType());
        }


        [Fact]
        public async Task GivenValidInput_WhenNameInvalid_ThenThrowException()
        {
            // Arrange
            var user = UserServiceTestData.GetUserWithInvalidName();
            var exceptionType = typeof(InvalidOperationException);
            var expectedMessage = ValidationMessage.FirstNameRequired;

            // Act
            var ex = await Assert.ThrowsAsync(exceptionType, async () =>
            {
                await _sut.AddUser(user.Firstname, user.Surname, user.EmailAddress, user.DateOfBirth, user.ClientId);
            });

            // Assert
            Assert.Equal(expectedMessage, ex.Message);
            Assert.Equal(exceptionType, ex.GetType());
        }

        [Fact]
        public async Task GivenInvalidInput_WhenInvalidEmail_ThenThrowException()
        {
            // Arrange
            var user = UserServiceTestData.GetUserWithInvalidEmail();
            var exceptionType = typeof(InvalidOperationException);
            var expectedMessage = ValidationMessage.EmailInvalid;

            // Act
            var ex = await Assert.ThrowsAsync(exceptionType, async () =>
            {
                await _sut.AddUser(user.Firstname, user.Surname, user.EmailAddress, user.DateOfBirth, user.ClientId);
            });

            // Assert
            Assert.Equal(expectedMessage, ex.Message);
            Assert.Equal(exceptionType, ex.GetType());
        }

    }
}