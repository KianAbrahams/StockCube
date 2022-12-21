using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Expressions;
using StockCube.WebAPI.WebAPI.V1.KitchenModule;

namespace StockCube.Test.WebAPI.V1.KitchenModule;

public class SectionController_Get_Should
{
    [Fact]
    public async Task Return_Status200Ok_WithListOfSections()
    {
        // arrange
        var sut = new SectionController();

        // act
        var response = await sut.GetListAsync();

        // assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result;
        result.Value.Should().NotBeNull();
        result.Value.Should().BeAssignableTo<SectionResponseDto>();

        var value = (SectionResponseDto)result.Value;
        value.Should().NotBeNull();
        value.Name.Should().Be("");
    }
}
