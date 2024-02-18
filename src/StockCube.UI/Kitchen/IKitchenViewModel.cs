namespace StockCube.UI.Kitchen;

public interface IKitchenViewModel
{
    public List<string> ListOfSections { get; set; }
    public Task Refresh();
}
