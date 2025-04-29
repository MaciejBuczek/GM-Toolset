namespace Character.API.Features.Shared.DTOs
{
    public abstract record CharacterBaseRequest(Guid UserId, Guid SchemaId, string Name, string Description, ICollection<Statistic> Statistics);
}
