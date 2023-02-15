using Ardalis.GuardClauses;
using MySqlConnector;

namespace StockCube.Infrastructure.MySQL;

internal class KitchenMySqlConnectionManager : MySqlConnectionManager
{
    public KitchenMySqlConnectionManager(string? connectionString)
        : base(connectionString) { }
}

internal class CookingMySqlConnectionManager : MySqlConnectionManager
{
    public CookingMySqlConnectionManager(string? connectionString)
        : base(connectionString) { }
}

internal class ShoppingMySqlConnectionManager : MySqlConnectionManager
{
    public ShoppingMySqlConnectionManager(string? connectionString)
        : base(connectionString) { }
}

internal class DBOMySqlConnectionManager : MySqlConnectionManager
{
    public DBOMySqlConnectionManager(string? connectionString)
        : base(connectionString) { }
}

internal abstract class MySqlConnectionManager
{
    private readonly string _connectionString = string.Empty;

    public MySqlConnection CreateConnection() => new MySqlConnection(_connectionString);

    public MySqlConnectionManager(string? connectionString)
        => _connectionString = Guard.Against.NullOrEmpty(connectionString);
}
