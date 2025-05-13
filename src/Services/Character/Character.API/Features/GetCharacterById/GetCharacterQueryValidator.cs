namespace Character.API.Features.GetCharacterById
{
    internal class GetCharacterQueryValidator : AbstractValidator<GetCharacterByIdQuery>
    {
        public GetCharacterQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
