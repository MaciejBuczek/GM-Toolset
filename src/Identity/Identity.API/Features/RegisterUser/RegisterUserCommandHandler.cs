
using Common.Exceptions;

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

            try
            {
                var result = await repository.RegisterUserAsync(identityUser, command.Password, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
            //catch (DocumentAlreadyExistsException)
            //{
            //    throw new BadRequestException("Character with this id already exists");
            //}

            return new RegisterUserResult(true);
        }
    }
}
