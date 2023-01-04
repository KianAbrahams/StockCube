namespace StockCube.WebAPI.WebAPI.V1.KitchenModule;

public sealed record CreateSectionRequestDto
{
  public string Name { get; set; } = string.Empty;
}
