namespace Character.API.UnitTests.Features
{
    public class GetCharacterByIdQueryTests
    {
        private readonly Mock<IDocumentSession> _session = new();

        [Fact]
        public async Task Handle_ShouldThrowCharacterNotFoundException_WhenCharacterIsNotFound()
        {
            //Arrange
            var command = new GetCharacterByIdQuery(Guid.NewGuid());

            _session.Setup(x => x.LoadAsync<Entities.Character>(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);

            var handler = new GetCharacterByIdQueryHandler(_session.Object);

            //Act & Assert
            await Assert.ThrowsAsync<CharacterNotFoundException>(async () => await handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_ShouldReturnGetCharacterByIdResult_WhenCharacterIsFound()
        {
            //Arrange
            var command = new GetCharacterByIdQuery(Guid.NewGuid());
            var character = new Entities.Character
            {
                Id = Guid.NewGuid(),
                SchemaId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Glimbo the Goblin",
                Description = "He is just a wibble goblin",
                Statistics = [new Statistic { Name = "Size", Value = "Wibble" }]
            };

            _session.Setup(x => x.LoadAsync<Entities.Character>(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(character);
            _session.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var handler = new GetCharacterByIdQueryHandler(_session.Object);

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
