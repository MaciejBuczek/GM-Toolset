namespace Character.API.UnitTests.Features
{
    public class UpdateCharacterCommandHandlerTests
    {
        private readonly Mock<ICharacterRepository> _repository = new();
        private readonly UpdateCharacterCommand _updateCharacterCommand = new(
                Id: Guid.NewGuid(),
                UserId: Guid.NewGuid(),
                SchemaId: Guid.NewGuid(),
                Name: "Glimbo the Goblin",
                Description: "He is just a wibble gobwin",
                Statistics: [new Statistic { Name = "Size", Value = "Wibble" }]
                );

        [Fact]
        public async Task Handle_ShouldThrowCharacterNotFoundException_WhenCharacterIsNotFound()
        {
            //Arrange
            var command = _updateCharacterCommand;

            _repository.Setup(x => x.UpdateCharacterAsync(It.IsAny<Entities.Character>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NonExistentDocumentException(typeof(Entities.Character), Guid.NewGuid()));

            var handler = new UpdateCharacterCommandHandler(_repository.Object);

            //Act & Assert
            await Assert.ThrowsAsync<CharacterNotFoundException>(async () => await handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_ShouldReturnUpdateCharaterResult_WhenCharacterIsFound()
        {
            //Arrange
            var command = _updateCharacterCommand;

            _repository.Setup(x => x.UpdateCharacterAsync(It.IsAny<Entities.Character>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var handler = new UpdateCharacterCommandHandler(_repository.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
        }
    }
}
