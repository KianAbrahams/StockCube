using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using StockCube.Domain.KitchenModule;
using static System.Collections.Specialized.BitVector32;

namespace StockCube.WebAPI.WebAPI.V1.KitchenModule;

[Route("api/[controller]")]
[ApiController]
public sealed class SectionController : ControllerBase
{
    private readonly ISectionService _sectionService;

    public SectionController(ISectionService sectionService)
        => _sectionService = sectionService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SectionResponseDto>>> GetListAsync()
    {
        var result = await _sectionService.GetListAsync();
        var response = new List<SectionResponseDto>();
        foreach (var section in result.Value)
        {
            response.Add(new SectionResponseDto { Name = section.Name, Id = section.Id });
        }
        return Ok(response.AsEnumerable());
    }
    [HttpGet("{Id}")] 
    public async Task<ActionResult<SectionResponseDto>> GetByIdAsync(Guid Id)
    {
        var result = await _sectionService.GetByIdAsync(Id);
        if (result.Status == ResultStatus.NotFound)
        {
            return NotFound();
        }
        var response = new SectionResponseDto()
        {
            Name = result.Value.Name,
            Id = result.Value.Id
        };
        return Ok(response);
    }
}
