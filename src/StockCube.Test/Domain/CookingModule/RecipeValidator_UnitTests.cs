#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
using Microsoft.Extensions.DependencyInjection;
using StockCube.Domain.CookingModule;

namespace StockCube.Test.Domain.RecipeModule;

public sealed class RecipeValidator_ValidateAsync_Should
{
    [Fact]
    public async Task Returns_Valid_WhenGivenAValidRecipe()
    {
        // arrange
        var testRecipe = new Recipe() { Id = Guid.Empty, Name = "testRecipe" };

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IRecipeValidator>().ValidateAsync(testRecipe);

        // assert
        response.Should().NotBeNull();
        response.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Returns_InValidErrorMessages_WhenGivenANullRecipeName()
    {
        // arrange
        var testRecipe = new Recipe() { Id = Guid.Empty, Name = null! };

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IRecipeValidator>().ValidateAsync(testRecipe);

        // assert
        response.Should().NotBeNull();
        response.IsValid.Should().BeFalse();
        response.Errors.Should().HaveCount(1);
        response.Errors[0].ErrorMessage.Should().Be($"'{nameof(Recipe.Name)}' must not be empty.");
    }

    [Fact]
    public async Task Returns_InValidErrorMessages_WhenGivenANameLongerThanRecipeNameMaxLength()
    {
        // arrange
        var recipeName = new string('a', Recipe.NameMaxLength + 1);
        var testRecipe = new Recipe() { Id = Guid.NewGuid(), Name = recipeName };

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IRecipeValidator>().ValidateAsync(testRecipe);

        // assert
        response.Should().NotBeNull();
        response.IsValid.Should().BeFalse();
        response.Errors.Should().HaveCount(1);
        response.Errors[0].ErrorMessage.Should().Be($"The length of '{nameof(Recipe.Name)}' must be {Recipe.NameMaxLength} characters or fewer. You entered {recipeName.Length} characters.");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(Recipe.NameMaxLength)]
    public async Task Returns_Valid_WhenGivenAValidLengthName(int length)
    {
        // arrange
        var recipeName = new string('a', length);
        var testRecipe = new Recipe() { Id = Guid.NewGuid(), Name = recipeName };

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<IRecipeValidator>().ValidateAsync(testRecipe);

        // assert
        response.Should().NotBeNull();
        response.IsValid.Should().BeTrue();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
