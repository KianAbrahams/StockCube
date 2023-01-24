using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace StockCube.UI.Shared;

public interface IViewModelBase : INotifyPropertyChanged
{
    Task OnInitializedAsync();
    bool IsBusy { get; }
    event EventHandler<bool> IsBusyChanged;

    IAsyncRelayCommand Loaded { get; }
}
