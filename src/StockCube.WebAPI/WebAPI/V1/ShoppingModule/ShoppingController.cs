using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using StockCube.Domain.ShoppingModule;

namespace StockCube.WebAPI.WebAPI.V1.ShoppingModule;

[Route("api/[controller]")]
[ApiController]
public sealed class ShoppingController : ControllerBase
{
    private readonly IShoppingService _shoppingService;

    public ShoppingController(IShoppingService shoppingService)
        => _shoppingService = shoppingService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShoppingResponseDto>>> GetListAsync()
    {
        var result = await _shoppingService.GetListAsync();
        var response = new List<ShoppingResponseDto>();
        foreach (var section in result.Value)
        {
            response.Add(new ShoppingResponseDto { Name = section.Name, Id = section.Id });
        }
        return Ok(response.AsEnumerable());
    }

    [HttpDelete("{IngredientId}")]
    public async Task<ActionResult> DeleteAsync(Guid SectionId)
    {
        var result = await _shoppingService.DeleteAsync(SectionId);

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
    public async Task<ActionResult<ShoppingResponseDto>> AddIngredientAsync(CreateShoppingRequestDto request)
    {
        var section = new Ingredient()
        {
            Name = request.Name
        };

        var result = await _shoppingService.AddIngredient(section);

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach((error) => ModelState.AddModelError(error.Identifier, error.ErrorMessage));
            return UnprocessableEntity(ModelState);
        }

        var response = new ShoppingResponseDto()
        {
            Name = result.Value.Name,
            Id = result.Value.Id
        };
        return CreatedAtRoute("GetById", new { SectionId = response.Id.ToString() }, response);
    }
}
