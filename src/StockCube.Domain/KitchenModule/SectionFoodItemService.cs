using StockCube.Infrastructure.KitchenModule;

namespace StockCube.Domain.KitchenModule;

internal sealed class SectionFoodItemService : ISectionFoodItemService
{
    private readonly IKitchenRepository _repository;
    private readonly ISectionFoodItemValidator _validator;

    public SectionFoodItemService(IKitchenRepository repository, ISectionFoodItemValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<SectionFoodItem>>> GetListAsync(Guid sectionId)
    {
        var result = await _repository.GetSectionFoodItemListAsync(sectionId);
        if (result == null)
        {
            return Result <IEnumerable<SectionFoodItem>>.Error();
        }

        return Result<IEnumerable<SectionFoodItem>>.Success(result);
    }

    public async Task<Result<SectionFoodItem>> CreateSectionFoodItem(SectionFoodItem sectionFoodItem)
    {
        // TODO: write validation for SectionFoodItem

        var result = await _repository.CreateSectionFoodItem(sectionFoodItem);
        if (result == null)
        {
            return Result<SectionFoodItem>.Error();
        }
            
        return Result<SectionFoodItem>.Success(result);
    }
}
