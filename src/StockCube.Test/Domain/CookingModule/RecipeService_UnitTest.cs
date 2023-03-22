#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using StockCube.Domain.CookingModule;
using StockCube.Infrastructure.CookingModule;

namespace StockCube.Test.Domain.CookingModule;

public sealed class RecipeService_GetListAsync_Should
{
    [Fact]
    public async Task Return_SuccessResult_WithListOfRecipes()
    {
        // arrange
        var testRecipe = new List<Recipe>()
        {
            new Recipe { Name = "Recipe1", Id = Guid.NewGuid() },
            new Recipe { Name = "Recipe2", Id = Guid.NewGuid()}
        };

        var mockRepository = Substitute.For<IRecipeRepository>();
        var mockValidator = Substitute.For<IRecipeValidator>();

        mockRepository.GetRecipeListAsync().Returns(Task.FromResult(testRecipe.AsEnumerable()));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IRecipeService>().GetListAsync();

        // assert
        response.Status.Should().Be(ResultStatus.Ok);

        response.Value.Should().NotBeNull();
        response.Value.Should().HaveCount(testRecipe.Count);

        var items = response.Value.ToList();
        items.First().Should().BeOfType<Recipe>();
        for (var i = 0; i < response.Value.Count(); i++)
        {
            items[i].Name.Should().Be(testRecipe[i].Name);
        }
    }
}

public sealed class RecipeController_GetById_Should
{
    [Fact]
    public async Task Return_SuccessResult_WithSelectedRecipe()
    {
        // arrange
        var recipeId = Guid.NewGuid();
        var testRecipe = new Recipe() { Id = recipeId, Name = "Recipe1" };

        var mockRepository = Substitute.For<IRecipeRepository>();
        var mockValidator = Substitute.For<IRecipeValidator>();

        mockRepository.GetRecipeByIdAsync(recipeId).Returns(Task.FromResult(testRecipe));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IRecipeService>().GetByIdAsync(recipeId);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.Ok);

        response.Value.Should().NotBeNull();
        response.Value.Id.Should().Be(recipeId);
    }

    [Fact]
    public async Task Return_NotFoundResult_WhenGivenNonExistingRecipeId()
    {
        // arrange
        var recipeId = Guid.NewGuid();

        var mockRepository = Substitute.For<IRecipeRepository>();
        var mockValidator = Substitute.For<IRecipeValidator>();

        mockRepository.GetRecipeByIdAsync(recipeId).Returns(Task.FromResult<Recipe>(null!));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IRecipeService>().GetByIdAsync(recipeId);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.NotFound);

        response.Value.Should().BeNull();
    }
}

public sealed class RecipeController_DeleteById_Should
{
    [Fact]
    public async Task Return_SuccessResult_WhenDeletedUsingValidId()
    {
        // arrange
        var recipeId = Guid.NewGuid();

        var mockRepository = Substitute.For<IRecipeRepository>();
        var mockValidator = Substitute.For<IRecipeValidator>();


        mockRepository.DeleteRecipeByIdAsync(recipeId).Returns(Task.FromResult(true));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        //act
        var response = await services.BuildServiceProvider().GetRequiredService<IRecipeService>().DeleteAsync(recipeId);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.Ok);

        response.Value.Should().BeNull();
    }

    [Fact]
    public async Task Return_NotFoundResult_WhenDeletedUsingUnknownId()
    {
        // arrange
        var recipeId = Guid.NewGuid();

        var mockRepository = Substitute.For<IRecipeRepository>();
        var mockValidator = Substitute.For<IRecipeValidator>();

        mockRepository.DeleteRecipeByIdAsync(recipeId).Returns(Task.FromResult(false));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        //act
        var response = await services.BuildServiceProvider().GetRequiredService<IRecipeService>().DeleteAsync(recipeId);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.NotFound);

        response.Value.Should().BeNull();
    }
}

public sealed class RecipeController_CreateRecipe_Should
{
    [Fact]
    public async Task Return_SuccessResult_WhenRecipeIsCreated()
    {
        // arrange
        var newRecipe = new Recipe() { Id = Guid.NewGuid(), Name = "TestRecipe" };

        var mockRepository = Substitute.For<IRecipeRepository>();
        var mockValidator = Substitute.For<IRecipeValidator>();

        mockValidator.ValidateAsync(newRecipe).Returns(Task.FromResult(new ValidationResult()));
        mockRepository.CreateRecipe(newRecipe).Returns(Task.FromResult(newRecipe));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IRecipeService>().CreateRecipeAsync(newRecipe);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.Ok);

        response.Value.Should().NotBeNull();
        response.Value.Name.Should().Be(newRecipe.Name);
        response.Value.Id.Should().Be(newRecipe.Id);
    }

    [Fact]
    public async Task Return_Error_WhenRecipeFailsToCreate()
    {
        // arrange
        var testRecipe = new Recipe() { Id = Guid.NewGuid(), Name = "Recipe1" };

        var mockRepository = Substitute.For<IRecipeRepository>();
        var mockValidator = Substitute.For<IRecipeValidator>();

        mockRepository.CreateRecipe(testRecipe).Returns(Task.FromResult<Recipe>(null!));
        mockValidator.ValidateAsync(testRecipe).Returns(Task.FromResult(new ValidationResult()));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IRecipeService>().CreateRecipeAsync(testRecipe);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.Error);

        response.Value.Should().BeNull();
    }

    [Fact]
    public async Task Return_Invalid_WhenRecipeIsInvalid()
    {
        // arrange
        var testRecipe = new Recipe() { Id = Guid.NewGuid(), Name = "" };

        var validationResult = new ValidationResult()
        {
            Errors = new List<ValidationFailure>()
            {
                new ValidationFailure() { PropertyName = "Name", ErrorMessage = "Should not be empty" }
            }
        };

        var mockRepository = Substitute.For<IRecipeRepository>();
        var mockValidator = Substitute.For<IRecipeValidator>();

        mockValidator.ValidateAsync(testRecipe).Returns(Task.FromResult(validationResult));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IRecipeService>().CreateRecipeAsync(testRecipe);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.Invalid);

        response.ValidationErrors.Should().NotBeEmpty();

        await mockRepository.DidNotReceive().CreateRecipe(Arg.Any<Recipe>());
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
