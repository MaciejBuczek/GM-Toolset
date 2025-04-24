using Character.API.Entities;
using Common.CQRS;

namespace Character.API.Features.CreateCharacter
{
    public record CreateCharacterResponse(Guid CharacterId);
    public record CreateCharacterCommand(Guid UserId, Guid SchemaId, string Name, string Description, IEnumerable<Statistic> Statistics) : ICommand<CreateCharacterResponse>;

    public class CreateCharacterCommandHandler : ICommandHandler<CreateCharacterCommand, CreateCharacterResponse>
    {
        public Task<CreateCharacterResponse> Handle(CreateCharacterCommand command, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
