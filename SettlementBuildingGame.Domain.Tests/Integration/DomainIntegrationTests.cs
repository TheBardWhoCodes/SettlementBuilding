using SettlementBuildingGame.Domain.Settlement;
using SettlementBuildingGame.Domain.Resources.Ore;
using SettlementBuildingGame.Domain.Resources.Wood;
using SettlementBuildingGame.Domain.Resources.Stone;

namespace SettlementBuildingGame.Domain.Tests.Integration;

/// <summary>
/// Integration tests that verify the interaction between different components of the domain
/// </summary>
public class DomainIntegrationTests
{
    [Fact]
    public void Complete_Resource_Management_Workflow_Should_Work()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();
        var settlement1 = new SettlementBuildingGame.Domain.Settlement.Settlement();
        var settlement2 = new SettlementBuildingGame.Domain.Settlement.Settlement();

        var oak = new Oak();
        var copperOre = new CopperOre();
        var ironOre = new IronOre();

        // Act - Add resources to settlements
        settlement1.Inventory.AddResource(oak, 5);
        settlement1.Inventory.AddResource(copperOre, 10);

        settlement2.Inventory.AddResource(oak, 15);
        settlement2.Inventory.AddResource(ironOre, 25);

        world.Settlements = [settlement1, settlement2];

        var economy = world.Economy;

        // Assert - Verify resource counts
        Assert.Equal(5, settlement1.Inventory.GetResourceCount(typeof(Oak)));
        Assert.Equal(10, settlement1.Inventory.GetResourceCount(typeof(CopperOre)));
        Assert.Equal(0, settlement1.Inventory.GetResourceCount(typeof(IronOre)));

        Assert.Equal(15, settlement2.Inventory.GetResourceCount(typeof(Oak)));
        Assert.Equal(0, settlement2.Inventory.GetResourceCount(typeof(CopperOre)));
        Assert.Equal(25, settlement2.Inventory.GetResourceCount(typeof(IronOre)));

        // Assert - Verify economy calculations
        var oakValue = economy.GetRealValueOf(oak); // Total Oak: 20, Equilibrium: 10, Expected: 5.0
        var copperValue = economy.GetRealValueOf(copperOre); // Total Copper: 10, Equilibrium: 15, Expected: 22.5
        var ironValue = economy.GetRealValueOf(ironOre); // Total Iron: 25, Equilibrium: 20, Expected: 16.0

