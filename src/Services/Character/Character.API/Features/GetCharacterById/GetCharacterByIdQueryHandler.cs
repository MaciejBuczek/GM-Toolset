
namespace Character.API.Features.GetCharacterById
{
    public record GetCharacterByIdResult(Entities.Character Character);
    public record GetCharacterByIdQuery(Guid Id) : IQuery<GetCharacterByIdResult>;

    public class GetCharacterByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetCharacterByIdQuery, GetCharacterByIdResult>
    {
        public async Task<GetCharacterByIdResult> Handle(GetCharacterByIdQuery query, CancellationToken cancellationToken = default)
        {
            var character = await session.LoadAsync<Entities.Character>(query.Id, cancellationToken);

            return character is not null ? new GetCharacterByIdResult(character) : throw new ApplicationException("character not found");
        }
    }
}
