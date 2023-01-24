using Microsoft.Extensions.DependencyInjection;
using StockCube.UI.Kitchen;

namespace StockCube.UI;

public static class DependencyInjectionExtensions
{
    public static void AddStockCubeUI(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAddSectionViewModel, AddSectionViewModel>();
    }
}
