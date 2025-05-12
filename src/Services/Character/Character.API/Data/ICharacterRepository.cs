namespace Character.API.Data
{
    internal interface ICharacterRepository
    {
        Task<Entities.Character?> GetCharacterById(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Entities.Character>> GetCharacterByUserId(Guid userId, CancellationToken cancellationToken);
        Task<IEnumerable<Entities.Character>> GetCharacterBySchemaId(Guid schemaId, CancellationToken cancellationToken);
        Task<bool> UpdateCharacter(Entities.Character character, CancellationToken cancellationToken);
        Task<bool> DeleteCharacter(Entities.Character character, CancellationToken cancellationToken);
        Task<Guid> CreateCharacter(Entities.Character character, CancellationToken cancellationToken);
    }
}
