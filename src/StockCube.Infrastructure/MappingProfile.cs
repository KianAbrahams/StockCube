using StockCube.Framework.TypeMapping;

namespace StockCube.Infrastructure;

public class MappingProfile : MappingProfileBase
{
    public MappingProfile()
        : base(typeof(MappingProfile).Assembly)
    {
    }
}
