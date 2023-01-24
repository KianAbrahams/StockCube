using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StockCube.UI.Shared;

namespace StockCube.UI.Kitchen;

internal partial class AddSectionViewModel : ViewModelBase, IAddSectionViewModel
{
    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private Guid sectionId = Guid.Empty;

    [RelayCommand]
    public Task SaveAsync()
    {
        Console.WriteLine("Save");
        return Task.CompletedTask;
    }

    [RelayCommand]
    public void Cancel()
    {
        Console.WriteLine("Cancel");
    }
}
