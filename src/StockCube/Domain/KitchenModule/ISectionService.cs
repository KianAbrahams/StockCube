namespace StockCube.Domain.KitchenModule;

public interface ISectionService 
{
    Task<IEnumerable<Section>> GetListAsync();
    Task<Section> GetByIdAsync(Guid Id);
}
