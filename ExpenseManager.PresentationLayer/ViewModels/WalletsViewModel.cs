using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.PresentationLayer.Pages;

namespace ExpenseManager.PresentationLayer.ViewModels;

public partial class WalletsViewModel : BaseViewModel
{
    private readonly IWalletService _walletService;

    public ObservableCollection<WalletResponse> Wallets { get; } = new();

    public IReadOnlyList<string> SortOptions { get; } = new[]
    {
        "Name (A–Z)",
        "Name (Z–A)",
        "Currency",
    };

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _selectedSort;

    public WalletsViewModel(IWalletService walletService)
    {
        _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
        _selectedSort = SortOptions[0];
    }

    [RelayCommand]
    public async Task Reload()
    {
        IsBusy = true;
        try
        {
            IEnumerable<WalletResponse> items = _walletService.GetAll();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                string query = SearchText.Trim();
                items = items.Where(w => w.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
            }

            items = SelectedSort switch
            {
                "Name (Z–A)" => items.OrderByDescending(w => w.Name),
                "Currency" => items.OrderBy(w => w.Currency),
                _ => items.OrderBy(w => w.Name),
            };

            Wallets.Clear();
            foreach (var wallet in items)
            {
                Wallets.Add(wallet);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to load wallets: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task LoadWallet(Guid walletGuid)
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync(nameof(WalletDetailsPage),
                new Dictionary<string, object> { { "WalletGuid", walletGuid } });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to open wallet: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task AddWallet()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync(nameof(WalletDetailsPage),
                new Dictionary<string, object> { { "WalletGuid", Guid.Empty } });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to open wallet creation: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task DeleteWallet(Guid walletGuid)
    {
        bool confirmed = await Shell.Current.DisplayAlertAsync(
            "Delete wallet",
            "Are you sure? All its transactions will be removed.",
            "Delete",
            "Cancel");

        if (!confirmed)
        {
            return;
        }

        IsBusy = true;
        try
        {
            _walletService.Delete(new DeleteWalletRequest(walletGuid));
            await Reload();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to delete wallet: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
