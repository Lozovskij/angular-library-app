using AutoFixture;
using FluentAssertions;
using LibraryApp.Core.Entities;
using LibraryApp.Core.Exceptions;
using Moq;

namespace UnitTests.Core.Entities;

public class BookInstanceTests : TestsBase
{
    [Theory]
    [InlineData(BookInstanceStatus.Available, BookInstanceStatus.OnHold)]
    [InlineData(BookInstanceStatus.OnHold, BookInstanceStatus.Available)]
    [InlineData(BookInstanceStatus.OnHold, BookInstanceStatus.CheckedOut)]
    [InlineData(BookInstanceStatus.CheckedOut, BookInstanceStatus.Available)]
    [InlineData(BookInstanceStatus.CheckedOut, BookInstanceStatus.Overdue)]
    [InlineData(BookInstanceStatus.Overdue, BookInstanceStatus.Available)]
    public void SetStatus_Success(BookInstanceStatus currStatus, BookInstanceStatus nextStatus)
    {
        //Arrange
        int? stubPatronId = nextStatus == BookInstanceStatus.Available ? null : _fixture.Create<int>();
        var sut = new BookInstance() { PatronId = stubPatronId };
        var propertyInfo = typeof(BookInstance).GetProperty(nameof(BookInstance.Status));
        propertyInfo.SetValue(sut, currStatus, null);

        //Act
        sut.SetStatus(nextStatus);

        //Assert
        sut.Status.Should().Be(nextStatus);
    }

    [Theory]
    [InlineData(BookInstanceStatus.Available, BookInstanceStatus.Overdue)]
    [InlineData(BookInstanceStatus.Available, BookInstanceStatus.CheckedOut)]
    [InlineData(BookInstanceStatus.OnHold, BookInstanceStatus.Overdue)]
    [InlineData(BookInstanceStatus.CheckedOut, BookInstanceStatus.OnHold)]
    [InlineData(BookInstanceStatus.Overdue, BookInstanceStatus.OnHold)]
    [InlineData(BookInstanceStatus.Overdue, BookInstanceStatus.CheckedOut)]
    public void SetStatus_Exception(BookInstanceStatus currStatus, BookInstanceStatus nextStatus)
    {
        //Arrange
        var sut = new BookInstance() { PatronId = _fixture.Create<int>() };
        var propertyInfo = typeof(BookInstance).GetProperty(nameof(BookInstance.Status));
        propertyInfo.SetValue(sut, currStatus, null);

        //Act
        Action act = () => sut.SetStatus(nextStatus);

        //Assert
        act.Should().Throw<InvalidStatusChangeException>();
    }

    [Fact]
    public void SetStatus_SetAvailableWhenPatronIdIsNotNull_Exception()
    {
        //Arrange
        var mockPatronId = _fixture.Create<int>();
        var sut = new BookInstance() { PatronId = mockPatronId };
        var propertyInfo = typeof(BookInstance).GetProperty(nameof(BookInstance.Status));
        propertyInfo.SetValue(sut, BookInstanceStatus.OnHold, null);

        //Act
        Action act = () => sut.SetStatus(BookInstanceStatus.Available);

        //Assert
        act.Should().Throw<Exception>();
    }


    [Fact]
    public void SetStatus_SetOnHoldWhenPatronIdIsNull_Exception()
    {
        //Arrange
        int? mockPatronId = null;
        var sut = new BookInstance() { PatronId = mockPatronId };
        var propertyInfo = typeof(BookInstance).GetProperty(nameof(BookInstance.Status));
        propertyInfo.SetValue(sut, BookInstanceStatus.Available, null);

        //Act
        Action act = () => sut.SetStatus(BookInstanceStatus.OnHold);

        //Assert
        act.Should().Throw<Exception>();
    }
}
