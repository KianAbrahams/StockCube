using StockCube.Domain.ShoppingModule;

namespace StockCube.Infrastructure.ShoppingRepository;

public interface IShoppingRepository
{
    Task<IEnumerable<Ingredient>> GetShoppingListAsync();
    Task<bool> DeleteIngredientByIdAsync(Guid sectionId);
    Task<Ingredient> AddIngredient(Ingredient ingredient);
}
