namespace Character.API.Features.DeleteCharacterById
{
    public class DeleteCharacterByIdCommandValidator : AbstractValidator<DeleteCharacterByIdCommand>
    {
        public DeleteCharacterByIdCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
