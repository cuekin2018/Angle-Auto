using LegacyApp.Service;
using Moq;

namespace LegacyApp.UnitTests
{
    public class CreditVerifierServiceTests
    {
        private Mock<IUserCreditService> _userCreditServiceMock;
        private readonly ICreditVerifierService _sut;

        public CreditVerifierServiceTests()
        {
            _userCreditServiceMock = new Mock<IUserCreditService>();
            _sut = new CreditVerifierService(_userCreditServiceMock.Object);
        }

        [Fact]
        public void GivenVeryImportantClient_ThenReturnsNoCreditLimit()
        {
            // Arrange
            string clientName = "VeryImportantClient";
            string firstname = "ron";
            string surname = "stanley";
            DateTime dateOfBirth = new DateTime(year: 1990, month: 6, day: 1);
            int expectedLimit = -1;  // no limit
            int initialLimit = -1;

            _userCreditServiceMock.Setup(s => s.GetCreditLimit(firstname, surname, dateOfBirth)).Returns(initialLimit);

            // Act
            var actualLimit = _sut.Verify(clientName, firstname, surname, dateOfBirth);

            // Assert
            Assert.Equal(expectedLimit, actualLimit);
        }

        [Fact]
        public void GivenImportantClient_ThenReturnsTwiceCreditLimit()
        {
            // Arrange
            string clientName = "ImportantClient";
            string firstname = "ron";
            string surname = "stanley";
            DateTime dateOfBirth = new DateTime(year: 1990, month: 6, day: 1);
            int expectedLimit = 600;
            int initialLimit = 300;

            _userCreditServiceMock.Setup(s => s.GetCreditLimit(firstname, surname, dateOfBirth)).Returns(initialLimit);

            // Act
            var actualLimit = _sut.Verify(clientName, firstname, surname, dateOfBirth);

            // Assert
            Assert.Equal(expectedLimit, actualLimit);
        }

        [Fact]
        public void GivenRegularClient_ThenReturnsCreditLimit()
        {
            // Arrange
            string clientName = "RegularClient";
            string firstname = "ron";
            string surname = "stanley";
            DateTime dateOfBirth = new DateTime(year: 1990, month: 6, day: 1);
            int expectedLimit = 200;
            int initialLimit = 200;

            _userCreditServiceMock.Setup(s => s.GetCreditLimit(firstname, surname, dateOfBirth)).Returns(initialLimit);

            // Act
            var actualLimit = _sut.Verify(clientName, firstname, surname, dateOfBirth);

            // Assert
            Assert.Equal(expectedLimit, actualLimit);
        }
    }
}
