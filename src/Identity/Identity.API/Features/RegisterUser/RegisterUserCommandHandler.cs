
namespace Identity.API.Features.RegisterUser
{
    internal record RegisterUserResult(bool Success);
    internal record RegisterUserCommand(string Username, string Email, string Password, string ConfirmPassword) : ICommand<RegisterUserResult>;

    internal class RegisterUserCommandHandler(IAuthService repository) : ICommandHandler<RegisterUserCommand, RegisterUserResult>
    {
        public async Task<RegisterUserResult> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var identityUser = new IdentityUser
            {
                UserName = command.Username,
                Email = command.Email
            };

            var result = await repository.RegisterUserAsync(identityUser, command.Password, cancellationToken);

            if (result.Errors.Any())
            {
                throw new ValidationException(result.Errors.Select(e => new ValidationFailure(e.Code, e.Description)));
            }

            return result.Succeeded ? new RegisterUserResult(true) : throw new InternalServerException("Something went wrong.");
        }
    }
}
