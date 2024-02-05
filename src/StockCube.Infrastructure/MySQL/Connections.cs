using System.Data;
using System.Data.SqlClient;
using Ardalis.GuardClauses;

namespace StockCube.Infrastructure.MySQL;

internal class KitchenSqlConnectionManager : SqlConnectionManager
{
    public KitchenSqlConnectionManager(string? connectionString)
        : base(connectionString) { }
}

internal class CookingSqlConnectionManager : SqlConnectionManager
{
    public CookingSqlConnectionManager(string? connectionString)
        : base(connectionString) { }
}

internal class ShoppingSqlConnectionManager : SqlConnectionManager
{
    public ShoppingSqlConnectionManager(string? connectionString)
        : base(connectionString) { }
}

internal class DBOSqlConnectionManager : SqlConnectionManager
{
    public DBOSqlConnectionManager(string? connectionString)
        : base(connectionString) { }
}

internal abstract class SqlConnectionManager
{
    private readonly string _connectionString = string.Empty;

    public SqlConnection CreateConnection() => new SqlConnection(_connectionString);

    public async Task<SqlConnection> CreateConnectionAsync()
    {
        var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync().ConfigureAwait(false);
        if (connection.State != ConnectionState.Open)
            throw new Exception("Connection Failed");
        return connection;
    }

    public SqlConnectionManager(string? connectionString)
        => _connectionString = Guard.Against.NullOrEmpty(connectionString);
}
