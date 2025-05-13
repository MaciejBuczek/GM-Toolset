namespace Character.API.Data
{
    internal interface ICharacterRepository
    {
        Task<Entities.Character?> GetCharacterByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Entities.Character>> GetCharacterByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<IEnumerable<Entities.Character>> GetCharacterBySchemaIdAsync(Guid schemaId, CancellationToken cancellationToken);
        Task<bool> UpdateCharacterAsync(Entities.Character character, CancellationToken cancellationToken);
        Task<bool> DeleteCharacterAsync(Entities.Character character, CancellationToken cancellationToken);
        Task<Guid> CreateCharacterAsync(Entities.Character character, CancellationToken cancellationToken);
    }
}
