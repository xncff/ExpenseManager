using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.BusinessLayer.Models;

namespace ExpenseManager.DataAccessLayer;

public class WalletRepo : IWalletRepo
{
    public Wallet Create(Wallet wallet)
    {
        throw new NotImplementedException("Storage is currently read-only mock.");
    }

    public Wallet GetByGuid(Guid guid)
    {
        Wallet result = null;
        foreach (Wallet w in InMemoryStorage.Wallets)
        {
            if (w.Guid == guid)
            {
                result = w;
                break;
            }
        }
        if (result is null)
        {
            throw new KeyNotFoundException($"Wallet {guid} not found.");
        }
        return result;
    }

    public IEnumerable<Wallet> GetAll()
    {
        return InMemoryStorage.Wallets;
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