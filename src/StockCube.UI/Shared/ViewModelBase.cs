using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace StockCube.UI.Shared;

internal abstract class ViewModelBase : ObservableObject, IViewModelBase
{
    [Obsolete("Replaced with OnLoadedAsync")]
    public virtual async Task OnInitializedAsync() => await OnLoadedAsync().ConfigureAwait(true);

    //protected ILoggingProvider Log { get; }

    private bool _isBusy;
    public virtual bool IsBusy
    {
        get => _isBusy;
        protected set
        {
            SetProperty(ref _isBusy, value);
            OnIsBusyChanged(_isBusy);
        }
    }

    public event EventHandler<bool> IsBusyChanged = null!;

    protected void OnIsBusyChanged(bool isBusy) => IsBusyChanged?.Invoke(this, isBusy);


    public IAsyncRelayCommand Loaded { get; }

    public virtual async Task OnLoadedAsync() => await Task.CompletedTask.ConfigureAwait(false);

    //public ViewModelBase(ILoggingProvider log)
    //{
    //    Log = log;
    //    Loaded = new AsyncRelayCommand(execute: OnLoadedAsync);
    //}
    public ViewModelBase()
    {
        Loaded = new AsyncRelayCommand(execute: OnLoadedAsync);
    }
}
