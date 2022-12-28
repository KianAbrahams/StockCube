namespace StockCube.Domain.KitchenModule;

public sealed record Section
{
    public string Name { get; set; }
    public Guid Id { get; set; }
}
