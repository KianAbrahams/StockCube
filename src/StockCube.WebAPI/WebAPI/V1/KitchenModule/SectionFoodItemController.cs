using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using StockCube.Domain.KitchenModule;

namespace StockCube.WebAPI.WebAPI.V1.KitchenModule;

[Route("api/[controller]")]
[ApiController]
public sealed class SectionFoodItemsController : ControllerBase, ISectionFoodItemController
{
    private readonly ISectionFoodItemService _sectionFoodItemService;
    public SectionFoodItemsController(ISectionFoodItemService sectionService)
        => _sectionFoodItemService = sectionService;

    [HttpGet]
    public async Task<ActionResult<IList<SectionFoodItemResponseDto>>> GetListAsync(Guid SectionId)
    {
        var result = await _sectionFoodItemService.GetListAsync(SectionId);
        var response = new List<SectionFoodItemResponseDto>();
        foreach (var foodItem in result.Value)
        {
            response.Add(new SectionFoodItemResponseDto
            {
                FoodItemId = foodItem.FoodItemId,
                Description = foodItem.Description,
                Name = foodItem.Name,
                SectionFoodItemId = foodItem.FoodItemId,
                SectionId = foodItem.SectionId,
                UnitName = foodItem.UnitName
            });
        }
        return Ok(response.AsEnumerable());
    }

    [HttpPost]
    public async Task<ActionResult<SectionFoodItemResponseDto>> CreateSectionFoodItem(CreateSectionFoodItemResponseDto request)
    {
        var sectionFoodItem = new SectionFoodItem()
        {
            SectionId = request.SectionId,
            Name = request.Name,
            Description = request.Description,
            UnitName = request.UnitName
        };

        var result = await _sectionFoodItemService.CreateSectionFoodItem(sectionFoodItem);

        if (result.Status == ResultStatus.Invalid)
        {
            result.ValidationErrors.ToList().ForEach(error => ModelState.AddModelError(error.Identifier, error.ErrorMessage));
            return UnprocessableEntity(ModelState);
        }

        var response = new SectionFoodItemResponseDto()
        {
            SectionFoodItemId = result.Value.SectionFoodItemId,
            SectionId = result.Value.SectionId,
            Name = result.Value.Name,
            FoodItemId = result.Value.FoodItemId,
            Description = result.Value.Description,
            UnitName = result.Value.UnitName
        };
        return CreatedAtRoute("GetById", new { SectionFoodItemId = response.SectionFoodItemId.ToString() }, response);
    }
}
