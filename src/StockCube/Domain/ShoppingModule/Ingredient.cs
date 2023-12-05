namespace StockCube.Domain.ShoppingModule;

public sealed record Ingredient
{
    public const int NameMaxLength = 30;
    public string Name { get; set; } = string.Empty;
    public Guid Id { get; set; } = Guid.Empty;
}
