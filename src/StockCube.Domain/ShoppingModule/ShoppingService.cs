using Ardalis.Result.FluentValidation;
using StockCube.Infrastructure.ShoppingRepository;

namespace StockCube.Domain.ShoppingModule;

internal sealed class ShoppingService : IShoppingService
{
    private readonly IShoppingRepository _repository;
    private readonly IShoppingValidator _validator;

    public ShoppingService(IShoppingRepository repository, IShoppingValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<Ingredient>>> GetListAsync()
    {
        var result = await _repository.GetShoppingListAsync();

        return Result<IEnumerable<Ingredient>>.Success(result);
    }

    public async Task<Result> DeleteAsync(Guid Id)
    {
        if (Id == Guid.Empty)
        {
            // TODO: Convert Id to valid type and add validator to do this ...
            return Result.Invalid(new List<ValidationError>()
            {
                new ValidationError() { Identifier = "Id", ErrorMessage = "Invalid Id"}
            });
        }
        var result = await _repository.DeleteIngredientByIdAsync(Id);
        if (result == false)
        {
            return Result.NotFound();
        }

        return Result.Success();
    }

    public async Task<Result<Ingredient>> AddIngredient(Ingredient ingredient)
    {
        var validationResult = await _validator.ValidateAsync(ingredient);
        if (validationResult.IsValid == false)
        {
            return Result<Ingredient>.Invalid(validationResult.AsErrors());
        }

        var result = await _repository.AddIngredient(ingredient);
        if (result == null)
        {
            return Result<Ingredient>.Error();
        }

        return Result<Ingredient>.Success(result);
    }
}
