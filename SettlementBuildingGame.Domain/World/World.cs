
namespace SettlementBuildingGame.Domain.World;

public class World
{
    public IEnumerable<Settlement.Settlement> Settlements { get; set; } = [];
    public Economy.Economy Economy { get => new(this); }
}
