namespace Character.API.Features.CreateCharacter
{
    internal class CreateCharacterCommandValidator : AbstractValidator<CreateCharacterCommand>
    {
        public CreateCharacterCommandValidator()
        {
            Include(new CharacterBaseRequestValidator());
        }
    }
}
