namespace Identity.API.Data
{
    public interface IAuthService
    {
        Task<bool> LoginUserAsync(string email, string password, CancellationToken cancellationToken);
        Task<bool> RegisterUserAsync(IdentityUser user, string password, CancellationToken cancellationToken);
    }
}