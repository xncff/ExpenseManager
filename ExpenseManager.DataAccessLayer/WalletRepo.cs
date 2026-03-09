using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.BusinessLayer.Models;

namespace ExpenseManager.DataAccessLayer;

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
        foreach (Wallet w in _storage.Wallets)
        {
            if (w.Guid == guid) return w;
        }
        throw new KeyNotFoundException($"Wallet {guid} not found.");
    }

    public IEnumerable<Wallet> GetAll()
    {
        return _storage.Wallets.ToList();
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