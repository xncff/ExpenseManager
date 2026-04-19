using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.BusinessLayer.Mappings;
using ExpenseManager.DataAccessLayer.Models;
using ExpenseManager.DataAccessLayer.Interfaces;

namespace ExpenseManager.BusinessLayer.Services;

public class WalletService : IWalletService
{
    private readonly IWalletRepo _repo;

    public WalletService(IWalletRepo repo)
    {
        _repo = repo;
    }

    public async Task<WalletResponse> CreateAsync(CreateWalletRequest request)
    {
        Wallet wallet = new Wallet(request.Name, request.Currency);
        await _repo.SaveAsync(wallet);
        return wallet.ToDto();
    }

    public async Task<WalletResponse> GetByGuidAsync(GetWalletRequest request)
    {
        Wallet wallet = await _repo.GetByGuidAsync(request.Guid)
            ?? throw new KeyNotFoundException($"Wallet {request.Guid} not found.");
        return wallet.ToDto();
    }

    public async Task<IEnumerable<WalletResponse>> GetAllAsync()
    {
        IEnumerable<Wallet> wallets = await _repo.GetAllAsync();
        return wallets.Select(w => w.ToDto()).ToList();
    }

    public Task<decimal> GetTotalAsync(GetWalletTotalRequest request)
    {
        return _repo.GetTotalAsync(request.Guid);
    }

    public async Task<WalletResponse> UpdateAsync(UpdateWalletRequest request)
    {
        Wallet wallet = await _repo.GetByGuidAsync(request.Guid)
            ?? throw new KeyNotFoundException($"Wallet {request.Guid} not found.");
        wallet.Name = request.Name;
        wallet.Currency = request.Currency;
        await _repo.SaveAsync(wallet);
        return wallet.ToDto();
    }

    public Task DeleteAsync(DeleteWalletRequest request)
    {
        return _repo.DeleteAsync(request.Guid);
    }
}
