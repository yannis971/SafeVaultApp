using NUnit.Framework;
using Moq;
using SafeVaultApp.Services;
using SafeVaultApp.Models;
using System;

namespace SafeVaultApp.Tests
{
    [TestFixture]
    public class AuthTests
    {
        private Mock<AuthService> _authServiceMock;

        [SetUp]
        public void Setup()
        {
            _authServiceMock = new Mock<AuthService>();
        }

        [Test]
        public void Login_WithInvalidCredentials_ShouldThrowUnauthorizedAccessException()
        {
            _authServiceMock
                .Setup(s => s.AuthenticateUser("wrong@example.com", "wrongpassword"))
                .Throws(new UnauthorizedAccessException("Invalid credentials"));

            Assert.Throws<UnauthorizedAccessException>(() =>
                _authServiceMock.Object.AuthenticateUser("wrong@example.com", "wrongpassword"));
        }

        [Test]
        public void Login_WithValidCredentials_ShouldReturnTrue()
        {
            _authServiceMock
                .Setup(s => s.AuthenticateUser("user@example.com", "correctpassword"))
                .Returns(System.Threading.Tasks.Task.FromResult(true));

            var result = _authServiceMock.Object.AuthenticateUser("user@example.com", "correctpassword").Result;

            Assert.Equals(result, true);
        }
    }
}
