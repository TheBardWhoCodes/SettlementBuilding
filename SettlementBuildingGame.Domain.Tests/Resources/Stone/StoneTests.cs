using SettlementBuildingGame.Domain.Resources;
using StoneDomain = SettlementBuildingGame.Domain.Resources.Stone;
using SettlementBuildingGame.Domain.Resources.Stone.Interfaces;

namespace SettlementBuildingGame.Domain.Tests.Resources.Stone;

public class StoneTests
{
    [Fact]
    public void Stone_Should_Inherit_From_Resource()
    {
        // Arrange & Act
        var stone = new StoneDomain.Stone();

        // Assert
        Assert.IsType<StoneDomain.Stone>(stone);
        Assert.IsAssignableFrom<Resource>(stone);
    }

    [Fact]
    public void Stone_Should_Implement_IQuarryable()
    {
        // Arrange & Act
        var stone = new StoneDomain.Stone();

        // Assert
        Assert.IsAssignableFrom<IQuarryable>(stone);
    }

    [Fact]
    public void Stone_Quarry_Should_Execute_Without_Exception()
    {
        // Arrange
        var stone = new StoneDomain.Stone();

        // Act & Assert
        var exception = Record.Exception(() => stone.Quarry());
        Assert.Null(exception);
    }

}
