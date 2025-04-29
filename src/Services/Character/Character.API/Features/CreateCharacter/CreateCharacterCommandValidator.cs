namespace Character.API.Features.CreateCharacter
{
    public class CreateCharacterCommandValidator : AbstractValidator<CreateCharacterCommand>
    {
        public CreateCharacterCommandValidator()
        {
            Include(new CharacterBaseRequestValidator());
        }
    }
}
