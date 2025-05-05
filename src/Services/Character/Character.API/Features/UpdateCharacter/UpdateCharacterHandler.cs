using Character.API.Exceptions;

namespace Character.API.Features.UpdateCharacter
{
    public record UpdateCharaterResult(bool Succeded);
    public record UpdateCharacterCommand(Guid Id, Guid UserId, Guid SchemaId, string Name, string Description, ICollection<Statistic> Statistics)
        : CharacterBaseRequest(UserId, SchemaId, Name, Description, Statistics), ICommand<UpdateCharaterResult>;

    public class UpdateCharacterHandler(IDocumentSession session) : ICommandHandler<UpdateCharacterCommand, UpdateCharaterResult>
    {
        public async Task<UpdateCharaterResult> Handle(UpdateCharacterCommand command, CancellationToken cancellationToken = default)
        {
            var character = new Entities.Character
            {
                Id = command.Id,
                SchemaId = command.SchemaId,
                UserId = command.UserId,
                Name = command.Name,
                Description = command.Description,
                Statistics = command.Statistics
            };
            try
            {
                session.Update(character);
                await session.SaveChangesAsync(cancellationToken);
            }
            catch (NonExistentDocumentException)
            {
                throw new CharacterNotFoundException(command.Id);
            }

            return new UpdateCharaterResult(true);
        }
    }
}
