#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Derefence from a possible null reference
#pragma warning disable CS8604 // Possible null reference argument for parameter.
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
        mockSectionService.GetListAsync().Returns(Task.FromResult(Result<IEnumerable<Section>>.Success(testSection.AsEnumerable())));

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
            items[i].name.Should().Be(testSection[i].Name);
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
        mockSectionService.GetByIdAsync(sectionId).Returns(Task.FromResult(Result<Section?>.Success(testSection)));

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
    public async Task Return_Status404NotFound_WhenGivenUnknownId()
    {
        // arrange
        var sectionId = Guid.NewGuid();
        var mockSectionService = Substitute.For<ISectionService>();
        mockSectionService.GetByIdAsync(sectionId).Returns(Task.FromResult(Result<Section?>.NotFound()));

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

public sealed class SectionController_DeleteById_Should
{
    [Fact]
    public async Task Return_Status200Ok_WhenGivenAValidId()
    {
        // arrange
        var sectionId = Guid.NewGuid();
        var mockSectionService = Substitute.For<ISectionService>();
        mockSectionService.DeleteAsync(sectionId).Returns(Task.FromResult(Result.Success()));

        var services = new ServiceCollection();
        services.AddTransient<SectionController>();
        services.AddSingleton(mockSectionService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<SectionController>().DeleteAsync(sectionId);

        // assert
        response.Should().NotBeNull();
        response.Should().BeAssignableTo<OkResult>();
    }

    [Fact]
    public async Task Return_Status404NotFound_WhenGivenAnUnknownId()
    {
        // arrange
        var sectionId = Guid.NewGuid();
        var mockSectionService = Substitute.For<ISectionService>();
        mockSectionService.DeleteAsync(sectionId).Returns(Task.FromResult(Result.NotFound()));

        var services = new ServiceCollection();
        services.AddTransient<SectionController>();
        services.AddSingleton(mockSectionService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<SectionController>().DeleteAsync(sectionId);

        // assert
        response.Should().NotBeNull();
        response.Should().BeAssignableTo<NotFoundResult>();
    }
}

public sealed class SectionController_Create_Should
{
    [Fact]
    public async Task Return_Status201Created_WhenGivenAValidSection()
    {
        // arrange
        var testSectionRequest = new CreateSectionRequestDto() { Name = "Section1" };
        var testSection = new Section() { Id = Guid.Empty, Name = testSectionRequest.Name };

        var mockSectionService = Substitute.For<ISectionService>();
        mockSectionService.CreateSection(testSection).Returns(Task.FromResult(Result<Section>.Success(testSection)));

        var services = new ServiceCollection();
        services.AddTransient<SectionController>();
        services.AddSingleton(mockSectionService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<SectionController>().CreateSectionAsync(testSectionRequest);

        // assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<CreatedAtRouteResult>();

        var result = (CreatedAtRouteResult)response.Result!;
        result.Value.Should().NotBeNull();
        result.Value.Should().BeAssignableTo<SectionResponseDto>();

        var value = (SectionResponseDto)result.Value!;
        value.Id.Should().Be(testSection.Id);
        value.name.Should().Be(testSection.Name);
    }

    [Fact]
    public async Task Return_Status400BadRequest_WhenGivenAnInvalidSectionRequest()
    {
        // arrange
        var testSectionRequest = new CreateSectionRequestDto() { Name = "" };
        var testSection = new Section() { Id = Guid.Empty, Name = testSectionRequest.Name };
        var validationErrors = new List<ValidationError>()
        {
            new ValidationError() { Identifier = "Name", ErrorMessage = "Should not be empty" }
        };

        var mockSectionService = Substitute.For<ISectionService>();
        mockSectionService.CreateSection(testSection).Returns(Task.FromResult(Result<Section>.Invalid(validationErrors)));

        var services = new ServiceCollection();
        services.AddTransient<SectionController>();
        services.AddSingleton(mockSectionService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<SectionController>().CreateSectionAsync(testSectionRequest);

        // assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<UnprocessableEntityObjectResult>();

        var result = (UnprocessableEntityObjectResult)response.Result!;
        result.Value.Should().NotBeNull();

        var errors = (SerializableError)result.Value!;

        foreach (var expectedErrorDetails in validationErrors)
        {
            var messages = (string[])errors[expectedErrorDetails.Identifier];
            messages.Should().Contain(expectedErrorDetails.ErrorMessage);
        }
    }
}

#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Derefence from a possible null reference
#pragma warning disable CS8604 // Possible null reference argument for parameter.
