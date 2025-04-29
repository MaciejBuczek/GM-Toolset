namespace Character.API.Features.DeleteCharacterById
{
    public record DeleteCharacterByIdResult(bool Success);
    public record DeleteCharacterByIdCommand(Guid Id) : ICommand<DeleteCharacterByIdResult>;
    public class DeleteCharacterByIdHandler(IDocumentSession session) : ICommandHandler<DeleteCharacterByIdCommand, DeleteCharacterByIdResult>
    {
        public async Task<DeleteCharacterByIdResult> Handle(DeleteCharacterByIdCommand command, CancellationToken cancellationToken = default)
        {
            var character = await session.LoadAsync<Entities.Character>(command.Id, cancellationToken) ??
                throw new ApplicationException("Character not found");
            
            session.Delete(character);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteCharacterByIdResult(true);
        }
    }
}
