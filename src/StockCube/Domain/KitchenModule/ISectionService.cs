namespace StockCube.Domain.KitchenModule;

public interface ISectionService 
{
    Task<Result<IEnumerable<Section>>> GetListAsync();
    Task<Result<Section?>> GetByIdAsync(Guid Id);
    Task<Result> DeleteSectionByIdAsync(Guid sectionId);
    Task<Result<Section>> CreateSection();
}
