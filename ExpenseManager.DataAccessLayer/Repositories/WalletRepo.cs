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
        throw new NotImplementedException("Storage is currently read-only mock.");
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
        throw new NotImplementedException("Storage is currently read-only mock.");
    }

    public void Delete(Guid guid)
    {
        throw new NotImplementedException("Storage is currently read-only mock.");
    }
}