using StockCube.Domain.CookingModule;
using StockCube.Framework.TypeMapping;

namespace StockCube.WebAPI.WebAPI.V1.CookingModule;

public sealed record IngredientResponseDto : IMapFrom<Ingredient>
{
}
