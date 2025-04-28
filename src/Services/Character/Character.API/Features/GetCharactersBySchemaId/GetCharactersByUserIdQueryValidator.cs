namespace Character.API.Features.GetCharactersBySchemaId
{
    public class GetCharactersBySchemaIdQueryValidator : AbstractValidator<GetCharactersBySchemaIdQuery>
    {
        public GetCharactersBySchemaIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
