namespace Character.API.UnitTests.Features
{
    public class DeleteCharacterByIdCommandHandlerTests
    {
        private readonly Mock<IDocumentSession> _session = new();

        [Fact]
        public async Task Handle_ShouldThrowCharacterNotFoundException_WhenCharacterIsNotFound()
        {
            //Arrange
            var command = new DeleteCharacterByIdCommand(Guid.NewGuid());

            _session.Setup(x => x.LoadAsync<Entities.Character>(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);

            var handler = new DeleteCharacterByIdCommandHandler(_session.Object);

            //Act & Assert
            await Assert.ThrowsAsync<CharacterNotFoundException>(async () => await handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_ShouldReturnDeleteCharacterByIdResult_WhenCharacterIsFound()
        {
            //Arrange
            var command = new DeleteCharacterByIdCommand(Guid.NewGuid());

            _session.Setup(x => x.LoadAsync<Entities.Character>(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Entities.Character
                {
                    Id = Guid.NewGuid(),
                    Name = string.Empty,
                    Statistics = []
                });
            _session.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var handler = new DeleteCharacterByIdCommandHandler(_session.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            _session.Verify(s => s.Delete(It.IsAny<Entities.Character>()), Times.Once);
            _session.Verify(s => s.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }
    }
}
