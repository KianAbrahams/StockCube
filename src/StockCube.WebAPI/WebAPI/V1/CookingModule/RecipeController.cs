using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using StockCube.Domain.CookingModule;
using StockCube.Domain.KitchenModule;
using StockCube.WebAPI.WebAPI.V1.KitchenModule;

namespace StockCube.WebAPI.WebAPI.V1.RecipeModule;

[Route("api/[controller]")]
[ApiController]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    public RecipeController(IRecipeService recipeService)
        => _recipeService = recipeService;

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
            Id = result.Value.Id
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
        var recipe = new Recipe()
        {
            Name = request.Name
        };

        var result = await _recipeService.CreateRecipe(recipe);

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((error) => ModelState.AddModelError(error.Identifier, error.ErrorMessage));
            return UnprocessableEntity(ModelState);
        }

        var response = new RecipeResponseDto()
        {
            Name = result.Value.Name,
            Id = result.Value.Id
        };
        return CreatedAtRoute("GetById", new { SectionId = response.Id.ToString() }, response);
    }
}
