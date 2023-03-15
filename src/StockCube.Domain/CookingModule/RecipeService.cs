using Ardalis.Result.FluentValidation;
using StockCube.Infrastructure.CookingModule;

namespace StockCube.Domain.CookingModule;

internal sealed class RecipeService : IRecipeService
{
    private readonly IRecipeValidator _validator;
    private readonly IRecipeRepository _repository;

    public RecipeService(IRecipeRepository repository, IRecipeValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task<Result<IEnumerable<Recipe>>> GetListAsync()
    {
        var result = await _repository.GetRecipeListAsync();

        return Result<IEnumerable<Recipe>>.Success(result);
    }

    public async Task<Result<Recipe>> GetByIdAsync(Guid Id)
    {
        if (Id == Guid.Empty)
        {
            // TODO: Convert Id to valid type and add validator to do this ...
            return Result<Recipe>.Invalid(new List<ValidationError>()
            {
                new ValidationError() { Identifier = "Id", ErrorMessage = "Invalid Id"}
            });
        }
        var result = await _repository.GetRecipeByIdAsync(Id);
        if (result == null)
        {
            return Result<Recipe>.NotFound();
        }

        return Result<Recipe>.Success(result);
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
        var result = await _repository.DeleteRecipeByIdAsync(Id);
        if (result == false)
        {
            return Result.NotFound();
        }

        return Result.Success();
    }

    public async Task<Result<Recipe>> CreateRecipe(Recipe recipe)
    {
        var validationResult = await _validator.ValidateAsync(recipe);
        if (validationResult.IsValid == false)
        {
            return Result<Recipe>.Invalid(validationResult.AsErrors());
        }

        var result = await _repository.CreateRecipe(recipe);
        if (result == null)
        {
            return Result<Recipe>.Error();
        }

        return Result<Recipe>.Success(result);
    }
}
