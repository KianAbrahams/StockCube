namespace StockCube.Domain.CookingModule;

public interface IRecipeService
{
    Task<Result<IEnumerable<Recipe>>> GetListAsync();
    Task<Result<Recipe>> GetByIdAsync(Guid Id);
    Task<Result> DeleteAsync(Guid recipeId);
    Task<Result<Recipe>> CreateRecipe(Recipe recipe);
}
