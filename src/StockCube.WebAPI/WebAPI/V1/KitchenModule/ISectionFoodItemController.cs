using Microsoft.AspNetCore.Mvc;

namespace StockCube.WebAPI.WebAPI.V1.KitchenModule;

public interface ISectionFoodItemController
{
    public Task<ActionResult<IList<SectionFoodItemResponseDto>>> GetListAsync(Guid SectionId);
    public Task<ActionResult<SectionFoodItemResponseDto>> CreateSectionFoodItem(CreateSectionFoodItemResponseDto request);
}
