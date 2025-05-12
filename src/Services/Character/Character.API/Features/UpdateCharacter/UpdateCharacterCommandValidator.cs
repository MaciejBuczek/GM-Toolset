namespace Character.API.Features.UpdateCharacter
{
    internal class UpdateCharacterCommandValidator : AbstractValidator<UpdateCharacterCommand>
    {
        public UpdateCharacterCommandValidator()
        {
            Include(new CharacterBaseRequestValidator());

            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
