namespace StockCube.Domain.CookingModule;

internal sealed class RecipeValidator : AbstractValidator<Recipe>, IRecipeValidator
{
    public RecipeValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(Recipe.NameMaxLength);
    }
}
