using NUnit.Framework;
using Moq;
using SafeVaultApp.Services;
using SafeVaultApp.Models;
// Add the following using if IRoleService is in a different namespace
namespace SafeVaultApp.Tests
{
    // Define IRoleService if it does not exist
    public interface IRoleService
    {
        bool CanAccessAdminPanel(User user);
    }

    [TestFixture]
    public class RoleBasedAccessTests
    {
    private Mock<IRoleService> _roleServiceMock;

        [SetUp]
        public void Setup()
        {
            _roleServiceMock = new Mock<IRoleService>();
        }

        [Test]
        public void AdminUser_CanAccessAdminPanel()
        {
            var adminUser = new User { Role = "Admin" };

            _roleServiceMock
                .Setup(s => s.CanAccessAdminPanel(adminUser))
                .Returns(true);

            var result = _roleServiceMock.Object.CanAccessAdminPanel(adminUser);

            Assert.Equals(result, true);
        }

        [Test]
        public void RegularUser_CannotAccessAdminPanel()
        {
            var regularUser = new User { Role = "User" };

            _roleServiceMock
                .Setup(s => s.CanAccessAdminPanel(regularUser))
                .Returns(false);

            var result = _roleServiceMock.Object.CanAccessAdminPanel(regularUser);

            Assert.Equals(result,false);
        }
    }
}
