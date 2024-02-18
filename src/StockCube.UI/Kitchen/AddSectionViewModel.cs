using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using StockCube.UI.Shared;
using StockCube.WebAPI.WebAPI.V1.KitchenModule;

namespace StockCube.UI.Kitchen;

internal partial class AddSectionViewModel : ViewModelBase, IAddSectionViewModel
{
    private readonly ISectionController _sectionController;

    public AddSectionViewModel(ISectionController sectionController)
        => _sectionController = sectionController;

    [ObservableProperty]
    private string name = string.Empty;
    [ObservableProperty]
    private Guid sectionId = Guid.Empty;

    [RelayCommand]
    public async Task<Task> SaveAsync()
    {
        var request = await _sectionController.CreateSectionAsync(new CreateSectionRequestDto()
        {
            Name = this.Name,
        });
        return Task.CompletedTask;
    }
}
