namespace StockCube.Domain.CookingModule;

public sealed record Recipe
{
    public const int NameMaxLength = 30;
    public string Name { get; set; } = string.Empty;
    public Guid Id { get; set; } = Guid.Empty;
}
