using AutoFixture;
using FluentAssertions;
using LibraryApp.Core.Entities;
using LibraryApp.Core.Exceptions;
using LibraryApp.Core.Handlers.Commands;
using LibraryApp.Core.Interfaces;
using LibraryApp.Core.Interfaces.Repositories;
using Moq;

namespace UnitTests.Core.Handlers.Commands;

public class CancelHoldCommandTests : TestsBase
{
    [Fact]
    public async Task Handle_OnSuccess_BookIsAvailable()
    {
        //Arrange
        var book = _fixture.Create<BookInstance>();
        book.SetStatus(BookInstanceStatus.OnHold);
        var mock = new Mock<IBookInstancesRepository>();
        mock.Setup(r => r.GetPatronBookAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(book);

        var sut = new CancelHoldCommandHandler(mock.Object, new Mock<IUserService>().Object);

        //Act
        await sut.Handle(_fixture.Create<CancelHoldCommand>(), CancellationToken.None);

        //Assert
        mock.Verify(m => m.UpdateAsync(It.IsAny<BookInstance>(), CancellationToken.None));
        book.Status.Should().Be(BookInstanceStatus.Available);
    }
}
