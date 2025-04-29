namespace Character.API.Features.Shared.Validators
{
    public class CharacterBaseRequestValidator : AbstractValidator<CharacterBaseRequest>
    {
        public CharacterBaseRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
            RuleFor(x => x.SchemaId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Statistics).NotEmpty();
            RuleForEach(x => x.Statistics).SetValidator(new StatisticValidator());
        }
    }
}
