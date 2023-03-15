namespace StockCube.WebAPI.WebAPI.V1.RecipeModule;

public sealed record CreateRecipeRequestDto
{
    public string Name { get; set; } = string.Empty;
}
