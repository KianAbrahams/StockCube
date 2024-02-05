using Microsoft.Extensions.Configuration;
using StockCube.Infrastructure.CookingModule;
using StockCube.Infrastructure.KitchenModule;
using StockCube.Infrastructure.MySQL;
using StockCube.Infrastructure.ShoppingModule;
using StockCube.Infrastructure.ShoppingRepository;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddStockCubeInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddConnectionManagers(configuration);

        // Cooking Module
        serviceCollection.AddTransient<IRecipeRepository, RecipeRepository>();

        // Kitchen Module
        serviceCollection.AddTransient<IKitchenRepository, KitchenRepository>();

        // Kitchen Module
        serviceCollection.AddTransient<IShoppingRepository, ShoppingRepository>();

        return serviceCollection;
    }

    private static IServiceCollection AddConnectionManagers(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var kitchenConnectionString = configuration.GetConnectionString("Kitchen");
        serviceCollection.AddTransient(x => new KitchenSqlConnectionManager(kitchenConnectionString));

        var cookingConnectionString = configuration.GetConnectionString("Cooking");
        serviceCollection.AddTransient(x => new CookingSqlConnectionManager(cookingConnectionString));

        var shoppingConnectionString = configuration.GetConnectionString("Shopping");
        serviceCollection.AddTransient(x => new ShoppingSqlConnectionManager(shoppingConnectionString));

        var dboConnectionString = configuration.GetConnectionString("dbo");
        serviceCollection.AddTransient(x => new DBOSqlConnectionManager(dboConnectionString));

        return serviceCollection;
    }
}
