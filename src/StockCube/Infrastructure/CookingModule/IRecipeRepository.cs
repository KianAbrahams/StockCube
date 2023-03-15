using StockCube.Domain.CookingModule;

namespace StockCube.Infrastructure.CookingModule;

public interface IRecipeRepository
{
    Task<IEnumerable<Recipe>> GetRecipeListAsync();

    Task<Recipe> GetRecipeByIdAsync(Guid Id);

    Task<bool> DeleteRecipeByIdAsync(Guid recipeId);
    Task<Recipe> CreateRecipe(Recipe recipe);
}
