#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using StockCube.Domain.KitchenModule;
using StockCube.WebAPI.WebAPI.V1.KitchenModule;

namespace StockCube.Test.WebAPI.V1.KitchenModule;

public sealed class SectionController_Get_Should
{
    [Fact]
    public async Task Return_Status200Ok_WithListOfSections()
    {
        // arrange
        var testSection = new List<Section>()
        {
            new Section { Name = "Section1", Id = Guid.NewGuid() },
            new Section { Name = "Section2", Id = Guid.NewGuid()}
        };
        var mockSectionService = Substitute.For<ISectionService>();
        mockSectionService.GetListAsync().Returns(Task.FromResult(testSection.AsEnumerable()));

        var services = new ServiceCollection();
        services.AddTransient<SectionController>();
        services.AddSingleton(mockSectionService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<SectionController>().GetListAsync();

        // assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result;
        result.Value.Should().NotBeNull();
        result.Value.Should().BeAssignableTo<IEnumerable<SectionResponseDto>>();

        var value = (IEnumerable<SectionResponseDto>)result.Value;
        value.Should().NotBeNull();
        value.Should().HaveCount(testSection.Count);

        var items = value.ToList();
        items.First().Should().BeOfType<SectionResponseDto>();
        for (var i = 0; i < value.Count(); i++)
        {
            items[i].Name.Should().Be(testSection[i].Name);
        }
    }
}

public sealed class SectionController_GetById_Should
{
    [Fact]
    public async Task Return_Status200Ok_WithSelectedSection()
    {
        // arrange
        var sectionId = Guid.NewGuid();
        var testSection = new Section() { Id = sectionId, Name = "Section1" };
        var mockSectionService = Substitute.For<ISectionService>();
        mockSectionService.GetByIdAsync(sectionId).Returns(Task.FromResult(testSection));

        var services = new ServiceCollection();
        services.AddTransient<SectionController>();
        services.AddSingleton(mockSectionService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<SectionController>().GetByIdAsync(sectionId);

        // assert
        response.Should().NotBeNull();
        response.Result.Should().BeAssignableTo<OkObjectResult>();

        var result = (OkObjectResult)response.Result;
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<SectionResponseDto>();

        var value = (SectionResponseDto)result.Value;
        value.Should().NotBeNull();
        value.Id.Should().Be(sectionId);
    }

    [Fact]
    public async Task Return_Status404NotFound_WhenGivenNonExistingSectionId()
    {
        // arrange
        var sectionId = Guid.NewGuid();
        Section testSection = null;
        var mockSectionService = Substitute.For<ISectionService>();
        mockSectionService.GetByIdAsync(sectionId).Returns(Task.FromResult(testSection));

        var services = new ServiceCollection();
        services.AddTransient<SectionController>();
        services.AddSingleton(mockSectionService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<SectionController>().GetByIdAsync(sectionId);

        // assert
        response.Should().NotBeNull();
        response.Result.Should().BeOfType<NotFoundResult>();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
