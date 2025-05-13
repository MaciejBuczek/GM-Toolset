namespace Character.API.Features.DeleteCharacterById
{
    internal record DeleteCharacterByIdResult(bool Success);
    internal record DeleteCharacterByIdCommand(Guid Id) : ICommand<DeleteCharacterByIdResult>;
    internal class DeleteCharacterByIdCommandHandler(ICharacterRepository repository) : ICommandHandler<DeleteCharacterByIdCommand, DeleteCharacterByIdResult>
    {
        public async Task<DeleteCharacterByIdResult> Handle(DeleteCharacterByIdCommand command, CancellationToken cancellationToken = default)
        {
            var character = await repository.GetCharacterByIdAsync(command.Id, cancellationToken) ??
                throw new CharacterNotFoundException(command.Id);

            await repository.DeleteCharacterAsync(character, cancellationToken);
            return new DeleteCharacterByIdResult(true);
        }
    }
}
