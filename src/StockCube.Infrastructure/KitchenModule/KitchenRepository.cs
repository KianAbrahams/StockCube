using MySqlConnector;
using StockCube.Domain.KitchenModule;
using StockCube.Infrastructure.MySQL;

namespace StockCube.Infrastructure.KitchenModule;

internal class KitchenRepository : IKitchenRepository
{
    private readonly MySqlConnectionManager _mySqlConnectionManager;

    public KitchenRepository(KitchenMySqlConnectionManager mySqlConnectionManager)
        => _mySqlConnectionManager = mySqlConnectionManager;

    public async Task<Section> CreateSection(Section section)
    {
        using var mySqlConnection = _mySqlConnectionManager.CreateConnection();
        await mySqlConnection.OpenAsync();
        if (mySqlConnection.State != System.Data.ConnectionState.Open) { throw new Exception("Connection Failed"); }
        //var count = connection.Execute(@"
        //  set nocount on
        //  create table #t(i int)
        //  set nocount off
        //  insert #t
        //  select @a a union all select @b
        //  set nocount on
        //  drop table #t", new { a = 1, b = 2 });
        //Assert.Equal(2, count);


        //var users = connection.Query<string>("select user_name from users where user_id = @userId", new { userId });
        //if (users.FirstOrDefault() is string userName)
        //    return Results.Ok(new { Name = userName });
        //else
        //    return Results.NotFound();
        await mySqlConnection.CloseAsync();
        return new Section() { Name = section.Name, Id = Guid.NewGuid() };
    }

    public Task<bool> DeleteSectionByIdAsync(Guid sectionId)
        => throw new NotImplementedException();

    public Task<Section> GetSectionByIdAsync(Guid Id)
        => throw new NotImplementedException();

    public Task<IEnumerable<Section>> GetSectionListAsync()
    {
        var sectionList = new List<Section>()
        {
            new Section() { Id = Guid.NewGuid(), Name = "Cupboard under the sink" },
            new Section() { Id = Guid.NewGuid(), Name = "Cupboard by the fridge"},
            new Section() { Id = Guid.NewGuid(), Name = "Fridge"},
            new Section() { Id = Guid.NewGuid(), Name = "Freezer"}
        };
        return Task.FromResult(sectionList.AsEnumerable());
    }
}
