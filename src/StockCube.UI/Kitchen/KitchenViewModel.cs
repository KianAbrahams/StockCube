using StockCube.UI.Shared;
using System.Text.Json;
using StockCube.WebAPI.WebAPI.V1.KitchenModule;

namespace StockCube.UI.Kitchen;

internal class KitchenViewModel : ViewModelBase, IKitchenViewModel
{
    private readonly HttpClient httpClient;

    private readonly List<string> _listOfSections = new();

    public List<string> ListOfSections
    {
        get => _listOfSections;
        set => _listOfSections.Add(value.ToString());
    }

    private readonly List<FoodItemModel> _foodItemViews = [];

    public List<FoodItemModel> FoodItems
    {
        get => _foodItemViews;
        set => _foodItemViews.Add(value.FirstOrDefault());
    }

    public KitchenViewModel()
    {
        httpClient = new HttpClient();
        Refresh().WaitAsync(CancellationToken.None);
    }

    public async Task<HttpResponseMessage> GetSections()
        => await httpClient.GetAsync("http://localhost:5100/api/Section").ConfigureAwait(false);

    public async Task<HttpResponseMessage> GetFoodItems(Guid sectionId)
        => await httpClient.GetAsync("http://localhost:5100/api/FoodItem/" + sectionId).ConfigureAwait(false);

    public async Task Refresh()
    { 
        try
        {
            // Sections refresh
            var sections = await GetSections();
            var responseBody = await sections.Content.ReadAsStringAsync().ConfigureAwait(false);
            var listOfSections = JsonSerializer.Deserialize<List<SectionResponseDto>>(responseBody);

            if (listOfSections is null)
                return;

            foreach (var section in listOfSections)
            {
                ListOfSections.Add(section.name);
                await GetFoodItems(section.Id).ConfigureAwait(false);
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
