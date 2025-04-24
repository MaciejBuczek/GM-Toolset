namespace Character.API.Features.CreateCharacter
{
    public class CreateCharacterCommandValidator : AbstractValidator<CreateCharacterCommand>
    {
        public CreateCharacterCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
            RuleFor(x => x.SchemaId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Statistics).NotEmpty();
            RuleForEach(x => x.Statistics).SetValidator(new StatisticValidator());
        }
    }
}
