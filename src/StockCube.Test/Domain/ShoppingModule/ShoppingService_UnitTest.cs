#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Derefence from a possible null reference
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using StockCube.Domain.ShoppingModule;
using StockCube.Infrastructure.ShoppingRepository;

namespace StockCube.Test.Domain.ShoppingModule;

public sealed class ShoppingService_GetListAsync_Should
{
    [Fact]
    public async Task Return_SuccessResult_WithListOfIngredients()
    {
        // arrange
        var testShoppingList = new List<Ingredient>()
        {
            new Ingredient { Name = "Ingrdient1", Id = Guid.NewGuid() },
            new Ingredient { Name = "Ingrdient2", Id = Guid.NewGuid()}
        };

        var mockRepository = Substitute.For<IShoppingRepository>();
        var mockValidator = Substitute.For<IShoppingValidator>();

        mockRepository.GetShoppingListAsync().Returns(Task.FromResult(testShoppingList.AsEnumerable()));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IShoppingService>().GetListAsync();

        // assert
        response.Status.Should().Be(ResultStatus.Ok);

        response.Value.Should().NotBeNull();
        response.Value.Should().HaveCount(testShoppingList.Count);

        var items = response.Value.ToList();
        items.First().Should().BeOfType<Ingredient>();
        for (var i = 0; i < response.Value.Count(); i++)
        {
            items[i].Name.Should().Be(testShoppingList[i].Name);
        }
    }
}

public sealed class ShoppingController_DeleteById_Should
{
    [Fact]
    public async Task Return_SuccessResult_WhenDeletedUsingValidId()
    {
        // arrange
        var ingredientId = Guid.NewGuid();

        var mockRepository = Substitute.For<IShoppingRepository>();
        var mockValidator = Substitute.For<IShoppingValidator>();


        mockRepository.DeleteIngredientByIdAsync(ingredientId).Returns(Task.FromResult(true));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        //act
        var response = await services.BuildServiceProvider().GetRequiredService<IShoppingService>().DeleteAsync(ingredientId);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.Ok);

        response.Value.Should().BeNull();
    }

    [Fact]
    public async Task Return_NotFoundResult_WhenDeletedUsingUnknownId()
    {
        // arrange
        var ingredientId = Guid.NewGuid();

        var mockRepository = Substitute.For<IShoppingRepository>();
        var mockValidator = Substitute.For<IShoppingValidator>();

        mockRepository.DeleteIngredientByIdAsync(ingredientId).Returns(Task.FromResult(false));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        //act
        var response = await services.BuildServiceProvider().GetRequiredService<IShoppingService>().DeleteAsync(ingredientId);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.NotFound);

        response.Value.Should().BeNull();
    }
}

public sealed class ShoppingController_AddIngredient_Should
{
    [Fact]
    public async Task Return_SuccessResult_WhenIngredientIsAdded()
    {
        // arrange
        var newIngredient = new Ingredient() { Id = Guid.NewGuid(), Name = "TestIngredient" };

        var mockRepository = Substitute.For<IShoppingRepository>();
        var mockValidator = Substitute.For<IShoppingValidator>();

        mockValidator.ValidateAsync(newIngredient).Returns(Task.FromResult(new ValidationResult()));
        mockRepository.AddIngredient(newIngredient).Returns(Task.FromResult(newIngredient));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IShoppingService>().AddIngredient(newIngredient);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.Ok);

        response.Value.Should().NotBeNull();
        response.Value.Name.Should().Be(newIngredient.Name);
        response.Value.Id.Should().Be(newIngredient.Id);
    }

    [Fact]
    public async Task Return_Error_WhenIngredientFailsToBeAdded()
    {
        // arrange
        var testIngredient = new Ingredient() { Id = Guid.NewGuid(), Name = "Ingredient1" };

        var mockRepository = Substitute.For<IShoppingRepository>();
        var mockValidator = Substitute.For<IShoppingValidator>();

        mockRepository.AddIngredient(testIngredient).Returns(Task.FromResult<Ingredient>(null!));
        mockValidator.ValidateAsync(testIngredient).Returns(Task.FromResult(new ValidationResult()));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IShoppingService>().AddIngredient(testIngredient);
            
        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.Error);

        response.Value.Should().BeNull();
    }

    [Fact]
    public async Task Return_Invalid_WhenIngredientIsInvalid()
    {
        // arrange
        var testIngredient = new Ingredient() { Id = Guid.NewGuid(), Name = "" };

        var validationResult = new ValidationResult()
        {
            Errors = new List<ValidationFailure>()
            {
                new ValidationFailure() { PropertyName = "Name", ErrorMessage = "Should not be empty" }
            }
        };

        var mockRepository = Substitute.For<IShoppingRepository>();
        var mockValidator = Substitute.For<IShoppingValidator>();

        mockValidator.ValidateAsync(testIngredient).Returns(Task.FromResult(validationResult));

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();
        services.AddSingleton(mockRepository);
        services.AddSingleton(mockValidator);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IShoppingService>().AddIngredient(testIngredient);

        // assert
        response.Should().NotBeNull();
        response.Status.Should().Be(ResultStatus.Invalid);

        response.ValidationErrors.Should().NotBeEmpty();

        await mockRepository.DidNotReceive().AddIngredient(Arg.Any<Ingredient>());
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Derefence from a possible null reference
