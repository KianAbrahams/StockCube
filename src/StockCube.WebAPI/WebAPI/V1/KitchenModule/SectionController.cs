using Microsoft.AspNetCore.Mvc;
using StockCube.Domain.KitchenModule;

namespace StockCube.WebAPI.WebAPI.V1.KitchenModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionController(ISectionService sectionService)
            => _sectionService = sectionService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SectionResponseDto>>> GetListAsync()
        {
            var result = await _sectionService.GetListAsync();
            var response = new List<SectionResponseDto>();
            foreach (var section in result)
            {
                response.Add(new SectionResponseDto { Name = section.Name });
            }
            return Ok(response.AsEnumerable());
        }
    }
}
