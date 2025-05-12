using Character.API.Exceptions;

namespace Character.API.Features.DeleteCharacterById
{
    internal record DeleteCharacterByIdResult(bool Success);
    internal record DeleteCharacterByIdCommand(Guid Id) : ICommand<DeleteCharacterByIdResult>;
    
    internal class DeleteCharacterByIdCommandHandler(IDocumentSession session) : ICommandHandler<DeleteCharacterByIdCommand, DeleteCharacterByIdResult>
    {
        public async Task<DeleteCharacterByIdResult> Handle(DeleteCharacterByIdCommand command, CancellationToken cancellationToken = default)
        {
            var character = await session.LoadAsync<Entities.Character>(command.Id, cancellationToken) ??
                throw new CharacterNotFoundException(command.Id);

            session.Delete(character);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteCharacterByIdResult(true);
        }
    }
}
