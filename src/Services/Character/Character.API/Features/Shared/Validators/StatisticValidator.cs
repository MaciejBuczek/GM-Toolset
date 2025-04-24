namespace Character.API.Features.Shared.Validators
{
    public class StatisticValidator : AbstractValidator<Statistic>
    {
        public StatisticValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
            RuleFor(x => x.Value).NotEmpty().MaximumLength(64);
        }
    }
}
