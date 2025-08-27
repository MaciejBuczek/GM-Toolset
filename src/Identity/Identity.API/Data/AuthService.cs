namespace Identity.API.Data
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> RegisterUserAsync(IdentityUser user, string password, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }
        public async Task<bool> LoginUserAsync(string email, string password, CancellationToken cancellationToken)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);

            if (identityUser is null)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(identityUser, password);

        }
    }
}
