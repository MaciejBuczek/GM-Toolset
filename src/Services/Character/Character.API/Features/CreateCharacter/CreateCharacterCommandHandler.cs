namespace Character.API.Features.CreateCharacter
{
    public record CreateCharacterResponse(Guid CharacterId);
    public record CreateCharacterCommand(Guid UserId, Guid SchemaId, string Name, string Description, IEnumerable<Statistic> Statistics) : ICommand<CreateCharacterResponse>;

    public class CreateCharacterCommandHandler(IDocumentSession session) : ICommandHandler<CreateCharacterCommand, CreateCharacterResponse>
    {
        public async Task<CreateCharacterResponse> Handle(CreateCharacterCommand command, CancellationToken cancellationToken)
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

            return new CreateCharacterResponse(character.Id);
        }
    }
}
