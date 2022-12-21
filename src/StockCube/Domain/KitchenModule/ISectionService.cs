namespace StockCube.Domain.KitchenModule;

public interface ISectionService 
{
    Task<IEnumerable<Section>> GetListAsync();
}
