namespace Character.API.UnitTests.Features
{
    public class CreateCharacterCommandHandlerTests
    {
        private readonly Mock<IDocumentSession> _session = new();

        private static CreateCharacterCommand CreateCharacterCommand => new(
                UserId: Guid.NewGuid(),
                SchemaId: Guid.NewGuid(),
                Name: "Glimbo the Goblin",
                Description: "He is just a wibble gobwin",
                Statistics: [new Statistic { Name = "Size", Value = "Wibble" }]
                );

        [Fact]
        public async Task Handle_ShouldThrowException_WhenIdIsNotUnique()
        {
            //Arrange
            var command = CreateCharacterCommand;

            _session.Setup(x => x.Insert(It.IsAny<Entities.Character>()))
                .Throws(new DocumentAlreadyExistsException(null, typeof(Entities.Character), new Guid()));

            var handler = new CreateCharacterCommandHandler(_session.Object);

            //Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(async () => await handler.Handle(command, default));        
        }

        [Fact]
        public async Task Handle_ShouldReturnCreateCharacterResult_WhenIdIsUnique()
        {
            //Arrange
            var command = CreateCharacterCommand;
            var insertedCharacter = new Entities.Character
            {
                Name = command.Name,
                Statistics = command.Statistics
            };

            _session.Setup(x => x.Insert(It.IsAny<Entities.Character>()))
                .Callback<Entities.Character[]>(c =>
                {
                    var character = c[0];
                    character.Id = Guid.NewGuid();
                    insertedCharacter = character;
                });
            _session.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var handler = new CreateCharacterCommandHandler(_session.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            _session.Verify(s => s.Insert(It.IsAny<Entities.Character>()), Times.Once);
            _session.Verify(s => s.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.CharacterId);
            Assert.Equal(insertedCharacter.Id, result.CharacterId);
        }
    }
}
