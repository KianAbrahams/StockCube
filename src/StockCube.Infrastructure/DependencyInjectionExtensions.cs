using StockCube.Infrastructure.KitchenModule;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void AddStockCubeInfrastructure(this IServiceCollection serviceCollection)
        => serviceCollection.AddTransient<IKitchenRepository, KitchenRepository>();
}
