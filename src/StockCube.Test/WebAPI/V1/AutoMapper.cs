#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using StockCube.Domain.CookingModule;
using StockCube.WebAPI.WebAPI.V1.RecipeModule;

namespace StockCube.WebAPI.V1;

public class WebAPIMappingProfile_Should
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;
    private readonly ILoggerFactory _loggerFactory;

    public WebAPIMappingProfile_Should()
    {
        // Newer AutoMapper versions no longer expose the ctor that accepted an ILoggerFactory.
        // Create configuration using the Action overload and then build the mapper.
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _configuration = configuration;
        _mapper = configuration.CreateMapper();
    }

    [Fact]
    [Trait("Category", "Unit")]
    [SuppressMessage("Style", "IDE0022:Use expression body for methods", Justification = "One line test")]
    public void HaveValidConfiguration()
    {
        // Arrange

        // Act
        _configuration.AssertConfigurationIsValid();

        // Assert
    }

    [Theory]
    [Trait("Category", "Unit")]
    [InlineData(typeof(Recipe), typeof(RecipeResponseDto))]
    public void ShouldSupportTasksModuleMappingFromSourceToDestination(Type source, Type destination)
    {
        // Arrange
        var instance = Activator.CreateInstance(source);

        // Act
        var result = _mapper.Map(instance, source, destination);

        // Assert
        result.Should().NotBeNull();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
