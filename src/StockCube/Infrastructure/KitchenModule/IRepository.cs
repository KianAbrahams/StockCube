using StockCube.Domain.KitchenModule;

namespace StockCube.Infrastructure.KitchenModule;

public interface IRepository
{
    Task<IEnumerable<Section>> GetSectionListAsync();

    Task<Section> GetSectionByIdAsync(Guid Id);
}
