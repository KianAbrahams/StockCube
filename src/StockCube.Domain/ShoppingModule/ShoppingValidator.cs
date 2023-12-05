namespace StockCube.Domain.ShoppingModule;

internal sealed class ShoppingValidator : AbstractValidator<Ingredient>, IShoppingValidator
{
    public ShoppingValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(Ingredient.NameMaxLength);
    }
}
