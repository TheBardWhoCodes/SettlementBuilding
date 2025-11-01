using System;

namespace SettlementBuildingGame.Domain.Settlement;

public class Settlement
{
    public Inventory Inventory { get; set; } = new Inventory();
}