using SettlementBuildingGame.Domain.Settlement;
using SettlementBuildingGame.Domain.Resources;
using SettlementBuildingGame.Domain.Resources.Ore;
using SettlementBuildingGame.Domain.Resources.Wood;
using SettlementBuildingGame.Domain.Resources.Stone;

namespace SettlementBuildingGame.Domain.Tests.Settlement;

public class InventoryTests
{
    [Fact]
    public void AddResource_Should_Add_New_Resource_To_Inventory()
    {
        // Arrange
        var inventory = new Inventory();
        var oak = new Oak();

        // Act
        inventory.AddResource(oak);

        // Assert
        var count = inventory.GetResourceCount(typeof(Oak));
        Assert.Equal(1, count);
    }

    [Fact]
    public void AddResource_Should_Increment_Existing_Resource_Count()
    {
        // Arrange
        var inventory = new Inventory();
        var oak = new Oak();

        // Act
        inventory.AddResource(oak);
        inventory.AddResource(oak);

        // Assert
        var count = inventory.GetResourceCount(typeof(Oak));
        Assert.Equal(2, count);
    }

    [Fact]
    public void AddResource_With_Amount_Should_Add_Specified_Quantity()
    {
        // Arrange
        var inventory = new Inventory();
        var copperOre = new CopperOre();

        // Act
        inventory.AddResource(copperOre, 5);

        // Assert
        var count = inventory.GetResourceCount(typeof(CopperOre));
        Assert.Equal(5, count);
    }

    [Fact]
    public void AddResource_With_Amount_Should_Increment_Existing_Resource_By_Amount()
    {
        // Arrange
        var inventory = new Inventory();
        var ironOre = new IronOre();

        // Act
        inventory.AddResource(ironOre, 3);
        inventory.AddResource(ironOre, 7);

        // Assert
        var count = inventory.GetResourceCount(typeof(IronOre));
        Assert.Equal(10, count);
    }

    [Fact]
    public void GetResourceCount_Should_Return_Zero_For_Nonexistent_Resource()
    {
        // Arrange
        var inventory = new Inventory();

        // Act
        var count = inventory.GetResourceCount(typeof(GoldOre));

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void GetResourceCount_Should_Track_Different_Resource_Types_Separately()
    {
        // Arrange
        var inventory = new Inventory();
        var oak = new Oak();
        var stone = new Stone();
        var copperOre = new CopperOre();

        // Act
        inventory.AddResource(oak, 3);
        inventory.AddResource(stone, 5);
        inventory.AddResource(copperOre, 2);

        // Assert
        Assert.Equal(3, inventory.GetResourceCount(typeof(Oak)));
        Assert.Equal(5, inventory.GetResourceCount(typeof(Stone)));
        Assert.Equal(2, inventory.GetResourceCount(typeof(CopperOre)));
        Assert.Equal(0, inventory.GetResourceCount(typeof(IronOre))); // Not added
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    public void AddResource_Should_Handle_Various_Amounts(int amount)
    {
        // Arrange
        var inventory = new Inventory();
        var resource = new Oak();

        // Act
        inventory.AddResource(resource, amount);

        // Assert
        var count = inventory.GetResourceCount(typeof(Oak));
        Assert.Equal(amount, count);
    }
}

public class SettlementTests
{
    [Fact]
    public void Settlement_Should_Have_Inventory_Property()
    {
        // Arrange & Act
        var settlement = new SettlementBuildingGame.Domain.Settlement.Settlement();

        // Assert
        Assert.NotNull(settlement.Inventory);
        Assert.IsType<Inventory>(settlement.Inventory);
    }

    [Fact]
    public void Settlement_Should_Initialize_With_Empty_Inventory()
    {
        // Arrange & Act
        var settlement = new SettlementBuildingGame.Domain.Settlement.Settlement();

        // Assert
        var oakCount = settlement.Inventory.GetResourceCount(typeof(Oak));
        var stoneCount = settlement.Inventory.GetResourceCount(typeof(Stone));
        var copperCount = settlement.Inventory.GetResourceCount(typeof(CopperOre));

        Assert.Equal(0, oakCount);
        Assert.Equal(0, stoneCount);
        Assert.Equal(0, copperCount);
    }

    [Fact]
    public void Settlement_Inventory_Should_Be_Mutable()
    {
        // Arrange
        var settlement = new SettlementBuildingGame.Domain.Settlement.Settlement();
        var oak = new Oak();

        // Act
        settlement.Inventory.AddResource(oak, 5);

        // Assert
        var count = settlement.Inventory.GetResourceCount(typeof(Oak));
        Assert.Equal(5, count);
    }

    [Fact]
    public void Settlement_Should_Allow_Inventory_Replacement()
    {
        // Arrange
        var settlement = new SettlementBuildingGame.Domain.Settlement.Settlement();
        var newInventory = new Inventory();
        var stone = new Stone();
        newInventory.AddResource(stone, 10);

        // Act
        settlement.Inventory = newInventory;

        // Assert
        var count = settlement.Inventory.GetResourceCount(typeof(Stone));
        Assert.Equal(10, count);
    }
}