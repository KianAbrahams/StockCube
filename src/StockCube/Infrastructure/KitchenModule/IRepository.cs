using StockCube.Domain.KitchenModule;

namespace StockCube.Infrastructure.KitchenModule;

public interface IRepository
{
    Task<IEnumerable<Section>> GetSectionListAsync();

    Task<Section> GetSectionByIdAsync(Guid Id);

    Task<bool> DeleteSectionByIdAsync(Guid sectionId);
    Task<Section> CreateSection(Section section);
}
