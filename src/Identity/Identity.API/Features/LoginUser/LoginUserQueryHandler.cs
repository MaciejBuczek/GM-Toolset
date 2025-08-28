namespace Identity.API.Features.LoginUser
{
    internal record LoginUserResult(string Token);
    internal record LoginUserQuery(string Email, string Password) : IQuery<LoginUserResult>;

    internal class LoginUserQueryHandler(IAuthService repository) : IQueryHandler<LoginUserQuery, LoginUserResult>
    {
        public async Task<LoginUserResult> Handle(LoginUserQuery Query, CancellationToken cancellationToken)
        {
            try
            {
                var result = await repository.LoginUserAsync(Query.Email, Query.Password, cancellationToken);

                if (!result)
                {
                    throw new BadRequestException("Invalid email or password");
                }

                var jwtToken = repository.GenerateTokenString(Query.Email, Query.Password);

                return new LoginUserResult(jwtToken);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }

        }
    }
}
