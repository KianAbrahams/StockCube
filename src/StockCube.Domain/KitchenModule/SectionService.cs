using Ardalis.Result.FluentValidation;
using StockCube.Infrastructure.KitchenModule;

namespace StockCube.Domain.KitchenModule;

internal sealed class SectionService : ISectionService
{
    private readonly IRepository _repository;
    private readonly ISectionValidator _validator;

    public SectionService(IRepository repository, ISectionValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<Section>>> GetListAsync()
    {
        var result = await _repository.GetSectionListAsync();

        return Result<IEnumerable<Section>>.Success(result);
    }

    public async Task<Result<Section>> GetByIdAsync(Guid Id)
    {
        var result = await _repository.GetSectionByIdAsync(Id);
        if(result == null)
        {
            return Result<Section>.NotFound();
        }

        return Result<Section>.Success(result);
    }

    public async Task<Result> DeleteAsync(Guid Id)
    {
        var result = await _repository.DeleteSectionByIdAsync(Id);
        if (result == false)
        {
            return Result.NotFound();
        }

        return Result.Success();
    }

    public async Task<Result<Section>> CreateSection(Section section)
    {
        var validationResult = await _validator.ValidateAsync(section);
        if (validationResult.IsValid == false)
        {
            return Result<Section>.Invalid(validationResult.AsErrors());
        }

        var result = await _repository.CreateSection(section);
        if (result == null)
        {
            return Result<Section>.Error();
        }

        return Result<Section>.Success(result);
    }
}
