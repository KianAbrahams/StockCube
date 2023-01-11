using StockCube.Domain.KitchenModule;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void AddStockCubeDomainModel(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ISectionService, SectionService>();
        serviceCollection.AddTransient<ISectionValidator, SectionValidator>();
    }
}
