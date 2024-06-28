using Microsoft.AspNetCore.Identity;

namespace StudentInformationSystem.Authorization
{
    public class PasswordCheckService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public PasswordCheckService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (await _userManager.IsLockedOutAsync(user))
            {
                throw new Exception("User locked");

            }

            return await _userManager.CheckPasswordAsync(user, password);
        }
    }

}
