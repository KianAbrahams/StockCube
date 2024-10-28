using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StockCube.UI.Shared;

namespace StockCube.UI.Login;

internal partial class LoginViewModel : ViewModelBase, ILoginViewModel
{
    public LoginViewModel() {}

    [ObservableProperty]
    private string userName = string.Empty;
    [ObservableProperty]
    private string password = string.Empty;

    [RelayCommand]
    public async Task<Task> LoginAsync()
    {
        Console.WriteLine(UserName + " " + Password);
        return Task.CompletedTask;
    }

}
