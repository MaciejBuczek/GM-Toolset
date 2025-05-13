namespace Character.API.Features.GetCharactersByUserId
{
    internal class GetCharactersByUserIdQueryValidator : AbstractValidator<GetCharactersByUserIdQuery>
    {
        public GetCharactersByUserIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
