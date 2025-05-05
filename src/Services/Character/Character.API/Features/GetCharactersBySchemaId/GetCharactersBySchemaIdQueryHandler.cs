using Common.Exceptions;

namespace Character.API.Features.GetCharactersBySchemaId
{
    public record GetCharactersBySchemaIdResult(IEnumerable<Entities.Character> Characters);
    public record GetCharactersBySchemaIdQuery(Guid Id) : IQuery<GetCharactersBySchemaIdResult>;
    public class GetCharactersBySchemaIdQueryHandler(IDocumentSession session) : IQueryHandler<GetCharactersBySchemaIdQuery, GetCharactersBySchemaIdResult>
    {
        public async Task<GetCharactersBySchemaIdResult> Handle(GetCharactersBySchemaIdQuery query, CancellationToken cancellationToken = default)
        {
            var characters = await session.Query<Entities.Character>().Where(c => c.SchemaId.Equals(query.Id)).ToListAsync(cancellationToken);
            return characters is null || characters.IsEmpty()
                ? throw new NotFoundException($"Characters not found - SchemaId: {query.Id}")
                : new GetCharactersBySchemaIdResult(characters);
        }
    }
}
