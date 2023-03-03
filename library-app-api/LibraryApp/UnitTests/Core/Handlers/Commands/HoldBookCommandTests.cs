using AutoFixture;
using FluentAssertions;
using LibraryApp.Core.Entities;
using LibraryApp.Core.Exceptions;
using LibraryApp.Core.Handlers.Commands;
using LibraryApp.Core.Interfaces;
using LibraryApp.Core.Interfaces.Repositories;
using Moq;

namespace UnitTests.Core.Handlers.Commands;

public class HoldBookCommandTests : TestsBase
{
    private IUserService _userServiceStub;

    public HoldBookCommandTests()
    {
        _userServiceStub = new Mock<IUserService>().Object;
    }

    [Fact]
    public async Task Handle_OnSuccess_BookInstanceStatusChanged()
    {
        //Arrange
        BookInstance[] books = _fixture.CreateMany<BookInstance>(1).ToArray();
        books[0].PatronId = null;
        books[0].SetStatus(BookInstanceStatus.Available);
        
        var mock = new Mock<IBookInstancesRepository>();
        mock.Setup(r => r.IsAvailableAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        mock.Setup(r => r.GetAvailableBooksAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(books);
        var sut = new HoldBookCommandHandler(mock.Object, _userServiceStub);

        //Act
        await sut.Handle(_fixture.Create<HoldBookCommand>(), CancellationToken.None);

        //Assert
        books[0].Status.Should().Be(BookInstanceStatus.OnHold);
    }

    //Exception when User has 5 books on hold already
    [Fact]
    public async Task Handle_UserHasFiveBooksOnHold_Exception()
    {
        //Arrange
        BookInstance[] books = _fixture.CreateMany<BookInstance>(5).ToArray();
        for (int i = 0; i < books.Length; i++)
        {
            books[i].SetStatus(BookInstanceStatus.OnHold);
        }
        var mock = new Mock<IBookInstancesRepository>();
        mock.Setup(r => r.GetPatronBooksAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(books);
        var sut = new HoldBookCommandHandler(mock.Object, _userServiceStub);

        //Act
        Func<Task> act = () => sut.Handle(_fixture.Create<HoldBookCommand>(), CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<Exception>().WithMessage(ExceptionMessages.TooMuchBooksOnHold);
    }

    //Exception when User has at least 2 books overdue
    [Fact]
    public async Task Handle_UserHasTwoBooksOverdue_Exception()
    {
        //Arrange
        BookInstance[] books = _fixture.CreateMany<BookInstance>(2).ToArray();

        books[0].SetStatus(BookInstanceStatus.OnHold);
        books[0].SetStatus(BookInstanceStatus.CheckedOut);
        books[0].SetStatus(BookInstanceStatus.Overdue);

        books[1].SetStatus(BookInstanceStatus.OnHold);
        books[1].SetStatus(BookInstanceStatus.CheckedOut);
        books[1].SetStatus(BookInstanceStatus.Overdue);
        
        var mock = new Mock<IBookInstancesRepository>();
        mock.Setup(r => r.GetPatronBooksAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(books);
        var userServiceStub = new Mock<IUserService>().Object;
        var sut = new HoldBookCommandHandler(mock.Object, _userServiceStub);

        //Act
        Func<Task> act = () => sut.Handle(_fixture.Create<HoldBookCommand>(), CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<Exception>().WithMessage(ExceptionMessages.TooMuchBooksOverdue);
    }

    //Exception when no available books availabe
    [Fact]
    public async Task Handle_BookIsNotAvailable_Exception()
    {
        //Arrange
        var mock = new Mock<IBookInstancesRepository>();
        mock.Setup(r => r.IsAvailableAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        var userServiceStub = new Mock<IUserService>().Object;
        var sut = new HoldBookCommandHandler(mock.Object, _userServiceStub);

        //Act
        Func<Task> act = () => sut.Handle(_fixture.Create<HoldBookCommand>(), CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<Exception>().WithMessage(ExceptionMessages.BookIsNotAvailable);
    }
}