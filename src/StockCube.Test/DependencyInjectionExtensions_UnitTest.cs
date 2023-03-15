#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Configuration;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace StockCube;

public class ServiceCollection_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task BeAbleToInstantiateAllRegiesteredTypes()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();
        // Adds user secrets to the builder config, perhaps there is a better way of doing this.
        builder.Configuration.AddEnvironmentVariables().AddUserSecrets(Assembly.GetExecutingAssembly());
        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddStockCubeInfrastructure(builder.Configuration);

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
