using System.Data;
using Dapper;
using StockCube.Domain.CookingModule;
using StockCube.Infrastructure.MySQL;

namespace StockCube.Infrastructure.CookingModule;

internal class RecipeRepository : IRecipeRepository
{
    // TODO: Write USP for each method.
    private readonly MySqlConnectionManager _mySqlConnectionManager;

    public RecipeRepository(KitchenMySqlConnectionManager mySqlConnectionManager)
    => _mySqlConnectionManager = mySqlConnectionManager;
    public async Task<Recipe> CreateRecipe(Recipe recipe)
    {
        using var connection = await _mySqlConnectionManager.CreateConnectionAsync();

        recipe.Id = Guid.NewGuid();
        await connection.ExecuteAsync(Constants.Db.Cooking.USP_CreateRecipe, recipe, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        await connection.CloseAsync().ConfigureAwait(false);
        return recipe;
    }

    public async Task<bool> DeleteRecipeByIdAsync(Guid recipeId)
    {
        using var connection = await _mySqlConnectionManager.CreateConnectionAsync();

        var result = await connection.QuerySingleAsync(Constants.Db.Cooking.USP_DeleteRecipeById, new { Id = recipeId.ToString() }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        await connection.CloseAsync().ConfigureAwait(false);
        return result.deleted;
    }

    public async Task<Recipe> GetRecipeByIdAsync(Guid recipeId)
    {
        using var connection = await _mySqlConnectionManager.CreateConnectionAsync();

        var result = await connection.QuerySingleAsync<Recipe>(Constants.Db.Cooking.USP_GetRecipeById, new { Id = recipeId.ToString() }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        await connection.CloseAsync().ConfigureAwait(false);
        return result;
    }

    public async Task<IEnumerable<Recipe>> GetRecipeListAsync()
    {
        using var connection = await _mySqlConnectionManager.CreateConnectionAsync();

        var result = await connection.QueryAsync<Recipe>(Constants.Db.Cooking.USP_GetRecipeList, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        await connection.CloseAsync().ConfigureAwait(false);
        return result;
    }
}
