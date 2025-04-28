namespace Character.API.Features.GetCharacterById
{
    public class GetCharacterQueryValidator : AbstractValidator<GetCharacterByIdQuery>
    {
        public GetCharacterQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
