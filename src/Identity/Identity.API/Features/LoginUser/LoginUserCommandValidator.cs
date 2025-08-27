namespace Identity.API.Features.LoginUser
{
    internal class LoginUserCommandValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserCommandValidator()
        {
            //Include(new UserBaseRequestValidator());
        }
    }
}
