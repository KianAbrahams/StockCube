using StockCube.Domain.CookingModule;
using StockCube.Domain.KitchenModule;

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

        return serviceCollection;
    }
}
