namespace Character.API.UnitTests.Features
{
    public class DeleteCharacterByIdCommandHandlerTests
    {
        private readonly Mock<ICharacterRepository> _repository = new();

        [Fact]
        public async Task Handle_ShouldThrowCharacterNotFoundException_WhenCharacterIsNotFound()
        {
            //Arrange
            var command = new DeleteCharacterByIdCommand(Guid.NewGuid());

            _repository.Setup(x => x.GetCharacterByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);

            var handler = new DeleteCharacterByIdCommandHandler(_repository.Object);

            //Act & Assert
            await Assert.ThrowsAsync<CharacterNotFoundException>(async () => await handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_ShouldReturnDeleteCharacterByIdResult_WhenCharacterIsFound()
        {
            //Arrange
            var command = new DeleteCharacterByIdCommand(Guid.NewGuid());
            var character = new Entities.Character
            {
                Id = Guid.NewGuid(),
                SchemaId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Glimbo the Goblin",
                Description = "He is just a wibble goblin",
                Statistics = [new Statistic { Name = "Size", Value = "Wibble" }]
            };

            _repository.Setup(x => x.GetCharacterByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => character);
            _repository.Setup(x => x.DeleteCharacterAsync(It.IsAny<Entities.Character>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var handler = new DeleteCharacterByIdCommandHandler(_repository.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            _repository.Verify(r => r.DeleteCharacterAsync(It.IsAny<Entities.Character>(), default), Times.Once);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }
    }
}
