using System.Data;
using Dapper;
using StockCube.Domain.KitchenModule;
using StockCube.Infrastructure.MySQL;

namespace StockCube.Infrastructure.KitchenModule;

internal class KitchenRepository : IKitchenRepository
{
    private readonly SqlConnectionManager _mySqlConnectionManager;

    public KitchenRepository(KitchenSqlConnectionManager mySqlConnectionManager)
        => _mySqlConnectionManager = mySqlConnectionManager;

    public async Task<Section> CreateSection(Section section)
    {
        using var connection = await _mySqlConnectionManager.CreateConnectionAsync();
        
        section.Id= Guid.NewGuid();
        await connection.ExecuteAsync(Constants.Db.Kitchen.USP_CreateSection, section, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        await connection.CloseAsync().ConfigureAwait(false);
        return section;
    }

    public async Task<bool> DeleteSectionByIdAsync(Guid sectionId)
    {
        using var connection = await _mySqlConnectionManager.CreateConnectionAsync();

        var result = await connection.QuerySingleAsync(Constants.Db.Kitchen.USP_DeleteSectionById, new { Id = sectionId.ToString() }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        await connection.CloseAsync().ConfigureAwait(false);
        return result.deleted;
    }

    public async Task<Section> GetSectionByIdAsync(Guid sectionId)
    {
        using var connection = await _mySqlConnectionManager.CreateConnectionAsync();

        var result = await connection.QuerySingleAsync<Section>(Constants.Db.Kitchen.USP_GetSectionById, new { Id = sectionId.ToString() }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        await connection.CloseAsync().ConfigureAwait(false);
        return result;
    }

    public async Task<IEnumerable<Section>> GetSectionListAsync()
    {
        using var connection = await _mySqlConnectionManager.CreateConnectionAsync();

        var result = await connection.QueryAsync<Section>(Constants.Db.Kitchen.USP_GetSectionList, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        await connection.CloseAsync().ConfigureAwait(false);
        return result;
    }

    public async Task<IEnumerable<SectionFoodItem>> GetSectionFoodItemListAsync(Guid sectionId)
    {
        using var connection = await _mySqlConnectionManager.CreateConnectionAsync();

        // TODO: Write new stored procedure for getting 
        var result = await connection.QueryAsync<SectionFoodItem>(Constants.Db.Kitchen.USP_GetSectionList, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        await connection.CloseAsync().ConfigureAwait(false);
        return result;
    }

    public async Task<SectionFoodItem> CreateSectionFoodItem(SectionFoodItem sectionFoodItem)
    {
        using var connection = await _mySqlConnectionManager.CreateConnectionAsync();

        sectionFoodItem.FoodItemId = Guid.NewGuid();
        sectionFoodItem.SectionFoodItemId = Guid.NewGuid();
        // TODO: Write new stored procedure for creating a new SectionFoodItem
        await connection.ExecuteAsync(Constants.Db.Kitchen.USP_CreateSection, sectionFoodItem, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        await connection.CloseAsync().ConfigureAwait(false);
        return sectionFoodItem;
    }

    public async Task<SectionFoodItem> UpdateSectionFoodItem(SectionFoodItem sectionFoodItem)
    {
        using var connection = await _mySqlConnectionManager.CreateConnectionAsync();
        // TODO: Write new stored procedure for updating a SectionFoodItem
        await connection.ExecuteAsync(Constants.Db.Kitchen.USP_CreateSection, sectionFoodItem, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        await connection.CloseAsync().ConfigureAwait(false);
        return sectionFoodItem;
    }

    public async Task<bool> DeleteSectionFoodItemAsync(Guid sectionFoodItemId)
    {
        using var connection = await _mySqlConnectionManager.CreateConnectionAsync();

        // TODO: Write new stored procedure for Deleting a SectionFoodItem
        var result = await connection.QuerySingleAsync(Constants.Db.Kitchen.USP_DeleteSectionById, new { Id = sectionFoodItemId.ToString() }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        await connection.CloseAsync().ConfigureAwait(false);
        return result.deleted;
    }
}
