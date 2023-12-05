namespace StockCube.WebAPI.WebAPI.V1.ShoppingModule;

public sealed record CreateShoppingRequestDto
{
  public string Name { get; set; } = string.Empty;
}
