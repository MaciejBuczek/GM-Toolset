namespace Character.API.UnitTests.Features
{
    public class CreateCharacterCommandHandlerTests
    {
        private readonly Mock<ICharacterRepository> _repository = new();

        private static CreateCharacterCommand _createCharacterCommand => new(
                UserId: Guid.NewGuid(),
                SchemaId: Guid.NewGuid(),
                Name: "Glimbo the Goblin",
                Description: "He is just a wibble gobwin",
                Statistics: [new Statistic { Name = "Size", Value = "Wibble" }]
                );

        [Fact]
        public async Task Handle_ShouldThrowBadRequestException_WhenIdIsNotUnique()
        {
            //Arrange
            var command = _createCharacterCommand;

            _repository.Setup(x => x.CreateCharacterAsync(It.IsAny<Entities.Character>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DocumentAlreadyExistsException(null, typeof(Entities.Character), Guid.NewGuid()));

            var handler = new CreateCharacterCommandHandler(_repository.Object);

            //Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(async () => await handler.Handle(command, default));        
        }

        [Fact]
        public async Task Handle_ShouldReturnCreateCharacterResult_WhenIdIsUnique()
        {
            //Arrange
            var command = _createCharacterCommand;
            var insertedCharacter = new Entities.Character
            {
                Name = command.Name,
                Statistics = command.Statistics
            };

            _repository.Setup(x => x.CreateCharacterAsync(It.IsAny<Entities.Character>(), It.IsAny<CancellationToken>()))
                .Callback<Entities.Character, CancellationToken>((character, token) =>
                {
                    character.Id = Guid.NewGuid();
                    insertedCharacter = character;
                });

            var handler = new CreateCharacterCommandHandler(_repository.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(insertedCharacter.Id, result.CharacterId);
        }
    }
}
