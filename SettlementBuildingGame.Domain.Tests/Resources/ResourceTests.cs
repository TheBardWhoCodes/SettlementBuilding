using SettlementBuildingGame.Domain.Resources;
using SettlementBuildingGame.Domain.Resources.Ore;
using SettlementBuildingGame.Domain.Resources.Wood;
using SettlementBuildingGame.Domain.Resources.Ore.Interfaces;
using StoneDomain = SettlementBuildingGame.Domain.Resources.Stone;

namespace SettlementBuildingGame.Domain.Tests.Resources;

public class ResourceTests
{
    [Fact]
    public void CopperOre_Should_Inherit_From_Ore()
    {
        // Arrange & Act
        var copperOre = new CopperOre();

        // Assert
        Assert.IsType<CopperOre>(copperOre);
        Assert.IsAssignableFrom<Ore>(copperOre);
        Assert.IsAssignableFrom<Resource>(copperOre);
    }

    [Fact]
    public void CopperOre_Should_Implement_IMineable()
    {
        // Arrange & Act
        var copperOre = new CopperOre();

        // Assert
        Assert.IsAssignableFrom<IMineable>(copperOre);
    }

    [Fact]
    public void CopperOre_Mine_Should_Execute_Without_Exception()
    {
        // Arrange
        var copperOre = new CopperOre();

        // Act & Assert
        var exception = Record.Exception(() => copperOre.Mine());
        Assert.Null(exception);
    }

    [Fact]
    public void IronOre_Should_Inherit_From_Ore()
    {
        // Arrange & Act
        var ironOre = new IronOre();

        // Assert
        Assert.IsType<IronOre>(ironOre);
        Assert.IsAssignableFrom<Ore>(ironOre);
        Assert.IsAssignableFrom<Resource>(ironOre);
    }

    [Fact]
    public void IronOre_Should_Implement_IMineable()
    {
        // Arrange & Act
        var ironOre = new IronOre();

        // Assert
        Assert.IsAssignableFrom<IMineable>(ironOre);
    }

    [Fact]
    public void IronOre_Mine_Should_Execute_Without_Exception()
    {
        // Arrange
        var ironOre = new IronOre();

        // Act & Assert
        var exception = Record.Exception(() => ironOre.Mine());
        Assert.Null(exception);
    }

    [Fact]
    public void GoldOre_Should_Inherit_From_Ore()
    {
        // Arrange & Act
        var goldOre = new GoldOre();

        // Assert
        Assert.IsType<GoldOre>(goldOre);
        Assert.IsAssignableFrom<Ore>(goldOre);
        Assert.IsAssignableFrom<Resource>(goldOre);
    }

    [Fact]
    public void GoldOre_Should_Implement_IMineable()
    {
        // Arrange & Act
        var goldOre = new GoldOre();

        // Assert
        Assert.IsAssignableFrom<IMineable>(goldOre);
    }

    [Fact]
    public void GoldOre_Mine_Should_Execute_Without_Exception()
    {
        // Arrange
        var goldOre = new GoldOre();

        // Act & Assert
        var exception = Record.Exception(() => goldOre.Mine());
        Assert.Null(exception);
    }


    [Theory]
    [InlineData(typeof(Oak))]
    [InlineData(typeof(CopperOre))]
    [InlineData(typeof(IronOre))]
    [InlineData(typeof(GoldOre))]
    [InlineData(typeof(StoneDomain.Stone))]
    public void All_Resources_Should_Inherit_From_Resource_Base_Class(Type resourceType)
    {
        // Arrange & Act
        var resource = Activator.CreateInstance(resourceType);

        // Assert
        Assert.IsAssignableFrom<Resource>(resource);
    }
}