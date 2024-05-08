using StockCube.Domain.KitchenModule;

namespace StockCube.Infrastructure.KitchenModule;

public interface IKitchenRepository
{
    Task<IEnumerable<Section>> GetSectionListAsync();

    Task<Section> GetSectionByIdAsync(Guid Id);

    Task<bool> DeleteSectionByIdAsync(Guid sectionId);
    Task<Section> CreateSection(Section section);

    Task<IEnumerable<SectionFoodItem>> GetSectionFoodItemListAsync(Guid sectionId);
    Task<SectionFoodItem> CreateSectionFoodItem(SectionFoodItem sectionFoodItem);
    Task<SectionFoodItem> UpdateSectionFoodItem(SectionFoodItem sectionFoodItem);
}
