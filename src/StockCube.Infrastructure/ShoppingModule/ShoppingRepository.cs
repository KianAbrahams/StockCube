using System.Data;
using Dapper;
using StockCube.Domain.KitchenModule;
using StockCube.Domain.ShoppingModule;
using StockCube.Infrastructure.MySQL;
using StockCube.Infrastructure.ShoppingRepository;

namespace StockCube.Infrastructure.ShoppingModule;

internal class ShoppingRepository : IShoppingRepository
{
    private readonly SqlConnectionManager _mySqlConnectionManager;

    public ShoppingRepository(KitchenSqlConnectionManager mySqlConnectionManager)
        => _mySqlConnectionManager = mySqlConnectionManager;

    public async Task<IEnumerable<Ingredient>> GetShoppingListAsync()
    {
        using var connection = await _mySqlConnectionManager.CreateConnectionAsync();

        var result = await connection.QueryAsync<Ingredient>(Constants.Db.Shopping.USP_GetShoppingList, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        await connection.CloseAsync().ConfigureAwait(false);
        return result;
    }
    public async Task<bool> DeleteIngredientByIdAsync(Guid sectionId)
    {
        using var connection = await _mySqlConnectionManager.CreateConnectionAsync();

        var result = await connection.QuerySingleAsync(Constants.Db.Shopping.USP_DeleteIngredientById, new { Id = sectionId.ToString() }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        await connection.CloseAsync().ConfigureAwait(false);
        return result.deleted;
    }

    public async Task<Ingredient> AddIngredient(Ingredient ingredient)
    {
        using var connection = await _mySqlConnectionManager.CreateConnectionAsync();

        ingredient.Id = Guid.NewGuid();
        await connection.ExecuteAsync(Constants.Db.Shopping.USP_UpdateShoppingList, ingredient, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        await connection.CloseAsync().ConfigureAwait(false);
        return ingredient;
    }
}
