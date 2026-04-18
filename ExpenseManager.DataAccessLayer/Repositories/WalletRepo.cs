using ExpenseManager.DataAccessLayer.Interfaces;
using ExpenseManager.DataAccessLayer.Models;
using ExpenseManager.DataAccessLayer.Storage;

namespace ExpenseManager.DataAccessLayer.Repositories;

public class WalletRepo : IWalletRepo
{
    private readonly InMemoryStorage _storage;

    public WalletRepo(InMemoryStorage storage)
    {
        _storage = storage;
    }

    public Wallet Create(Wallet wallet)
    {
        _storage.Wallets.Add(new InMemoryStorage.WalletRecord(wallet.Guid, wallet.Name, wallet.Currency));
        return wallet;
    }

    public Wallet GetByGuid(Guid guid)
    {
        var record = _storage.Wallets.FirstOrDefault(w => w.Guid == guid);
        if (record is null)
        {
            throw new KeyNotFoundException($"Wallet {guid} not found.");
        }
        return new Wallet(record.Guid, record.Name, record.Currency);
    }

    public decimal GetTotal(Guid guid)
    {
        return _storage.Transactions
            .Where(tx => tx.WalletGuid == guid)
            .Select(tx => tx.Amount)
            .DefaultIfEmpty(0)
            .Sum();
    }

    public IEnumerable<Wallet> GetAll()
    {
        return _storage.Wallets
            .Select(r => new Wallet(r.Guid, r.Name, r.Currency))
            .ToList();
    }

    public Wallet Update(Wallet wallet)
    {
        int index = FindIndex(wallet.Guid);
        _storage.Wallets[index] = new InMemoryStorage.WalletRecord(wallet.Guid, wallet.Name, wallet.Currency);
        return wallet;
    }

    public void Delete(Guid guid)
    {
        int index = FindIndex(guid);
        _storage.Wallets.RemoveAt(index);

        var orphanTransactions = _storage.Transactions.Where(t => t.WalletGuid == guid).ToList();
        foreach (var tx in orphanTransactions)
        {
            _storage.Transactions.Remove(tx);
        }
    }

    private int FindIndex(Guid guid)
    {
        for (int i = 0; i < _storage.Wallets.Count; i++)
        {
            if (_storage.Wallets[i].Guid == guid)
            {
                return i;
            }
        }
        throw new KeyNotFoundException($"Wallet {guid} not found.");
    }
}
