namespace Character.API.Features.DeleteCharacterById
{
    internal class DeleteCharacterByIdCommandValidator : AbstractValidator<DeleteCharacterByIdCommand>
    {
        public DeleteCharacterByIdCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
