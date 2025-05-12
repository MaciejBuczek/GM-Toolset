namespace Character.API.Features.UpdateCharacter
{
    internal record UpdateCharaterResult(bool Succeded);
    internal record UpdateCharacterCommand(Guid Id, Guid UserId, Guid SchemaId, string Name, string Description, ICollection<Statistic> Statistics)
        : CharacterBaseRequest(UserId, SchemaId, Name, Description, Statistics), ICommand<UpdateCharaterResult>;

    internal class UpdateCharacterHandler(ICharacterRepository repository) : ICommandHandler<UpdateCharacterCommand, UpdateCharaterResult>
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
                await repository.UpdateCharacterAsync(character, cancellationToken);
            }
            catch (NonExistentDocumentException)
            {
                throw new CharacterNotFoundException(command.Id);
            }

            return new UpdateCharaterResult(true);
        }
    }
}
