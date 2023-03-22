using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockCube.Domain.CookingModule;

namespace StockCube.WebAPI.WebAPI.V1.RecipeModule;

[Route("api/[controller]")]
[ApiController]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    private readonly IMapper _mapper;

    public RecipeController(IRecipeService recipeService, IMapper mapper)
    {
        _recipeService = recipeService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecipeResponseDto>>> GetListAsync()
    {
        var result = await _recipeService.GetListAsync();
        var response = new List<RecipeResponseDto>();
        foreach (var section in result.Value)
        {
            response.Add(new RecipeResponseDto { Name = section.Name, Id = section.Id });
        }
        return Ok(response.AsEnumerable());
    }

    [HttpGet("{RecipeId}", Name = "GetRecipeById")]
    public async Task<ActionResult<RecipeResponseDto>> GetByIdAsync(Guid SectionId)
    {
        var result = await _recipeService.GetByIdAsync(SectionId);

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((error) => ModelState.AddModelError(error.Identifier, error.ErrorMessage));
            return BadRequest(ModelState);
        }
        if (result.Status == ResultStatus.NotFound)
        {
            return NotFound();
        }

        var response = new RecipeResponseDto()
        {
            Name = result.Value.Name,
            Id = result.Value.Id //,
           // Ingredients = result.Value.Ingredients
        };
        return Ok(response);
    }

    [HttpDelete("{SectionId}")]
    public async Task<ActionResult> DeleteAsync(Guid SectionId)
    {
        var result = await _recipeService.DeleteAsync(SectionId);

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
    public async Task<ActionResult<RecipeResponseDto>> CreateRecipeAsync(CreateRecipeRequestDto request)
    {
        var recipe = _mapper.Map<Recipe>(request);

        var result = await _recipeService.CreateRecipeAsync(recipe);

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((error) => ModelState.AddModelError(error.Identifier, error.ErrorMessage));
            return UnprocessableEntity(ModelState);
        }

        var response = _mapper.Map<RecipeResponseDto>(result.Value);
        return CreatedAtRoute("GetById", new { SectionId = response.Id.ToString() }, response);
    }
}
