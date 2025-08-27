namespace Identity.API.Features.RegisterUser
{
    internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            //Include(new UserBaseRequestValidator());
        }
    }
}
