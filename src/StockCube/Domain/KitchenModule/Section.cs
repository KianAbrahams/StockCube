namespace StockCube.Domain.KitchenModule;

public sealed record Section
{
    public string Name { get; set; } = string.Empty;
    public Guid Id { get; set; } = Guid.Empty;
}
