using LegacyApp.Repository;
using LegacyApp.UnitTests.Helpers;
using LegacyApp.Service;
using LegacyApp.Common;
using Moq;
using LegacyApp.Model;
using FluentValidation;

namespace LegacyApp.UnitTests
{
    public class UserServiceTests
    {
        private readonly UserService _sut;
        private Mock<IClientRepository> _clientRepositoryMock;
        private Mock<IUserCreditService> _userCreditServiceMock;
        private Mock<ICreditVerifierService> _creditVerifierServiceMock;

        public UserServiceTests()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _userCreditServiceMock = new Mock<IUserCreditService>();
           _creditVerifierServiceMock = new Mock<ICreditVerifierService>();
            var userDataAccessWrapperMock = new Mock<IUserDataAccessWrapper>();
            _sut = new UserService(_clientRepositoryMock.Object, _creditVerifierServiceMock.Object, userDataAccessWrapperMock.Object);
        }

        [Fact]
        public async Task GivenImportantClient_WhenUserHasSufficientCredit_ThenCreateUser()
        {
            // Arrange
            var expectedUser = UserServiceTestData.GetValidUser();
            _creditVerifierServiceMock.Setup(s => s.Verify("ImportantClient", expectedUser.Firstname, expectedUser.Surname, expectedUser.DateOfBirth))
                .Returns(600);

            _clientRepositoryMock.Setup(s => s.GetByIdAsync(10)).ReturnsAsync(
                new Client
                {
                    Id = 10,
                    ClientStatus = ClientStatus.Platinum,
                    Name = "ImportantClient"
                });

            // Act
            var actualUser = await _sut.AddUser(expectedUser.Firstname, expectedUser.Surname, expectedUser.EmailAddress, expectedUser.DateOfBirth, expectedUser.ClientId);

            // Assert
            Assert.Equal(expectedUser.Firstname, actualUser.Firstname);
            Assert.Equal(expectedUser.EmailAddress, actualUser.EmailAddress);
            Assert.Equal(expectedUser.DateOfBirth, actualUser.DateOfBirth);
            Assert.NotNull(actualUser.Client);
        }

        [Fact]
        public async Task GivenImportantClient_WhenUserHasInsufficientCredit_ThenThrowException()
        {
            // Arrange
            var user = UserServiceTestData.GetValidUser();
            _creditVerifierServiceMock.Setup(s => s.Verify("ImportantClient", user.Firstname, user.Surname, user.DateOfBirth))
                .Returns(400);

            _clientRepositoryMock.Setup(s => s.GetByIdAsync(10)).ReturnsAsync(
                new Client
                {
                    Id = 10,
                    ClientStatus = ClientStatus.Platinum,
                    Name = "ImportantClient"
                });
            var exceptionType = typeof(InvalidOperationException);
            var expectedMessage = ValidationMessage.InsufficientCredit;

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
        public async Task GivenVeryImportantClient_WhenUserHasSufficientCredit_ThenCreateUser()
        {
            // Arrange
            var expectedUser = UserServiceTestData.GetValidUser();
            _creditVerifierServiceMock.Setup(s => s.Verify("VeryImportantClient", expectedUser.Firstname, expectedUser.Surname, expectedUser.DateOfBirth))
                .Returns(-1);   // no limit

            _clientRepositoryMock.Setup(s => s.GetByIdAsync(10)).ReturnsAsync(
                new Client
                {
                    Id = 10,
                    ClientStatus = ClientStatus.Platinum,
                    Name = "VeryImportantClient"
                });

            // Act
            var actualUser = await _sut.AddUser(expectedUser.Firstname, expectedUser.Surname, expectedUser.EmailAddress, expectedUser.DateOfBirth, expectedUser.ClientId);

            // Assert
            Assert.Equal(expectedUser.Firstname, actualUser.Firstname);
            Assert.Equal(expectedUser.EmailAddress, actualUser.EmailAddress);
            Assert.Equal(expectedUser.DateOfBirth, actualUser.DateOfBirth);
            Assert.NotNull(actualUser.Client);
        }

        [Fact]
        public async Task GivenValidInput_WhenUserUnderAge_ThenThrowError()
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
        public async Task GivenInvalidName_ThenThrowException()
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
        public async Task GivenInvalidEmail_ThenThrowException()
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