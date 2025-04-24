namespace Character.API.Features.CreateCharacter
{
    public record CreateCharacterResult(Guid CharacterId);
    public record CreateCharacterCommand(Guid UserId, Guid SchemaId, string Name, string Description, IEnumerable<Statistic> Statistics) : ICommand<CreateCharacterResult>;

    public class CreateCharacterCommandHandler(IDocumentSession session) : ICommandHandler<CreateCharacterCommand, CreateCharacterResult>
    {
        public async Task<CreateCharacterResult> Handle(CreateCharacterCommand command, CancellationToken cancellationToken)
        {
            var character = new Entities.Character
            {
                UserId = command.UserId,
                SchemaId = command.SchemaId,
                Name = command.Name,
                Description = command.Description,
                Statistics = command.Statistics
            };

            session.Store(character);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateCharacterResult(character.Id);
        }
    }
}
