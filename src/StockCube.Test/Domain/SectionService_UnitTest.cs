#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using StockCube.Domain.KitchenModule;
using StockCube.Infrastructure.KitchenModule;

namespace StockCube.Test.Domain;

public sealed class SectionService_GetListAsync_Should
{
    [Fact]
    public async Task Return_SuccessResult_WithListOfSections()
    {
        // arrange
        var testSection = new List<Section>()
        {
            new Section { Name = "Section1", Id = Guid.NewGuid() },
            new Section { Name = "Section2", Id = Guid.NewGuid()}
        };
        var mockRepository = Substitute.For<IRepository>();
        mockRepository.GetSectionListAsync().Returns(Task.FromResult(testSection.AsEnumerable()));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<ISectionService>().GetListAsync();

        // assert
        response.Status.Should().Be(ResultStatus.Ok);

        response.Value.Should().NotBeNull();
        response.Value.Should().HaveCount(testSection.Count);

        var items = response.Value.ToList();
        items.First().Should().BeOfType<Section>();
        for (var i = 0; i < response.Value.Count(); i++)
        {
            items[i].Name.Should().Be(testSection[i].Name);
        }
    }
}

public sealed class SectionController_GetById_Should
{
    [Fact]
    public async Task Return_SuccessResult_WithSelectedSection()
    {
        // arrange
        var sectionId = Guid.NewGuid();
        var testSection = new Section() { Id = sectionId, Name = "Section1" };
        var mockRepository = Substitute.For<IRepository>();
        mockRepository.GetSectionByIdAsync(sectionId).Returns(Task.FromResult(testSection));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<ISectionService>().GetByIdAsync(sectionId);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.Ok);

        response.Value.Should().NotBeNull();
        response.Value.Id.Should().Be(sectionId);
    }

    [Fact]
    public async Task Return_NotFoundResult_WhenGivenNonExistingSectionId()
    {
        // arrange
        var sectionId = Guid.NewGuid();
        var mockRepository = Substitute.For<IRepository>();
        mockRepository.GetSectionByIdAsync(sectionId).Returns(Task.FromResult<Section?>(null!));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<ISectionService>().GetByIdAsync(sectionId);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.NotFound);

        response.Value.Should().BeNull();  
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
