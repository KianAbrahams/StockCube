namespace StockCube.WebAPI.WebAPI.V1.KitchenModule
{
    public sealed record SectionResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid Id { get; set; } = Guid.Empty;
    }
}
