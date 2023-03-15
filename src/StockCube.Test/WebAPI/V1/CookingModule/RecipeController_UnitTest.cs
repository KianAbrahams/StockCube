#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using StockCube.Domain.CookingModule;
using StockCube.WebAPI.WebAPI.V1.RecipeModule;

namespace StockCube.Test.WebAPI.V1.CookingModule;

public sealed class RecipeController_Get_Should
{
    [Fact]
    public async Task Return_Status200Ok_WithListOfRecipes()
    {
        // arrange
        var testRecipe = new List<Recipe>()
        {
            new Recipe { Name = "Recipe1", Id = Guid.NewGuid() },
            new Recipe { Name = "Recipe2", Id = Guid.NewGuid()}
        };
        var mockRecipeService = Substitute.For<IRecipeService>();
        mockRecipeService.GetListAsync().Returns(Task.FromResult(Result<IEnumerable<Recipe>>.Success(testRecipe.AsEnumerable())));

        var services = new ServiceCollection();
        services.AddTransient<RecipeController>();
        services.AddSingleton(mockRecipeService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<RecipeController>().GetListAsync();

        // assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result;
        result.Value.Should().NotBeNull();
        result.Value.Should().BeAssignableTo<IEnumerable<RecipeResponseDto>>();

        var value = (IEnumerable<RecipeResponseDto>)result.Value;
        value.Should().NotBeNull();
        value.Should().HaveCount(testRecipe.Count);

        var items = value.ToList();
        items.First().Should().BeOfType<RecipeResponseDto>();
        for (var i = 0; i < value.Count(); i++)
        {
            items[i].Name.Should().Be(testRecipe[i].Name);
        }
    }
}

public sealed class RecipeController_GetById_Should
{
    [Fact]
    public async Task Return_Status200Ok_WithSelectedRecipe()
    {
        // arrange
        var recipeId = Guid.NewGuid();
        var testRecipe = new Recipe() { Id = recipeId, Name = "Recipe1" };
        var mockRecipeService = Substitute.For<IRecipeService>();
        mockRecipeService.GetByIdAsync(recipeId).Returns(Task.FromResult(Result<Recipe>.Success(testRecipe)));

        var services = new ServiceCollection();
        services.AddTransient<RecipeController>();
        services.AddSingleton(mockRecipeService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<RecipeController>().GetByIdAsync(recipeId);

        // assert
        response.Should().NotBeNull();
        response.Result.Should().BeAssignableTo<OkObjectResult>();

        var result = (OkObjectResult)response.Result;
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<RecipeResponseDto>();

        var value = (RecipeResponseDto)result.Value;
        value.Should().NotBeNull();
        value.Id.Should().Be(recipeId);
    }

    [Fact]
    public async Task Return_Status404NotFound_WhenGivenUnknownId()
    {
        // arrange
        var recipeId = Guid.NewGuid();
        var mockRecipeService = Substitute.For<IRecipeService>();
        mockRecipeService.GetByIdAsync(recipeId).Returns(Task.FromResult(Result<Recipe>.NotFound()));

        var services = new ServiceCollection();
        services.AddTransient<RecipeController>();
        services.AddSingleton(mockRecipeService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<RecipeController>().GetByIdAsync(recipeId);

        // assert
        response.Should().NotBeNull();
        response.Result.Should().BeOfType<NotFoundResult>();
    }
}

public sealed class RecipeController_DeleteById_Should
{
    [Fact]
    public async Task Return_Status200Ok_WhenGivenAValidId()
    {
        // arrange
        var recipeId = Guid.NewGuid();
        var mockRecipeService = Substitute.For<IRecipeService>();
        mockRecipeService.DeleteAsync(recipeId).Returns(Task.FromResult(Result.Success()));

        var services = new ServiceCollection();
        services.AddTransient<RecipeController>();
        services.AddSingleton(mockRecipeService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<RecipeController>().DeleteAsync(recipeId);

        // assert
        response.Should().NotBeNull();
        response.Should().BeAssignableTo<OkResult>();
    }

    [Fact]
    public async Task Return_Status404NotFound_WhenGivenAnUnknownId()
    {
        // arrange
        var recipeId = Guid.NewGuid();
        var mockRecipeService = Substitute.For<IRecipeService>();
        mockRecipeService.DeleteAsync(recipeId).Returns(Task.FromResult(Result.NotFound()));

        var services = new ServiceCollection();
        services.AddTransient<RecipeController>();
        services.AddSingleton(mockRecipeService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<RecipeController>().DeleteAsync(recipeId);

        // assert
        response.Should().NotBeNull();
        response.Should().BeAssignableTo<NotFoundResult>();
    }
}

public sealed class RecipeController_Create_Should
{
    [Fact]
    public async Task Return_Status201Created_WhenGivenAValidRecipe()
    {
        // arrange
        var testRecipeRequest = new CreateRecipeRequestDto() { Name = "Recipe1" };
        var testRecipe = new Recipe() { Id = Guid.Empty, Name = testRecipeRequest.Name };

        var mockRecipeService = Substitute.For<IRecipeService>();
        mockRecipeService.CreateRecipe(testRecipe).Returns(Task.FromResult(Result<Recipe>.Success(testRecipe)));

        var services = new ServiceCollection();
        services.AddTransient<RecipeController>();
        services.AddSingleton(mockRecipeService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<RecipeController>().CreateRecipeAsync(testRecipeRequest);

        // assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<CreatedAtRouteResult>();

        var result = (CreatedAtRouteResult)response.Result!;
        result.Value.Should().NotBeNull();
        result.Value.Should().BeAssignableTo<RecipeResponseDto>();

        var value = (RecipeResponseDto)result.Value!;
        value.Id.Should().Be(testRecipe.Id);
        value.Name.Should().Be(testRecipe.Name);
    }

    [Fact]
    public async Task Return_Status400BadRequest_WhenGivenAnInvalidRecipeRequest()
    {
        // arrange
        var testRecipeRequest = new CreateRecipeRequestDto() { Name = "" };
        var testRecipe = new Recipe() { Id = Guid.Empty, Name = testRecipeRequest.Name };
        var validationErrors = new List<ValidationError>()
        {
            new ValidationError() { Identifier = "Name", ErrorMessage = "Should not be empty" }
        };

        var mockRecipeService = Substitute.For<IRecipeService>();
        mockRecipeService.CreateRecipe(testRecipe).Returns(Task.FromResult(Result<Recipe>.Invalid(validationErrors)));

        var services = new ServiceCollection();
        services.AddTransient<RecipeController>();
        services.AddSingleton(mockRecipeService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<RecipeController>().CreateRecipeAsync(testRecipeRequest);

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
