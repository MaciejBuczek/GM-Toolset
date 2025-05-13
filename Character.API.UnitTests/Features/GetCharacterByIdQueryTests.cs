namespace Character.API.UnitTests.Features
{
    public class GetCharacterByIdQueryTests
    {
        private readonly Mock<ICharacterRepository> _repository = new();

        [Fact]
        public async Task Handle_ShouldThrowCharacterNotFoundException_WhenCharacterIsNotFound()
        {
            //Arrange
            var command = new GetCharacterByIdQuery(Guid.NewGuid());

            _repository.Setup(x => x.GetCharacterByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);

            var handler = new GetCharacterByIdQueryHandler(_repository.Object);

            //Act & Assert
            await Assert.ThrowsAsync<CharacterNotFoundException>(async () => await handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_ShouldReturnGetCharacterByIdResult_WhenCharacterIsFound()
        {
            //Arrange
            var characterGuid = Guid.NewGuid();
            var command = new GetCharacterByIdQuery(characterGuid);
            var character = new Entities.Character
            {
                Id = characterGuid,
                SchemaId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Glimbo the Goblin",
                Description = "He is just a wibble goblin",
                Statistics = [new Statistic { Name = "Size", Value = "Wibble" }]
            };

            _repository.Setup(x => x.GetCharacterByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(character);

            var handler = new GetCharacterByIdQueryHandler(_repository.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.NotNull(result?.Character);
            Assert.Equal(character.Id, result.Character.Id);
            Assert.Equal(character.SchemaId, result.Character.SchemaId);
            Assert.Equal(character.UserId, result.Character.UserId);
            Assert.Equal(character.Name, result.Character.Name);
            Assert.Equal(character.Description, result.Character.Description);
            Assert.Equal(character.Statistics, result.Character.Statistics);
        }
    }
}
