namespace StockCube.Domain.ShoppingModule;

public interface IShoppingService
{
    Task<Result<IEnumerable<Ingredient>>> GetListAsync();
    Task<Result> DeleteAsync(Guid ingredientId);
    Task<Result<Ingredient>> AddIngredient(Ingredient ingredient);
}
