using StockCube.UI.Shared;
using System.Text.Json;
using StockCube.WebAPI.WebAPI.V1.KitchenModule;

namespace StockCube.UI.Kitchen;

internal class KitchenViewModel : ViewModelBase, IKitchenViewModel
{
    private readonly HttpClient httpClient;

    private List<string> _listOfSections = new List<string>();

    public List<string> ListOfSections
    {
        get => _listOfSections;
        set => _listOfSections.Add(value.ToString());
    }

    public KitchenViewModel()
        => httpClient = new HttpClient();

    public async Task<HttpResponseMessage> GetSections()
        => await httpClient.GetAsync("http://localhost:5100/api/Section").ConfigureAwait(false);

    public async Task Refresh()
    { 
        try
        {
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
