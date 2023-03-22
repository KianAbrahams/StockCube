using StockCube.Framework.TypeMapping;

namespace StockCube.WebAPI;

public class MappingProfile : MappingProfileBase
{
    public MappingProfile()
        : base(typeof(MappingProfile).Assembly)
    {
    }
}

