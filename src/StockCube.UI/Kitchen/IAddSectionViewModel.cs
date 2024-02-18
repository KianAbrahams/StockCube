using CommunityToolkit.Mvvm.Input;

namespace StockCube.UI.Kitchen;

public interface IAddSectionViewModel
{
    string Name { get; set; }
    Guid SectionId { get; set; }

    IAsyncRelayCommand SaveCommand { get; }
}
