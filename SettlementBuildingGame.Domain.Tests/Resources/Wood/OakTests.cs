using SettlementBuildingGame.Domain.Resources;
using WoodDomain = SettlementBuildingGame.Domain.Resources.Wood;
using SettlementBuildingGame.Domain.Resources.Wood.Interfaces;

namespace SettlementBuildingGame.Domain.Tests.Resources.Wood;

public class OakTests
{
    [Fact]
    public void Oak_Should_Inherit_From_Wood()
    {
        // Arrange & Act
        var oak = new WoodDomain.Oak();

        // Assert
        Assert.IsType<WoodDomain.Oak>(oak);
        Assert.IsAssignableFrom<WoodDomain.Wood>(oak);
        Assert.IsAssignableFrom<Resource>(oak);
    }

    [Fact]
    public void Oak_Should_Implement_IChopable()
    {
        // Arrange & Act
        var oak = new WoodDomain.Oak();

        // Assert
        Assert.IsAssignableFrom<IChopable>(oak);
    }

    [Fact]
    public void Oak_Chop_Should_Execute_Without_Exception()
    {
        // Arrange
        var oak = new WoodDomain.Oak();

        // Act & Assert
        var exception = Record.Exception(() => oak.Chop());
        Assert.Null(exception);
    }
}
