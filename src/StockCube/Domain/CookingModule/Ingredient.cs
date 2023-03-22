namespace StockCube.Domain.CookingModule;

public sealed record Ingredient
{
    public string Name { get; set; } = string.Empty;
    public Guid Id { get; set; } = Guid.Empty;
}
