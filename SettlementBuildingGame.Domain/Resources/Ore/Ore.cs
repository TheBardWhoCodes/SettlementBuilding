using SettlementBuildingGame.Domain.Resources.Ore.Interfaces;

namespace SettlementBuildingGame.Domain.Resources.Ore;

public abstract class Ore : Resource, IMineable
{
    public void Mine()
    {
        // Implementation of mining logic
    }
}
