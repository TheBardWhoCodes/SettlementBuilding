using System;
using SettlementBuildingGame.Domain.Resources;
using SettlementBuildingGame.Domain.Resources.Ore;
using SettlementBuildingGame.Domain.Resources.Wood;

namespace SettlementBuildingGame.Domain.World.Economy;

public class Economy(World world)
{
    private readonly World _world = world;

    /// <summary>
    /// Represents the equilibrium state of resources in the economy.
    /// </summary>
    private static readonly Dictionary<Type, (int, decimal)> ResourceEquilibrium = new()
    {
        { typeof(Oak), (10, 10.0m) },
        { typeof(CopperOre), (15, 15.0m) },
        { typeof(IronOre), (20, 20.0m) },

        // Add other resources and their base values here
    };

    public decimal GetRealValueOf(Resource resource)
    {
        if (ResourceEquilibrium.TryGetValue(resource.GetType(), out var equilibrium))
        {
            int equilibriumCount = equilibrium.Item1;
            decimal equilibriumPrice = equilibrium.Item2;
            int currentCount = _world.Settlements.Sum(s => s.Inventory.GetResourceCount(resource.GetType()));
            decimal baseValue = equilibriumPrice * equilibriumCount / Math.Max(currentCount, 1);
            return baseValue;
        }

        throw new ArgumentException("Resource does not have a defined equilibrium", nameof(resource));
    }
}
