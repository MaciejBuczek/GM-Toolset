namespace Character.API.Features.GetCharactersByUserId
{
    public class GetCharactersByUserIdQueryValidator : AbstractValidator<GetCharactersByUserIdQuery>
    {
        public GetCharactersByUserIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
