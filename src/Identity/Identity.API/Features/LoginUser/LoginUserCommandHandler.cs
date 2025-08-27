
using Common.Exceptions;

namespace Identity.API.Features.LoginUser
{
    internal record LoginUserResult(bool Success);
    internal record LoginUserQuery(string Email, string Password) : IQuery<LoginUserResult>;

    internal class LoginUserQueryHandler(IAuthService repository) : IQueryHandler<LoginUserQuery, LoginUserResult>
    {
        public async Task<LoginUserResult> Handle(LoginUserQuery Query, CancellationToken cancellationToken)
        {
            try
            {
                var result = await repository.LoginUserAsync(Query.Email, Query.Password, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
            //catch (DocumentAlreadyExistsException)
            //{
            //    throw new BadRequestException("Character with this id already exists");
            //}

            return new LoginUserResult(true);
        }
    }
}
