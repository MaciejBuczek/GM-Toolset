namespace Character.API.Features.GetCharactersBySchemaId
{
    public record GetCharactersBySchemaIdResult(IEnumerable<Entities.Character> Characters);
    public record GetCharactersBySchemaIdQuery(Guid Id) : IQuery<GetCharactersBySchemaIdResult>;
    public class GetCharactersBySchemaIdQueryHandler(IDocumentSession session) : IQueryHandler<GetCharactersBySchemaIdQuery, GetCharactersBySchemaIdResult>
    {
        public async Task<GetCharactersBySchemaIdResult> Handle(GetCharactersBySchemaIdQuery request, CancellationToken cancellationToken = default)
        {
            var characters = await session.Query<Entities.Character>().Where(c => c.SchemaId.Equals(request.Id)).ToListAsync(cancellationToken);
            return characters is null || characters.IsEmpty()
                ? throw new ApplicationException("Characters not found")
                : new GetCharactersBySchemaIdResult(characters);
        }
    }
}
