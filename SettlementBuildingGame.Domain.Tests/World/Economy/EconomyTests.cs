using SettlementBuildingGame.Domain.Settlement;
using SettlementBuildingGame.Domain.Resources.Ore;
using SettlementBuildingGame.Domain.Resources.Wood;
using SettlementBuildingGame.Domain.Resources.Stone;

namespace SettlementBuildingGame.Domain.Tests.World.Economy;

public class EconomyTests
{
    [Fact]
    public void Economy_Should_Require_World_In_Constructor()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();

        // Act
        var economy = new SettlementBuildingGame.Domain.World.Economy.Economy(world);

        // Assert
        Assert.NotNull(economy);
    }

    [Fact]
    public void GetRealValueOf_Should_Return_Equilibrium_Price_When_No_Resources_Exist()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();
        var economy = new SettlementBuildingGame.Domain.World.Economy.Economy(world);
        var oak = new Oak();

        // Act
        var value = economy.GetRealValueOf(oak);

        // Assert
        // When no resources exist (currentCount = 0, but Math.Max makes it 1)
        // baseValue = equilibriumPrice * equilibriumCount / 1 = 10.0 * 10 / 1 = 100
        Assert.Equal(100.0m, value);
    }

    [Fact]
    public void GetRealValueOf_Should_Return_Base_Price_When_Resource_Count_Equals_Equilibrium()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();
        var settlement = new SettlementBuildingGame.Domain.Settlement.Settlement();
        var oak = new Oak();
        
        // Add exactly the equilibrium amount (10 Oak)
        settlement.Inventory.AddResource(oak, 10);
        world.Settlements = [settlement];
        
        var economy = new SettlementBuildingGame.Domain.World.Economy.Economy(world);

        // Act
        var value = economy.GetRealValueOf(oak);

        // Assert
        // baseValue = equilibriumPrice * equilibriumCount / currentCount = 10.0 * 10 / 10 = 10.0
        Assert.Equal(10.0m, value);
    }

    [Fact]
    public void GetRealValueOf_Should_Return_Higher_Price_When_Resource_Is_Scarce()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();
        var settlement = new SettlementBuildingGame.Domain.Settlement.Settlement();
        var copperOre = new CopperOre();
        
        // Add less than equilibrium amount (5 instead of 15)
        settlement.Inventory.AddResource(copperOre, 5);
        world.Settlements = [settlement];
        
        var economy = new SettlementBuildingGame.Domain.World.Economy.Economy(world);

        // Act
        var value = economy.GetRealValueOf(copperOre);

        // Assert
        // baseValue = equilibriumPrice * equilibriumCount / currentCount = 15.0 * 15 / 5 = 45.0
        Assert.Equal(45.0m, value);
    }

    [Fact]
    public void GetRealValueOf_Should_Return_Lower_Price_When_Resource_Is_Abundant()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();
        var settlement = new SettlementBuildingGame.Domain.Settlement.Settlement();
        var ironOre = new IronOre();
        
        // Add more than equilibrium amount (40 instead of 20)
        settlement.Inventory.AddResource(ironOre, 40);
        world.Settlements = [settlement];
        
        var economy = new SettlementBuildingGame.Domain.World.Economy.Economy(world);

        // Act
        var value = economy.GetRealValueOf(ironOre);

        // Assert
        // baseValue = equilibriumPrice * equilibriumCount / currentCount = 20.0 * 20 / 40 = 10.0
        Assert.Equal(10.0m, value);
    }

    [Fact]
    public void GetRealValueOf_Should_Sum_Resources_Across_Multiple_Settlements()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();
        var settlement1 = new SettlementBuildingGame.Domain.Settlement.Settlement();
        var settlement2 = new SettlementBuildingGame.Domain.Settlement.Settlement();
        var oak = new Oak();
        
        settlement1.Inventory.AddResource(oak, 3);
        settlement2.Inventory.AddResource(oak, 7);
        world.Settlements = [settlement1, settlement2];
        
        var economy = new SettlementBuildingGame.Domain.World.Economy.Economy(world);

        // Act
        var value = economy.GetRealValueOf(oak);

        // Assert
        // Total resources = 3 + 7 = 10
        // baseValue = equilibriumPrice * equilibriumCount / currentCount = 10.0 * 10 / 10 = 10.0
        Assert.Equal(10.0m, value);
    }

    [Fact]
    public void GetRealValueOf_Should_Throw_ArgumentException_For_Unknown_Resource()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();
        var economy = new SettlementBuildingGame.Domain.World.Economy.Economy(world);
        var stone = new Stone(); // Stone is not in the ResourceEquilibrium dictionary

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => economy.GetRealValueOf(stone));
        Assert.Contains("Resource does not have a defined equilibrium", exception.Message);
        Assert.Equal("resource", exception.ParamName);
    }

    [Theory]
    [InlineData(1, 100.0)] // Very scarce
    [InlineData(5, 20.0)]  // Somewhat scarce
    [InlineData(10, 10.0)] // Equilibrium
    [InlineData(20, 5.0)]  // Abundant
    [InlineData(50, 2.0)]  // Very abundant
    public void GetRealValueOf_Should_Calculate_Correct_Values_For_Oak_At_Different_Quantities(int quantity, decimal expectedValue)
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();
        var settlement = new SettlementBuildingGame.Domain.Settlement.Settlement();
        var oak = new Oak();
        
        settlement.Inventory.AddResource(oak, quantity);
        world.Settlements = [settlement];
        
        var economy = new SettlementBuildingGame.Domain.World.Economy.Economy(world);

        // Act
        var value = economy.GetRealValueOf(oak);

        // Assert
        Assert.Equal(expectedValue, value);
    }

    [Fact]
    public void GetRealValueOf_Should_Handle_Empty_Settlements_List()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();
        world.Settlements = []; // Empty list
        var economy = new SettlementBuildingGame.Domain.World.Economy.Economy(world);
        var copperOre = new CopperOre();

        // Act
        var value = economy.GetRealValueOf(copperOre);

        // Assert
        // With no settlements, currentCount = 0, but Math.Max makes it 1
        // baseValue = equilibriumPrice * equilibriumCount / 1 = 15.0 * 15 / 1 = 225.0
        Assert.Equal(225.0m, value);
    }
}