using ExpenseManager.DataAccessLayer.Interfaces;
using ExpenseManager.DataAccessLayer.Models;
using ExpenseManager.DataAccessLayer.Storage;

namespace ExpenseManager.DataAccessLayer.Repositories;

public class WalletRepo : IWalletRepo
{
    private readonly IStorage _storage;

    public WalletRepo(IStorage storage)
    {
        _storage = storage;
    }

    public async Task<IEnumerable<Wallet>> GetAllAsync()
    {
        List<Wallet> result = new List<Wallet>();
        await foreach (WalletDbModel record in _storage.GetWalletsAsync())
        {
            result.Add(new Wallet(record.Guid, record.Name, record.Currency));
        }
        return result;
    }

    public async Task<Wallet?> GetByGuidAsync(Guid guid)
    {
        WalletDbModel? record = await _storage.GetWalletAsync(guid);
        return record is null ? null : new Wallet(record.Guid, record.Name, record.Currency);
    }

    public async Task<decimal> GetTotalAsync(Guid guid)
    {
        IEnumerable<TransactionDbModel> transactions = await _storage.GetTransactionsByWalletAsync(guid);
        return transactions.Select(t => t.Amount).DefaultIfEmpty(0).Sum();
    }

    public Task SaveAsync(Wallet wallet)
    {
        return _storage.SaveWalletAsync(new WalletDbModel(wallet.Guid, wallet.Name, wallet.Currency));
    }

    public Task DeleteAsync(Guid guid)
    {
        return _storage.DeleteWalletAsync(guid);
    }
}
