namespace Character.API.Features.GetCharactersByUserId
{
    internal record GetCharactersByUserIdResult(IEnumerable<Entities.Character> Characters);
    internal record GetCharactersByUserIdQuery(Guid Id) : IQuery<GetCharactersByUserIdResult>;

    internal class GetCharactersByUserIdQueryHandler(ICharacterRepository repository) : IQueryHandler<GetCharactersByUserIdQuery, GetCharactersByUserIdResult>
    {
        public async Task<GetCharactersByUserIdResult> Handle(GetCharactersByUserIdQuery query, CancellationToken cancellationToken = default)
        {
            var characters = await repository.GetCharacterByUserIdAsync(query.Id, cancellationToken);
            return characters.IsEmpty()
                ? throw new NotFoundException($"Characters not found - UserId: {query.Id}")
                : new GetCharactersByUserIdResult(characters);
        }
    }
}
