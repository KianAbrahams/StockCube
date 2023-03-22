using AutoMapper;
using StockCube.Domain.CookingModule;
using StockCube.Framework.TypeMapping;

namespace StockCube.WebAPI.WebAPI.V1.RecipeModule;

public sealed record CreateRecipeRequestDto : IMapTo<Recipe>
{
    public string Name { get; set; } = string.Empty;
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public void MapTo(AutoMapper.Profile profile)
            => profile.CreateMap<CreateRecipeRequestDto, Recipe>()
                      .ForMember(x => x.Id, opt => opt.Ignore());
}