        Assert.Equal(5.0m, oakValue);
        Assert.Equal(22.5m, copperValue);
        Assert.Equal(16.0m, ironValue);
    }

    [Fact]
    public void Resource_Interfaces_Should_Work_Across_Different_Resource_Types()
    {
        // Arrange
        var settlement = new SettlementBuildingGame.Domain.Settlement.Settlement();
        var minableResources = new List<SettlementBuildingGame.Domain.Resources.Ore.Interfaces.IMineable>
        {
            new CopperOre(),
            new IronOre(),
            new GoldOre()
        };

        var chopableResources = new List<SettlementBuildingGame.Domain.Resources.Wood.Interfaces.IChopable>
        {
            new Oak()
        };

        var quarryableResources = new List<SettlementBuildingGame.Domain.Resources.Stone.Interfaces.IQuarryable>
        {
            new Stone()
        };

        // Act & Assert - Mine operations
        foreach (var resource in minableResources)
        {
            var exception = Record.Exception(() => resource.Mine());
            Assert.Null(exception);

            // Add to settlement to verify polymorphism works
            if (resource is CopperOre copper)
                settlement.Inventory.AddResource(copper);
            else if (resource is IronOre iron)
                settlement.Inventory.AddResource(iron);
            else if (resource is GoldOre gold)
                settlement.Inventory.AddResource(gold);
        }

        // Act & Assert - Chop operations
        foreach (var resource in chopableResources)
        {
            var exception = Record.Exception(() => resource.Chop());
            Assert.Null(exception);

            if (resource is Oak oak)
                settlement.Inventory.AddResource(oak);
        }

        // Act & Assert - Quarry operations
        foreach (var resource in quarryableResources)
        {
            var exception = Record.Exception(() => resource.Quarry());
            Assert.Null(exception);

            if (resource is Stone stone)
                settlement.Inventory.AddResource(stone);
        }

        // Verify all resources were added
        Assert.Equal(1, settlement.Inventory.GetResourceCount(typeof(CopperOre)));
        Assert.Equal(1, settlement.Inventory.GetResourceCount(typeof(IronOre)));
        Assert.Equal(1, settlement.Inventory.GetResourceCount(typeof(GoldOre)));
        Assert.Equal(1, settlement.Inventory.GetResourceCount(typeof(Oak)));
        Assert.Equal(1, settlement.Inventory.GetResourceCount(typeof(Stone)));
    }

    [Fact]
    public void Multiple_Worlds_Should_Have_Independent_Economies()
    {
        // Arrange
        var world1 = new SettlementBuildingGame.Domain.World.World();
        var world2 = new SettlementBuildingGame.Domain.World.World();

        var settlement1 = new SettlementBuildingGame.Domain.Settlement.Settlement();
        var settlement2 = new SettlementBuildingGame.Domain.Settlement.Settlement();

        var oak = new Oak();

        settlement1.Inventory.AddResource(oak, 5);  // Scarce in world1
        settlement2.Inventory.AddResource(oak, 20); // Abundant in world2

        world1.Settlements = [settlement1];
        world2.Settlements = [settlement2];

        // Act
        var economy1 = world1.Economy;
        var economy2 = world2.Economy;

        var value1 = economy1.GetRealValueOf(oak); // Should be high (scarce)
        var value2 = economy2.GetRealValueOf(oak); // Should be low (abundant)

        // Assert
        Assert.Equal(20.0m, value1); // 10 * 10 / 5 = 20
        Assert.Equal(5.0m, value2);  // 10 * 10 / 20 = 5
        Assert.NotEqual(value1, value2);
    }

    [Fact]
    public void Economy_Should_Update_When_Settlement_Resources_Change()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();
        var settlement = new SettlementBuildingGame.Domain.Settlement.Settlement();
        var oak = new Oak();

        settlement.Inventory.AddResource(oak, 5);
        world.Settlements = [settlement];

        // Act & Assert - Initial state (scarce)
        var initialEconomy = world.Economy;
        var initialValue = initialEconomy.GetRealValueOf(oak);
        Assert.Equal(20.0m, initialValue); // 10 * 10 / 5 = 20

        // Add more resources
        settlement.Inventory.AddResource(oak, 15); // Total becomes 20

        // New economy instance should reflect updated inventory
        var updatedEconomy = world.Economy;
        var updatedValue = updatedEconomy.GetRealValueOf(oak);
        Assert.Equal(5.0m, updatedValue); // 10 * 10 / 20 = 5

        Assert.NotEqual(initialValue, updatedValue);
    }

    [Theory]
    [InlineData(typeof(CopperOre), typeof(SettlementBuildingGame.Domain.Resources.Ore.Interfaces.IMineable))]
    [InlineData(typeof(IronOre), typeof(SettlementBuildingGame.Domain.Resources.Ore.Interfaces.IMineable))]
    [InlineData(typeof(GoldOre), typeof(SettlementBuildingGame.Domain.Resources.Ore.Interfaces.IMineable))]
    [InlineData(typeof(Oak), typeof(SettlementBuildingGame.Domain.Resources.Wood.Interfaces.IChopable))]
    [InlineData(typeof(Stone), typeof(SettlementBuildingGame.Domain.Resources.Stone.Interfaces.IQuarryable))]
    public void Resource_Types_Should_Implement_Expected_Interfaces(Type resourceType, Type expectedInterface)
    {
        // Arrange & Act
        var resource = Activator.CreateInstance(resourceType);

        // Assert
        Assert.True(expectedInterface.IsAssignableFrom(resourceType));
        Assert.IsAssignableFrom(expectedInterface, resource);
    }
}