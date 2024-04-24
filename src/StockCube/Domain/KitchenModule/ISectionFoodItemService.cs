namespace StockCube.Domain.KitchenModule;

public interface ISectionFoodItemService
{
    Task<Result<IEnumerable<SectionFoodItem>>> GetListAsync(Guid sectionId);
    Task<Result<SectionFoodItem>> CreateSectionFoodItem(SectionFoodItem sectionFoodItem);
}
