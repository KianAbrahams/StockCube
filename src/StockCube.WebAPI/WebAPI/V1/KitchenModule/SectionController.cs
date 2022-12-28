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
                response.Add(new SectionResponseDto { Name = section.Name, Id = section.Id });
            }
            return Ok(response.AsEnumerable());
        }
        [HttpGet("{Id}")] 
        public async Task<ActionResult<SectionResponseDto>> GetByIdAsync(Guid Id)
        {
            var result = await _sectionService.GetListAsync();
            var response = new Section();
            foreach (var section in result)
            {
                if (section.Id == Id)
                {
                    response = section;
                }
            }
            return Ok(response);
        }
    }
}
