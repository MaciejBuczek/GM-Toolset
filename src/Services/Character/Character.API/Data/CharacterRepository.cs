namespace Character.API.Data
{
    internal class CharacterRepository(IDocumentSession session) : ICharacterRepository
    {
        public async Task<Guid> CreateCharacterAsync(Entities.Character character, CancellationToken cancellationToken)
        {
            session.Store(character);
            await session.SaveChangesAsync(cancellationToken);

            return character.Id;
        }

        public async Task<bool> DeleteCharacterAsync(Entities.Character character, CancellationToken cancellationToken)
        {
            session.Delete(character);
            await session.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<Entities.Character?> GetCharacterByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var character = await session.LoadAsync<Entities.Character>(id, cancellationToken);

            return character;
        }

        public async Task<IEnumerable<Entities.Character>> GetCharacterBySchemaIdAsync(Guid schemaId, CancellationToken cancellationToken)
        {
            var characters = await session.Query<Entities.Character>()
                .Where(c => c.SchemaId.Equals(schemaId))
                .ToListAsync(cancellationToken);

            return characters;
        }

        public async Task<IEnumerable<Entities.Character>> GetCharacterByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var characters = await session.Query<Entities.Character>()
                .Where(c => c.UserId.Equals(userId))
                .ToListAsync(cancellationToken);

            return characters;
        }

        public async Task<bool> UpdateCharacterAsync(Entities.Character character, CancellationToken cancellationToken)
        {
            session.Update(character);
            await session.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
