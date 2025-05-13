namespace Character.API.Features.GetCharactersBySchemaId
{
    internal class GetCharactersBySchemaIdQueryValidator : AbstractValidator<GetCharactersBySchemaIdQuery>
    {
        public GetCharactersBySchemaIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
