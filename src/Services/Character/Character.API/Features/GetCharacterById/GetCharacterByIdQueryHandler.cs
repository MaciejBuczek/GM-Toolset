﻿namespace Character.API.Features.GetCharacterById
{
    internal record GetCharacterByIdResult(Entities.Character Character);
    internal record GetCharacterByIdQuery(Guid Id) : IQuery<GetCharacterByIdResult>;

    internal class GetCharacterByIdQueryHandler(ICharacterRepository repository) : IQueryHandler<GetCharacterByIdQuery, GetCharacterByIdResult>
    {
        public async Task<GetCharacterByIdResult> Handle(GetCharacterByIdQuery query, CancellationToken cancellationToken = default)
        {
            var character = await repository.GetCharacterByIdAsync(query.Id, cancellationToken);

            return character is not null ? new GetCharacterByIdResult(character) : throw new CharacterNotFoundException(query.Id);
        }
    }
}
