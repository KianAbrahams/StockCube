namespace StockCube.WebAPI.WebAPI.V1.KitchenModule;

public sealed record SectionResponseDto
{
    public string name { get; set; } = string.Empty;
    public Guid Id { get; set; } = Guid.Empty;
}
