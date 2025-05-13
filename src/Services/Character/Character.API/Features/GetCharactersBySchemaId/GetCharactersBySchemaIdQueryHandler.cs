using Common.Exceptions;

namespace Character.API.Features.GetCharactersBySchemaId
{
    internal record GetCharactersBySchemaIdResult(IEnumerable<Entities.Character> Characters);
    internal record GetCharactersBySchemaIdQuery(Guid Id) : IQuery<GetCharactersBySchemaIdResult>;

    internal class GetCharactersBySchemaIdQueryHandler(ICharacterRepository repository) : IQueryHandler<GetCharactersBySchemaIdQuery, GetCharactersBySchemaIdResult>
    {
        public async Task<GetCharactersBySchemaIdResult> Handle(GetCharactersBySchemaIdQuery query, CancellationToken cancellationToken = default)
        {
            var characters = await repository.GetCharacterBySchemaIdAsync(query.Id, cancellationToken);
            return characters.IsEmpty()
                ? throw new NotFoundException($"Characters not found - SchemaId: {query.Id}")
                : new GetCharactersBySchemaIdResult(characters);
        }
    }
}
