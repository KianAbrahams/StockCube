namespace StockCube.WebAPI.WebAPI.V1.ShoppingModule;

public sealed record ShoppingResponseDto
{
    public string Name { get; set; } = string.Empty;
    public Guid Id { get; set; } = Guid.Empty;
}
