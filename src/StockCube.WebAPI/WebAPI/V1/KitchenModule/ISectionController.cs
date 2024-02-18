using Microsoft.AspNetCore.Mvc;

namespace StockCube.WebAPI.WebAPI.V1.KitchenModule
{
    public interface ISectionController
    {
        public Task<ActionResult<IList<SectionResponseDto>>> GetListAsync();

        public Task<ActionResult<SectionResponseDto>> CreateSectionAsync(CreateSectionRequestDto request);
    }
}
