namespace StockCube.Domain.KitchenModule;

public sealed record SectionFoodItem
{
    public Guid SectionId { get; set; }
    public Guid SectionFoodItemId { get; set; }
    public Guid FoodItemId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string UnitName { get; set; }

}
