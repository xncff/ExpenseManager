using CommunityToolkit.Mvvm.ComponentModel;

namespace ExpenseManager.PresentationLayer.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy = false;

    public bool IsNotBusy => !IsBusy;
}
