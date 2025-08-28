using Identity.API.Features.LoginUser;

namespace Identity.API.Data
{
    public interface IAuthService
    {
        string GenerateTokenString(string email, string password);
        Task<bool> LoginUserAsync(string email, string password, CancellationToken cancellationToken);
        Task<IdentityResult> RegisterUserAsync(IdentityUser user, string password, CancellationToken cancellationToken);
    }
}