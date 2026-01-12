using Microsoft.Extensions.Configuration;
using StockCube.Infrastructure.CookingModule;
using StockCube.Infrastructure.KitchenModule;
using StockCube.Infrastructure.MySQL;
using StockCube.Infrastructure.ShoppingModule;
using StockCube.Infrastructure.ShoppingRepository;
using System;

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

        // Shopping Module
        serviceCollection.AddTransient<IShoppingRepository, ShoppingRepository>();

        return serviceCollection;
    }

    private static IServiceCollection AddConnectionManagers(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        // Fail fast with clear error messages if a connection string is missing
        var kitchenConnectionString = configuration.GetConnectionString("Kitchen")
            ?? throw new InvalidOperationException("Connection string 'Kitchen' is not configured. Ensure ConnectionStrings:Kitchen exists in appsettings or user secrets.");
        serviceCollection.AddTransient(_ => new KitchenSqlConnectionManager(kitchenConnectionString));

        var cookingConnectionString = configuration.GetConnectionString("Cooking")
            ?? throw new InvalidOperationException("Connection string 'Cooking' is not configured. Ensure ConnectionStrings:Cooking exists in appsettings or user secrets.");
        serviceCollection.AddTransient(_ => new CookingSqlConnectionManager(cookingConnectionString));

        var shoppingConnectionString = configuration.GetConnectionString("Shopping")
            ?? throw new InvalidOperationException("Connection string 'Shopping' is not configured. Ensure ConnectionStrings:Shopping exists in appsettings or user secrets.");
        serviceCollection.AddTransient(_ => new ShoppingSqlConnectionManager(shoppingConnectionString));

        // Use "DBO" to match your secrets.json (case-insensitive, but keep names consistent)
        var dboConnectionString = configuration.GetConnectionString("DBO")
            ?? throw new InvalidOperationException("Connection string 'DBO' is not configured. Ensure ConnectionStrings:DBO exists in appsettings or user secrets.");
        serviceCollection.AddTransient(_ => new DBOSqlConnectionManager(dboConnectionString));

        return serviceCollection;
    }
}