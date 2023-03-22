using StockCube.Framework.TypeMapping;

namespace StockCube.Domain.CookingModule;

public sealed record Recipe
{
    public const int NameMaxLength = 30;
    public string Name { get; set; } = string.Empty;
    public Guid Id { get; set; } = Guid.Empty;
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
