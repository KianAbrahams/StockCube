#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using StockCube.Domain.KitchenModule;
using StockCube.Infrastructure.KitchenModule;

namespace StockCube.Test.Domain.KitchenModule;

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

        var mockRepository = Substitute.For<IKitchenRepository>();
        var mockValidator = Substitute.For<ISectionValidator>();

        mockRepository.GetSectionListAsync().Returns(Task.FromResult(testSection.AsEnumerable()));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

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

        var mockRepository = Substitute.For<IKitchenRepository>();
        var mockValidator = Substitute.For<ISectionValidator>();

        mockRepository.GetSectionByIdAsync(sectionId).Returns(Task.FromResult(testSection));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

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

        var mockRepository = Substitute.For<IKitchenRepository>();
        var mockValidator = Substitute.For<ISectionValidator>();

        mockRepository.GetSectionByIdAsync(sectionId).Returns(Task.FromResult<Section?>(null!));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<ISectionService>().GetByIdAsync(sectionId);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.NotFound);

        response.Value.Should().BeNull();
    }
}

public sealed class SectionController_DeleteById_Should
{
    [Fact]
    public async Task Return_SuccessResult_WhenDeletedUsingValidId()
    {
        // arrange
        var sectionId = Guid.NewGuid();

        var mockRepository = Substitute.For<IKitchenRepository>();
        var mockValidator = Substitute.For<ISectionValidator>();


        mockRepository.DeleteSectionByIdAsync(sectionId).Returns(Task.FromResult(true));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        //act
        var response = await services.BuildServiceProvider().GetRequiredService<ISectionService>().DeleteAsync(sectionId);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.Ok);

        response.Value.Should().BeNull();
    }

    [Fact]
    public async Task Return_NotFoundResult_WhenDeletedUsingUnknownId()
    {
        // arrange
        var sectionId = Guid.NewGuid();

        var mockRepository = Substitute.For<IKitchenRepository>();
        var mockValidator = Substitute.For<ISectionValidator>();

        mockRepository.DeleteSectionByIdAsync(sectionId).Returns(Task.FromResult(false));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        //act
        var response = await services.BuildServiceProvider().GetRequiredService<ISectionService>().DeleteAsync(sectionId);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.NotFound);

        response.Value.Should().BeNull();
    }
}

public sealed class SectionController_CreateSection_Should
{
    [Fact]
    public async Task Return_SuccessResult_WhenSectionIsCreated()
    {
        // arrange
        var newSection = new Section() { Id = Guid.NewGuid(), Name = "TestSection" };

        var mockRepository = Substitute.For<IKitchenRepository>();
        var mockValidator = Substitute.For<ISectionValidator>();

        mockValidator.ValidateAsync(newSection).Returns(Task.FromResult(new ValidationResult()));
        mockRepository.CreateSection(newSection).Returns(Task.FromResult(newSection));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<ISectionService>().CreateSection(newSection);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.Ok);

        response.Value.Should().NotBeNull();
        response.Value.Name.Should().Be(newSection.Name);
        response.Value.Id.Should().Be(newSection.Id);
    }

    [Fact]
    public async Task Return_Error_WhenSectionFailsToCreate()
    {
        // arrange
        var testSection = new Section() { Id = Guid.NewGuid(), Name = "Section1" };

        var mockRepository = Substitute.For<IKitchenRepository>();
        var mockValidator = Substitute.For<ISectionValidator>();

        mockRepository.CreateSection(testSection).Returns(Task.FromResult<Section>(null!));
        mockValidator.ValidateAsync(testSection).Returns(Task.FromResult(new ValidationResult()));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<ISectionService>().CreateSection(testSection);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.Error);

        response.Value.Should().BeNull();
    }

    [Fact]
    public async Task Return_Invalid_WhenSectionIsInvalid()
    {
        // arrange
        var testSection = new Section() { Id = Guid.NewGuid(), Name = "" };

        var validationResult = new ValidationResult()
        {
            Errors = new List<ValidationFailure>()
            {
                new ValidationFailure() { PropertyName = "Name", ErrorMessage = "Should not be empty" }
            }
        };

        var mockRepository = Substitute.For<IKitchenRepository>();
        var mockValidator = Substitute.For<ISectionValidator>();

        mockValidator.ValidateAsync(testSection).Returns(Task.FromResult(validationResult));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<ISectionService>().CreateSection(testSection);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.Invalid);

        response.ValidationErrors.Should().NotBeEmpty();

        await mockRepository.DidNotReceive().CreateSection(Arg.Any<Section>());
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
