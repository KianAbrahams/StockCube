#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;
using System.Reflection.Metadata;
using StockCube.Domain;
using StockCube.WebAPI;
using AutoMapper;
using FluentAssertions;
using Xunit;
using StockCube.Domain.CookingModule;
using StockCube.WebAPI.WebAPI.V1.RecipeModule;

namespace StockCube.WebAPI.V1;

public class WebAPIMappingProfile_Should
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public WebAPIMappingProfile_Should()
    {
        _configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
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
