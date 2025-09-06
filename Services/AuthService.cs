
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SafeVaultApp.Helpers;

namespace SafeVaultApp.Services
{
    public class AuthService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return false;

            var passwordHash = user.PasswordHash;
            return PasswordHelper.VerifyPassword(password, passwordHash);
        }
    }
}
