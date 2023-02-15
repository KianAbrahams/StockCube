using StockCube.Domain.KitchenModule;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddStockCubeDomainModel(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ISectionService, SectionService>();
        serviceCollection.AddTransient<ISectionValidator, SectionValidator>();
        return serviceCollection;
    }
}
