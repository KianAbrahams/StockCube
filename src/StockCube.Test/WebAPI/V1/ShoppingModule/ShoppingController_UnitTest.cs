#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Derefence from a possible null reference
#pragma warning disable CS8604 // Possible null reference argument for parameter.
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using StockCube.Domain.ShoppingModule;
using StockCube.WebAPI.WebAPI.V1.ShoppingModule;

namespace StockCube.Test.WebAPI.V1.ShoppingModule;

public sealed class ShoppingController_Get_Should
{
    [Fact]
    public async Task Return_Status200Ok_WithListOfIngredients()
    {
        // arrange
        var testIngredient = new List<Ingredient>()
        {
            new Ingredient { Name = "Ingredient1", Id = Guid.NewGuid() },
            new Ingredient { Name = "Ingredient2", Id = Guid.NewGuid()}
        };
        var mockShoppingService = Substitute.For<IShoppingService>();
        mockShoppingService.GetListAsync().Returns(Task.FromResult(Result<IEnumerable<Ingredient>>.Success(testIngredient.AsEnumerable())));

        var services = new ServiceCollection();
        services.AddTransient<ShoppingController>();
        services.AddSingleton(mockShoppingService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<ShoppingController>().GetListAsync();

        // assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result;
        result.Value.Should().NotBeNull();
        result.Value.Should().BeAssignableTo<IEnumerable<ShoppingResponseDto>>();

        var value = (IEnumerable<ShoppingResponseDto>)result.Value;
        value.Should().NotBeNull();
        value.Should().HaveCount(testIngredient.Count);

        var items = value.ToList();
        items.First().Should().BeOfType<ShoppingResponseDto>();
        for (var i = 0; i < value.Count(); i++)
        {
            items[i].Name.Should().Be(testIngredient[i].Name);
        }
    }
}

public sealed class ShoppingController_DeleteById_Should
{
    [Fact]
    public async Task Return_Status200Ok_WhenGivenAValidId()
    {
        // arrange
        var ingredientId = Guid.NewGuid();
        var mockShoppingService = Substitute.For<IShoppingService>();
        mockShoppingService.DeleteAsync(ingredientId).Returns(Task.FromResult(Result.Success()));

        var services = new ServiceCollection();
        services.AddTransient<ShoppingController>();
        services.AddSingleton(mockShoppingService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<ShoppingController>().DeleteAsync(ingredientId);

        // assert
        response.Should().NotBeNull();
        response.Should().BeAssignableTo<OkResult>();
    }

    [Fact]
    public async Task Return_Status404NotFound_WhenGivenAnUnknownId()
    {
        // arrange
        var ingredientId = Guid.NewGuid();
        var mockShoppingService = Substitute.For<IShoppingService>();
        mockShoppingService.DeleteAsync(ingredientId).Returns(Task.FromResult(Result.NotFound()));

        var services = new ServiceCollection();
        services.AddTransient<ShoppingController>();
        services.AddSingleton(mockShoppingService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<ShoppingController>().DeleteAsync(ingredientId);

        // assert
        response.Should().NotBeNull();
        response.Should().BeAssignableTo<NotFoundResult>();
    }
}

public sealed class ShoppingController_Create_Should
{
    [Fact]
    public async Task Return_Status201Created_WhenGivenAValidShopping()
    {
        // arrange
        var testShoppingRequest = new CreateShoppingRequestDto() { Name = "Ingredient1" };
        var testingIngredient = new Ingredient() { Id = Guid.Empty, Name = testShoppingRequest.Name };

        var mockShoppingService = Substitute.For<IShoppingService>();
        mockShoppingService.AddIngredient(testingIngredient).Returns(Task.FromResult(Result<Ingredient>.Success(testingIngredient)));

        var services = new ServiceCollection();
        services.AddTransient<ShoppingController>();
        services.AddSingleton(mockShoppingService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<ShoppingController>().AddIngredientAsync(testShoppingRequest);

        // assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<CreatedAtRouteResult>();

        var result = (CreatedAtRouteResult)response.Result!;
        result.Value.Should().NotBeNull();
        result.Value.Should().BeAssignableTo<ShoppingResponseDto>();

        var value = (ShoppingResponseDto)result.Value!;
        value.Id.Should().Be(testingIngredient.Id);
        value.Name.Should().Be(testingIngredient.Name);
    }

    [Fact]
    public async Task Return_Status400BadRequest_WhenGivenAnInvalidShoppingRequest()
    {
        // arrange
        var testShoppingRequest = new CreateShoppingRequestDto() { Name = "" };
        var testingIngredient = new Ingredient() { Id = Guid.Empty, Name = testShoppingRequest.Name };
        var validationErrors = new List<ValidationError>()
        {
            new ValidationError() { Identifier = "Name", ErrorMessage = "Should not be empty" }
        };

        var mockShoppingService = Substitute.For<IShoppingService>();
        mockShoppingService.AddIngredient(testingIngredient).Returns(Task.FromResult(Result<Ingredient>.Invalid(validationErrors)));

        var services = new ServiceCollection();
        services.AddTransient<ShoppingController>();
        services.AddSingleton(mockShoppingService);

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<ShoppingController>().AddIngredientAsync(testShoppingRequest);

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
