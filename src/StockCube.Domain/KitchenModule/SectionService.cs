using StockCube.Infrastructure.KitchenModule;

namespace StockCube.Domain.KitchenModule;

internal sealed class SectionService : ISectionService
{
    private readonly IRepository _repository;

    public SectionService(IRepository repository)
        => _repository = repository;

    public async Task<Result<IEnumerable<Section>>> GetListAsync()
    {
        var result = await _repository.GetSectionListAsync();

        return Result<IEnumerable<Section>>.Success(result);
    }

    public async Task<Result<Section>> GetByIdAsync(Guid Id)
    {
        var result = await _repository.GetSectionByIdAsync(Id);
        if(result == null)
        {
            return Result<Section>.NotFound();
        }

        return Result<Section>.Success(result);
    }
}
