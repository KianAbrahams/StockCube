namespace StockCube.Domain.KitchenModule;

internal sealed class SectionService : ISectionService
{
    public Task<Result<IEnumerable<Section>>> GetListAsync() => throw new NotImplementedException();
    public Task<Result<Section>> GetByIdAsync(Guid Id) => throw new NotImplementedException();
}
