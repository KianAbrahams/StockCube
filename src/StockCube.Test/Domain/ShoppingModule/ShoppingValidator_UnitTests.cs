#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
using Microsoft.Extensions.DependencyInjection;
using StockCube.Domain.ShoppingModule;

namespace StockCube.Test.Domain.ShoppingModule;

public sealed class ShoppingValidator_ValidateAsync_Should
{
    [Fact]
    public async Task Returns_Valid_WhenGivenAValidIngredient()
    {
        // arrange
        var testIngredient = new Ingredient() { Id = Guid.Empty, Name = "testIngredient" };

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IShoppingValidator>().ValidateAsync(testIngredient);

        // assert
        response.Should().NotBeNull();
        response.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Returns_InValidErrorMessages_WhenGivenANullIngredientName()
    {
        // arrange
        var testIngredient = new Ingredient() { Id = Guid.Empty, Name = null! };

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IShoppingValidator>().ValidateAsync(testIngredient);

        // assert
        response.Should().NotBeNull();
        response.IsValid.Should().BeFalse();
        response.Errors.Should().HaveCount(1);
        response.Errors[0].ErrorMessage.Should().Be($"'{nameof(Ingredient.Name)}' must not be empty.");
    }

    [Fact]
    public async Task Returns_InValidErrorMessages_WhenGivenANameLongerThanIngredientNameMaxLength()
    {
        // arrange
        var ingredientName = new string('a', Ingredient.NameMaxLength + 1);
        var testIngredient = new Ingredient() { Id = Guid.NewGuid(), Name = ingredientName };

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IShoppingValidator>().ValidateAsync(testIngredient);

        // assert
        response.Should().NotBeNull();
        response.IsValid.Should().BeFalse();
        response.Errors.Should().HaveCount(1);
        response.Errors[0].ErrorMessage.Should().Be($"The length of '{nameof(Ingredient.Name)}' must be {Ingredient.NameMaxLength} characters or fewer. You entered {ingredientName.Length} characters.");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(Ingredient.NameMaxLength)]
    public async Task Returns_Valid_WhenGivenAValidLengthName(int length)
    {
        // arrange
        var ingredientName = new string('a', length);
        var testIngredient = new Ingredient() { Id = Guid.NewGuid(), Name = ingredientName };

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IShoppingValidator>().ValidateAsync(testIngredient);

        // assert
        response.Should().NotBeNull();
        response.IsValid.Should().BeTrue();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
