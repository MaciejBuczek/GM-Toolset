
using Character.API.Exceptions;

namespace Character.API.Features.GetCharacterById
{
    internal record GetCharacterByIdResult(Entities.Character Character);
    internal record GetCharacterByIdQuery(Guid Id) : IQuery<GetCharacterByIdResult>;

    internal class GetCharacterByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetCharacterByIdQuery, GetCharacterByIdResult>
    {
        public async Task<GetCharacterByIdResult> Handle(GetCharacterByIdQuery query, CancellationToken cancellationToken = default)
        {
            var character = await session.LoadAsync<Entities.Character>(query.Id, cancellationToken);

            return character is not null ? new GetCharacterByIdResult(character) : throw new CharacterNotFoundException(query.Id);
        }
    }
}
