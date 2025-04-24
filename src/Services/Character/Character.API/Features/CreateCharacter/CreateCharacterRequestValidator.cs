namespace Character.API.Features.CreateCharacter
{
    public class CreateCharacterRequestValidator : AbstractValidator<CreateCharacterRequest>
    {
        public CreateCharacterRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
            RuleFor(x => x.SchemaId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Statistics).NotEmpty();
            RuleForEach(x => x.Statistics).SetValidator(new StatisticValidator());
        }
    }
}
