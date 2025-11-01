using SettlementBuildingGame.Domain.Resources;

namespace SettlementBuildingGame.Domain.Settlement;

public class Inventory
{
    private readonly Dictionary<Type, int> _resources = [];

    public void AddResource(Resource resource, int amount = 1)
    {
        var type = resource.GetType();
        if (_resources.ContainsKey(type))
        {
            _resources[type] += amount;
        }
        else
        {
            _resources[type] = amount;
        }
    }

    public int GetResourceCount(Type resourceType)
    {
        return _resources.TryGetValue(resourceType, out var count) ? count : 0;
    }
}
