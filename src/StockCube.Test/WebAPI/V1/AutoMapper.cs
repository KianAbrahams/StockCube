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
        // We don't actually want a logger. Automapper requires one as of 9.0.0
        _loggerFactory = NullLoggerFactory.Instance;

        var config = new MapperConfigurationExpression();
        config.AddProfile<MappingProfile>();
        _configuration = new MapperConfiguration(config, _loggerFactory);
        _mapper = _configuration.CreateMapper();
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
