namespace StockCube.Domain.KitchenModule
{
    public interface ISectionValidator : IValidator<Section> { }
}

namespace StockCube.Domain.CookingModule
{
    public interface IRecipeValidator : IValidator<Recipe> { }
}

namespace StockCube.Domain.ShoppingModule
{
    public interface IShoppingValidator : IValidator<Ingredient> { }
}
