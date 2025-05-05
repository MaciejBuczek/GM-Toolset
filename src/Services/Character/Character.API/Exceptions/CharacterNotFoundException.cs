using Common.Exceptions;

namespace Character.API.Exceptions
{
    public class CharacterNotFoundException(Guid Id) : NotFoundException(nameof(Entities.Character), Id)
    {
    }
}
