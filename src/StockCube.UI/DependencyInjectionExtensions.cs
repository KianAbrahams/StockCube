using Microsoft.Extensions.DependencyInjection;
using StockCube.UI.Kitchen;
using StockCube.UI.Login;

namespace StockCube.UI;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddStockCubeUI(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAddSectionViewModel, AddSectionViewModel>();
        serviceCollection.AddTransient<IKitchenViewModel, KitchenViewModel>();
        serviceCollection.AddTransient<ILoginViewModel, LoginViewModel>();
        return serviceCollection;
    }
}
