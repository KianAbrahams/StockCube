using StockCube.WebAPI.WebAPI.V1.KitchenModule;

namespace StockCube.WebAPI;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddWebAPI(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ISectionController, SectionController>();
        return serviceCollection;
    }
}
