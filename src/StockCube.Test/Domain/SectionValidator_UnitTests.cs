#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
using Microsoft.Extensions.DependencyInjection;
using StockCube.Domain.KitchenModule;

namespace StockCube.Test.Domain;

public sealed class SectionValidator_ValidateAsync_Should
{
    [Fact]
    public async Task Returns_Valid_WhenGivenAValidSection()
    {
        // arrange
        var testSection = new Section() { Id = Guid.Empty, Name = "testSection" };

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<ISectionValidator>().ValidateAsync(testSection);

        // assert
        response.Should().NotBeNull();
        response.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Returns_InValidErrorMessages_WhenGivenANullSectionName()
    {
        // arrange
        var testSection = new Section() { Id = Guid.Empty, Name = null! };

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<ISectionValidator>().ValidateAsync(testSection);

        // assert
        response.Should().NotBeNull();
        response.IsValid.Should().BeFalse();
        response.Errors.Should().HaveCount(1);
        response.Errors[0].ErrorMessage.Should().Be($"'{nameof(Section.Name)}' must not be empty.");
    }

    [Fact]
    public async Task Returns_InValidErrorMessages_WhenGivenANameLongerThanSectionNameMaxLength()
    {
        // arrange
        var sectionName = new string('a', Section.NameMaxLength + 1);
        var testSection = new Section() { Id = Guid.NewGuid(), Name = sectionName };

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<ISectionValidator>().ValidateAsync(testSection);

        // assert
        response.Should().NotBeNull();
        response.IsValid.Should().BeFalse();
        response.Errors.Should().HaveCount(1);
        response.Errors[0].ErrorMessage.Should().Be($"The length of '{nameof(Section.Name)}' must be {Section.NameMaxLength} characters or fewer. You entered {sectionName.Length} characters.");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(Section.NameMaxLength)]
    public async Task Returns_Valid_WhenGivenAValidLengthName(int length)
    {
        // arrange
        var sectionName = new string('a', length);
        var testSection = new Section() { Id = Guid.NewGuid(), Name = sectionName };

        var services = new ServiceCollection();
        services.AddStockCubeDomainModel();

        // act
        var response = await services.BuildServiceProvider().GetRequiredService<ISectionValidator>().ValidateAsync(testSection);

        // assert
        response.Should().NotBeNull();
        response.IsValid.Should().BeTrue();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
