using SettlementBuildingGame.Domain.Resources.Ore.Interfaces;
using SettlementBuildingGame.Domain.Resources.Wood.Interfaces;
using SettlementBuildingGame.Domain.Resources.Stone.Interfaces;
using SettlementBuildingGame.Domain.Resources.Ore;
using WoodDomain = SettlementBuildingGame.Domain.Resources.Wood;
using StoneDomain = SettlementBuildingGame.Domain.Resources.Stone;

namespace SettlementBuildingGame.Domain.Tests.Resources.Interfaces;

public class ResourceInterfaceTests
{
    #region IMineable Tests

    [Fact]
    public void IMineable_Should_Be_Implemented_By_All_Ore_Types()
    {
        // Arrange & Act
        var copperOre = new CopperOre();
        var ironOre = new IronOre();
        var goldOre = new GoldOre();

        // Assert
        Assert.IsAssignableFrom<IMineable>(copperOre);
        Assert.IsAssignableFrom<IMineable>(ironOre);
        Assert.IsAssignableFrom<IMineable>(goldOre);
    }

    [Fact]
    public void IMineable_Mine_Should_Execute_On_CopperOre()
    {
        // Arrange
        var copperOre = new CopperOre();

        // Act & Assert
        var exception = Record.Exception(() => copperOre.Mine());
        Assert.Null(exception);
    }

    [Fact]
    public void IMineable_Mine_Should_Execute_On_IronOre()
    {
        // Arrange
        var ironOre = new IronOre();

        // Act & Assert
        var exception = Record.Exception(() => ironOre.Mine());
        Assert.Null(exception);
    }

    [Fact]
    public void IMineable_Mine_Should_Execute_On_GoldOre()
    {
        // Arrange
        var goldOre = new GoldOre();

        // Act & Assert
        var exception = Record.Exception(() => goldOre.Mine());
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(typeof(CopperOre))]
    [InlineData(typeof(IronOre))]
    [InlineData(typeof(GoldOre))]
    public void IMineable_Should_Be_Implemented_By_Ore_Type(Type oreType)
    {
        // Arrange & Act
        var ore = Activator.CreateInstance(oreType);

        // Assert
        Assert.IsAssignableFrom<IMineable>(ore);
    }

    #endregion

    #region IChopable Tests

    [Fact]
    public void IChopable_Should_Be_Implemented_By_Wood_Types()
    {
        // Arrange & Act
        var wood = new WoodDomain.Wood();
        var oak = new WoodDomain.Oak();

        // Assert
        Assert.IsAssignableFrom<IChopable>(wood);
        Assert.IsAssignableFrom<IChopable>(oak);
    }

    [Fact]
    public void IChopable_Chop_Should_Execute_On_Wood()
    {
        // Arrange
        var wood = new WoodDomain.Wood();

        // Act & Assert
        var exception = Record.Exception(() => wood.Chop());
        Assert.Null(exception);
    }

    [Fact]
    public void IChopable_Chop_Should_Execute_On_Oak()
    {
        // Arrange
        var oak = new WoodDomain.Oak();

        // Act & Assert
        var exception = Record.Exception(() => oak.Chop());
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(typeof(WoodDomain.Wood))]
    [InlineData(typeof(WoodDomain.Oak))]
    public void IChopable_Should_Be_Implemented_By_Wood_Type(Type woodType)
    {
        // Arrange & Act
        var wood = Activator.CreateInstance(woodType);

        // Assert
        Assert.IsAssignableFrom<IChopable>(wood);
    }

    #endregion

    #region IQuarryable Tests

    [Fact]
    public void IQuarryable_Should_Be_Implemented_By_Stone()
    {
        // Arrange & Act
        var stone = new StoneDomain.Stone();

        // Assert
        Assert.IsAssignableFrom<IQuarryable>(stone);
    }

    [Fact]
    public void IQuarryable_Quarry_Should_Execute_On_Stone()
    {
        // Arrange
        var stone = new StoneDomain.Stone();

        // Act & Assert
        var exception = Record.Exception(() => stone.Quarry());
        Assert.Null(exception);
    }

    #endregion

    #region Interface Method Behavior Tests

    [Fact]
    public void IMineable_Mine_Should_Be_Callable_Through_Interface()
    {
        // Arrange
        IMineable mineable = new CopperOre();

        // Act & Assert
        var exception = Record.Exception(() => mineable.Mine());
        Assert.Null(exception);
    }

    [Fact]
    public void IChopable_Chop_Should_Be_Callable_Through_Interface()
    {
        // Arrange
        IChopable chopable = new WoodDomain.Oak();

        // Act & Assert
        var exception = Record.Exception(() => chopable.Chop());
        Assert.Null(exception);
    }

    [Fact]
    public void IQuarryable_Quarry_Should_Be_Callable_Through_Interface()
    {
        // Arrange
        IQuarryable quarryable = new StoneDomain.Stone();

        // Act & Assert
        var exception = Record.Exception(() => quarryable.Quarry());
        Assert.Null(exception);
    }

    #endregion

    #region Interface Compatibility Tests

    [Fact]
    public void Ore_Should_Implement_IMineable_Interface()
    {
        // Arrange & Act
        var ore = new CopperOre();

        // Assert
        Assert.True(ore is IMineable);
        Assert.True(typeof(IMineable).IsAssignableFrom(ore.GetType()));
    }

    [Fact]
    public void Wood_Should_Implement_IChopable_Interface()
    {
        // Arrange & Act
        var wood = new WoodDomain.Oak();

        // Assert
        Assert.True(wood is IChopable);
        Assert.True(typeof(IChopable).IsAssignableFrom(wood.GetType()));
    }

    [Fact]
    public void Stone_Should_Implement_IQuarryable_Interface()
    {
        // Arrange & Act
        var stone = new StoneDomain.Stone();

        // Assert
        Assert.True(stone is IQuarryable);
        Assert.True(typeof(IQuarryable).IsAssignableFrom(stone.GetType()));
    }

    #endregion

    #region Polymorphism Tests

    [Fact]
    public void Multiple_Mineable_Resources_Should_Support_Polymorphic_Behavior()
    {
        // Arrange
        var mineableResources = new List<IMineable>
        {
            new CopperOre(),
            new IronOre(),
            new GoldOre()
        };

        // Act & Assert
        foreach (var resource in mineableResources)
        {
            var exception = Record.Exception(() => resource.Mine());
            Assert.Null(exception);
        }
    }

    [Fact]
    public void Multiple_Chopable_Resources_Should_Support_Polymorphic_Behavior()
    {
        // Arrange
        var chopableResources = new List<IChopable>
        {
            new WoodDomain.Wood(),
            new WoodDomain.Oak()
        };

        // Act & Assert
        foreach (var resource in chopableResources)
        {
            var exception = Record.Exception(() => resource.Chop());
            Assert.Null(exception);
        }
    }

    #endregion
}