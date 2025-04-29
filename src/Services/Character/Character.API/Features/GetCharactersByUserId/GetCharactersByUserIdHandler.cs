namespace Character.API.Features.GetCharactersByUserId
{
    public record GetCharactersByUserIdResult(IEnumerable<Entities.Character> Characters);
    public record GetCharactersByUserIdQuery(Guid Id) : IQuery<GetCharactersByUserIdResult>;
    public class GetCharactersByUserIdQueryHandler(IDocumentSession session) : IQueryHandler<GetCharactersByUserIdQuery, GetCharactersByUserIdResult>
    {
        public async Task<GetCharactersByUserIdResult> Handle(GetCharactersByUserIdQuery query, CancellationToken cancellationToken = default)
        {
            var characters = await session.Query<Entities.Character>().Where(c => c.UserId.Equals(query.Id)).ToListAsync(cancellationToken);
            return characters is null || characters.IsEmpty()
                ? throw new ApplicationException("Characters not found")
                : new GetCharactersByUserIdResult(characters);
        }
    }
}
