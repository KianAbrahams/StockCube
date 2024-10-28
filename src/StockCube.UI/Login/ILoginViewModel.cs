using CommunityToolkit.Mvvm.Input;

namespace StockCube.UI.Login;

public interface ILoginViewModel
{
    string UserName { get; set; }
    string Password { get; set; }
    IAsyncRelayCommand LoginCommand { get; }
}
