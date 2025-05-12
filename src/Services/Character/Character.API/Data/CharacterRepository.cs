namespace Character.API.Data
{
    internal class CharacterRepository(IDocumentSession session) : ICharacterRepository
    {
        public async Task<Guid> CreateCharacter(Entities.Character character, CancellationToken cancellationToken)
        {
            session.Store(character);
            await session.SaveChangesAsync(cancellationToken);

            return character.Id;
        }

        public async Task<bool> DeleteCharacter(Entities.Character character, CancellationToken cancellationToken)
        {
            session.Delete(character);
            await session.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<Entities.Character?> GetCharacterById(Guid id, CancellationToken cancellationToken)
        {
            var character = await session.LoadAsync<Entities.Character>(id, cancellationToken);

            return character;
        }

        public async Task<IEnumerable<Entities.Character>> GetCharacterBySchemaId(Guid schemaId, CancellationToken cancellationToken)
        {
            var characters = await session.Query<Entities.Character>()
                .Where(c => c.SchemaId.Equals(schemaId))
                .ToListAsync();

            return characters;
        }

        public async Task<IEnumerable<Entities.Character>> GetCharacterByUserId(Guid userId, CancellationToken cancellationToken)
        {
            var characters = await session.Query<Entities.Character>()
                .Where(c => c.UserId.Equals(userId))
                .ToListAsync();

            return characters;
        }

        public async Task<bool> UpdateCharacter(Entities.Character character, CancellationToken cancellationToken)
        {
            session.Update(character);
            await session.SaveChangesAsync();

            return true;
        }
    }
}
