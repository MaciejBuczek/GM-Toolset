namespace Character.API.Features.CreateCharacter
{
    public record CreateCharacterResult(Guid CharacterId);
    public record CreateCharacterCommand(Guid UserId, Guid SchemaId, string Name, string Description, ICollection<Statistic> Statistics)
        : CharacterBaseRequest(UserId, SchemaId, Name, Description, Statistics), ICommand<CreateCharacterResult>;

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

            try
            {
                session.Insert(character);
                await session.SaveChangesAsync(cancellationToken);
            }
            catch (DocumentAlreadyExistsException)
            {
                throw new ApplicationException("Characther with this id already exists");
            }


            return new CreateCharacterResult(character.Id);
        }
    }
}
