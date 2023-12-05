using StockCube.Domain.CookingModule;
using StockCube.Domain.KitchenModule;
using StockCube.Domain.ShoppingModule;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddStockCubeDomainModel(this IServiceCollection serviceCollection)
    {
        // Cooking Module
        serviceCollection.AddTransient<IRecipeService, RecipeService>();
        serviceCollection.AddTransient<IRecipeValidator, RecipeValidator>();

        // Kitchen Module
        serviceCollection.AddTransient<ISectionService, SectionService>();
        serviceCollection.AddTransient<ISectionValidator, SectionValidator>();

        // Kitchen Module
        serviceCollection.AddTransient<IShoppingService, ShoppingService>();
        serviceCollection.AddTransient<IShoppingValidator, ShoppingValidator>();

        return serviceCollection;
    }
}
