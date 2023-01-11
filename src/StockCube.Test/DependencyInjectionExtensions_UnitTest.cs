#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StockCube;

public class ServiceCollection_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task BeAbleToInstantiateAllRegiesteredTypes()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddStockCubeInfrastructure();

        var serviceProvider = services.BuildServiceProvider();

        // Act
        var result = new List<object>();

        foreach (var serviceDescriptor in services)
        {
            try
            {
                var instance = serviceProvider.GetService(serviceDescriptor.ServiceType);
                instance.Should().NotBeNull();
                instance.Should().BeAssignableTo(serviceDescriptor.ServiceType);

                // TODO: Convert to attribute based supression.
#pragma warning disable CS8604 // Possible null reference argument.
                result.Add(instance);
#pragma warning restore CS8604 // Possible null reference argument.
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to instantiate '{serviceDescriptor.ServiceType.FullName}'", ex);
            }
        }

        // Assert
        var expectedInstanceCountLessExclusions = services.Count();
        result.Should().HaveCount(expectedInstanceCountLessExclusions);
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
