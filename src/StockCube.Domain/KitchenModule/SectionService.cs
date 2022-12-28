namespace StockCube.Domain.KitchenModule;

internal sealed class SectionService : ISectionService
{
    public Task<IEnumerable<Section>> GetListAsync() => throw new NotImplementedException();
    public Task<Section> GetByIdAsync(Guid Id) => throw new NotImplementedException();
}
