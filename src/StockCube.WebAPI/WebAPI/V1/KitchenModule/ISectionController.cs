using Microsoft.AspNetCore.Mvc;

namespace StockCube.WebAPI.WebAPI.V1.KitchenModule
{
    public interface ISectionController
    {
        public Task<ActionResult<IEnumerable<SectionResponseDto>>> GetListAsync();

        public Task<ActionResult<SectionResponseDto>> CreateSectionAsync(CreateSectionRequestDto request);
    }
}
