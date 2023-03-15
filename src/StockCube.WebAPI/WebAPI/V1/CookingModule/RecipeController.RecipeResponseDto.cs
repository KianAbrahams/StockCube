using StockCube.WebAPI.WebAPI.V1.CookingModule;

namespace StockCube.WebAPI.WebAPI.V1.RecipeModule;

public sealed record RecipeResponseDto
{
    public string Name { get; set; } = string.Empty;
    public Guid Id { get; set; } = Guid.Empty;
    public List<IngredientResponseDto> Ingredients { get; set; } = new List<IngredientResponseDto>();
}
