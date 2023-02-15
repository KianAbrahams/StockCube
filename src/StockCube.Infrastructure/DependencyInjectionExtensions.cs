using Microsoft.Extensions.Configuration;
using StockCube.Infrastructure.KitchenModule;
using StockCube.Infrastructure.MySQL;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddStockCubeInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddConnectionManagers(configuration);

        serviceCollection.AddTransient<IKitchenRepository, KitchenRepository>();
        return serviceCollection;
    }

    private static IServiceCollection AddConnectionManagers(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var kitchenConnectionString = configuration.GetConnectionString("Kitchen");
        serviceCollection.AddTransient(x => new KitchenMySqlConnectionManager(kitchenConnectionString));

        var cookingConnectionString = configuration.GetConnectionString("Cooking");
        serviceCollection.AddTransient(x => new CookingMySqlConnectionManager(cookingConnectionString));

        var shoppingConnectionString = configuration.GetConnectionString("Shopping");
        serviceCollection.AddTransient(x => new ShoppingMySqlConnectionManager(shoppingConnectionString));

        var dboConnectionString = configuration.GetConnectionString("dbo");
        serviceCollection.AddTransient(x => new DBOMySqlConnectionManager(dboConnectionString));

        return serviceCollection;
    }
}
