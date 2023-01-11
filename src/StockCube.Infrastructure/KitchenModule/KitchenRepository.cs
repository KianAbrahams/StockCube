using System.Reflection.Metadata.Ecma335;
using StockCube.Domain.KitchenModule;

namespace StockCube.Infrastructure.KitchenModule;

internal class KitchenRepository : IKitchenRepository
{
    public Task<Section> CreateSection(Section section)
        => Task.FromResult(new Section() {  Name = section.Name, Id = Guid.NewGuid() });

    public Task<bool> DeleteSectionByIdAsync(Guid sectionId)
        => throw new NotImplementedException();

    public Task<Section> GetSectionByIdAsync(Guid Id)
        => throw new NotImplementedException();

    public Task<IEnumerable<Section>> GetSectionListAsync()
    {
        var sectionList = new List<Section>()
        {
            new Section() { Id = Guid.NewGuid(), Name = "Cupboard under the sink" },
            new Section() { Id = Guid.NewGuid(), Name = "Cupboard by the fridge"},
            new Section() { Id = Guid.NewGuid(), Name = "Fridge"},
            new Section() { Id = Guid.NewGuid(), Name = "Freezer"}
        };
        return Task.FromResult(sectionList.AsEnumerable());
    }
}
