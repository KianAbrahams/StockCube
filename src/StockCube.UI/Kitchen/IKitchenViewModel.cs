namespace StockCube.UI.Kitchen;

public interface IKitchenViewModel
{
    public List<FoodItemModel> FoodItems { get; set; }
    public List<string> ListOfSections { get; set; }
    public Task Refresh();
}
