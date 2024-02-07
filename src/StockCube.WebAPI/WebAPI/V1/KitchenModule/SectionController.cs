using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using StockCube.Domain.KitchenModule;

namespace StockCube.WebAPI.WebAPI.V1.KitchenModule;

[Route("api/[controller]")]
[ApiController]
public sealed class SectionController : ControllerBase, ISectionController
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

    [HttpGet("{SectionId}", Name = "GetById")]
    public async Task<ActionResult<SectionResponseDto>> GetByIdAsync(Guid SectionId)
    {
        var result = await _sectionService.GetByIdAsync(SectionId);

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((error) => ModelState.AddModelError(error.Identifier, error.ErrorMessage));
            return BadRequest(ModelState);
        }
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

    [HttpDelete("{SectionId}")]
    public async Task<ActionResult> DeleteAsync(Guid SectionId)
    {
        var result = await _sectionService.DeleteAsync(SectionId);

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((error) => ModelState.AddModelError(error.Identifier, error.ErrorMessage));
            return BadRequest(ModelState);
        }
        if (result.Status == ResultStatus.NotFound)
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<SectionResponseDto>> CreateSectionAsync(CreateSectionRequestDto request)
    {
        var section = new Section()
        {
            Name = request.Name
        };

        var result = await _sectionService.CreateSection(section);

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((error) => ModelState.AddModelError(error.Identifier, error.ErrorMessage));
            return UnprocessableEntity(ModelState);
        }

        var response = new SectionResponseDto()
        {
            Name = result.Value.Name,
            Id = result.Value.Id
        };
        return CreatedAtRoute("GetById", new { SectionId = response.Id.ToString() }, response);
    }
}
