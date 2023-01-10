using LibraryApp.Core.Entities;
using LibraryApp.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace UnitTests.Infrastructure.Services;
public class TokenServiceTests
{
    private readonly TokenService _sut;
    public TokenServiceTests()
    {
        var confStub = new Mock<IConfiguration>();
        confStub.Setup(c => c.GetSection(It.IsAny<string>()).Value).Returns("test test test test");
        _sut = new TokenService(confStub.Object);
    }

    [Fact]
    public void Create_UsualScenario_Success()
    {
        //Arrange
        var passwordHashStr = "7YScPE3aW7qKhj/p2EhBhMmksT1xTsh2BChP0IJmPrx10fimbn3bitrTFIhM9boHebagisf7dUWSBx0RJYuSOQ==";
        var passwordSaltStr = "nnw/qsRS3uUR8rZLOv9/iRaeB2TfB3+kXbJ+esKU520tvT5S3bpb6abnylC08ddgn6RWs+kg9mHN0vw/L3ZkNAPiumff8j2NfCXSRiE43lH1niMc8TvrzX0vsJoiBqqtwyHuRcSRm/L+NFv3SNG5SBLbyOe9ckPJaJILHRgeYeE=";
        var patron = new Patron("Ivan", "Testov", "AA11", 1, passwordHashStr, passwordSaltStr);

        //Act
        var res = _sut.Create(patron);

        //Assert
        Assert.True(res.Length > 0);
    }
}
