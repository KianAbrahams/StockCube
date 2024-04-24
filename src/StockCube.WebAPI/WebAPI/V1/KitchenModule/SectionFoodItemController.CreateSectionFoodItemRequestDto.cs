namespace StockCube.WebAPI.WebAPI.V1.KitchenModule;

public sealed class CreateSectionFoodItemResponseDto
{
    public Guid SectionId { get; set; } = Guid.Empty;
    public Guid FoodItemId { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string UnitName { get; set; } = string.Empty;
}
