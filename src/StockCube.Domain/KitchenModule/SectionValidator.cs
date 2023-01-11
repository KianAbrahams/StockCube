namespace StockCube.Domain.KitchenModule;

internal sealed class SectionValidator : AbstractValidator<Section>, ISectionValidator
{
    public SectionValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(Section.NameMaxLength);
    }
}
