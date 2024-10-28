using StockCube.UI.Shared;
using System.Text.Json;
using StockCube.WebAPI.WebAPI.V1.KitchenModule;

namespace StockCube.UI.Kitchen;

internal class KitchenViewModel : ViewModelBase, IKitchenViewModel
{
    private readonly HttpClient httpClient;

    private List<string> _listOfSections = new();

    public List<string> ListOfSections
    {
        get => _listOfSections;
        set => _listOfSections.Add(value.ToString());
    }

    private List<FoodItemModel> _foodItemViews = [
        new ()
        {
        Name="Chicken",
        Amount=100,
        Units="g",
        Quantity="1x",
        ExpiryDate="03-feb-2023"
        },
        new ()
        {
        Name="Maris Piper Potatoes",
        Amount=2.5,
        Units="kg",
        Quantity="1x",
        ExpiryDate="19-feb-2023"
        },
        new ()
        {
        Name="Semi Skimmed Milk",
        Amount=4,
        Units="pints",
        Quantity="3x",
        ExpiryDate="10-feb-2023"
        },
        new ()
        {
        Name="Heniz Baked Beans",
        Amount=415,
        Units="g",
        Quantity="16x",
        ExpiryDate="03-feb-2024"
        }
    ];

    public List<FoodItemModel> FoodItems
    {
        get => _foodItemViews;
        set => _foodItemViews.Add(value.FirstOrDefault());
    }

    public KitchenViewModel()
        => httpClient = new HttpClient();

    public async Task<HttpResponseMessage> GetSections()
        => await httpClient.GetAsync("http://localhost:5100/api/Section").ConfigureAwait(false);

    public async Task Refresh()
    { 
        try
        {
            // Sections refresh
            var sections = await GetSections();
            var responseBody = await sections.Content.ReadAsStringAsync().ConfigureAwait(false);
            var listOfSections = JsonSerializer.Deserialize<List<SectionResponseDto>>(responseBody);
            foreach (var section in listOfSections)
            {
                ListOfSections.Add(section.name);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}

public class FoodItemModel
{
    public string? Name { get; set; }
    public double? Amount { get; set; }
    public string? Units { get; set; }
    public string? Quantity { get; set; }
    public string? ExpiryDate { get; set; }
}
