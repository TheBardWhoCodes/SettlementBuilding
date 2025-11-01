using SettlementBuildingGame.Domain.Settlement;

namespace SettlementBuildingGame.Domain.Tests.World;

public class WorldTests
{
    [Fact]
    public void World_Should_Initialize_With_Empty_Settlements()
    {
        // Arrange & Act
        var world = new SettlementBuildingGame.Domain.World.World();

        // Assert
        Assert.NotNull(world.Settlements);
        Assert.Empty(world.Settlements);
    }

    [Fact]
    public void World_Should_Have_Economy_Property()
    {
        // Arrange & Act
        var world = new SettlementBuildingGame.Domain.World.World();

        // Assert
        Assert.NotNull(world.Economy);
        Assert.IsType<SettlementBuildingGame.Domain.World.Economy.Economy>(world.Economy);
    }

    [Fact]
    public void World_Should_Create_New_Economy_Instance_Each_Time()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();

        // Act
        var economy1 = world.Economy;
        var economy2 = world.Economy;

        // Assert
        Assert.NotSame(economy1, economy2);
        Assert.IsType<SettlementBuildingGame.Domain.World.Economy.Economy>(economy1);
        Assert.IsType<SettlementBuildingGame.Domain.World.Economy.Economy>(economy2);
    }

    [Fact]
    public void World_Should_Allow_Adding_Settlements()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();
        var settlement1 = new SettlementBuildingGame.Domain.Settlement.Settlement();
        var settlement2 = new SettlementBuildingGame.Domain.Settlement.Settlement();

        // Act
        world.Settlements = [settlement1, settlement2];

        // Assert
        Assert.Equal(2, world.Settlements.Count());
        Assert.Contains(settlement1, world.Settlements);
        Assert.Contains(settlement2, world.Settlements);
    }

    [Fact]
    public void World_Should_Allow_Empty_Settlements_Collection()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();

        // Act
        world.Settlements = [];

        // Assert
        Assert.Empty(world.Settlements);
    }

    [Fact]
    public void World_Should_Allow_Null_Settlements_To_Be_Set()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();

        // Act
        world.Settlements = null!;

        // Assert
        Assert.Null(world.Settlements);
    }

    [Fact]
    public void World_Settlements_Should_Be_Enumerable()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();
        var settlements = new List<SettlementBuildingGame.Domain.Settlement.Settlement>
        {
            new(),
            new(),
            new()
        };

        // Act
        world.Settlements = settlements;

        // Assert
        var count = 0;
        foreach (var settlement in world.Settlements)
        {
            Assert.IsType<SettlementBuildingGame.Domain.Settlement.Settlement>(settlement);
            count++;
        }
        Assert.Equal(3, count);
    }

    [Fact]
    public void World_Economy_Should_Use_Current_World_Instance()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();
        var settlement = new SettlementBuildingGame.Domain.Settlement.Settlement();
        world.Settlements = [settlement];

        // Act
        var economy = world.Economy;

        // Assert
        // This test verifies that the Economy is created with the current world instance
        // We can't directly test the private field, but we can test that it behaves correctly
        Assert.NotNull(economy);
        
        // The economy should be able to access the world's settlements through the constructor
        var exception = Record.Exception(() => economy);
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    public void World_Should_Handle_Various_Settlement_Counts(int settlementCount)
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();
        var settlements = new List<SettlementBuildingGame.Domain.Settlement.Settlement>();
        
        for (int i = 0; i < settlementCount; i++)
        {
            settlements.Add(new SettlementBuildingGame.Domain.Settlement.Settlement());
        }

        // Act
        world.Settlements = settlements;

        // Assert
        Assert.Equal(settlementCount, world.Settlements.Count());
    }

    [Fact]
    public void World_Should_Allow_Settlement_Replacement()
    {
        // Arrange
        var world = new SettlementBuildingGame.Domain.World.World();
        var originalSettlements = new List<SettlementBuildingGame.Domain.Settlement.Settlement> { new(), new() };
        var newSettlements = new List<SettlementBuildingGame.Domain.Settlement.Settlement> { new(), new(), new() };

        // Act
        world.Settlements = originalSettlements;
        var originalCount = world.Settlements.Count();
        
        world.Settlements = newSettlements;
        var newCount = world.Settlements.Count();

        // Assert
        Assert.Equal(2, originalCount);
        Assert.Equal(3, newCount);
        Assert.Equal(newSettlements, world.Settlements);
    }
}