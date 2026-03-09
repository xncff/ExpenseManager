using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.BusinessLayer.Models;

namespace ExpenseManager.DataAccessLayer;

public class TransactionRepo : ITransactionRepo
{
    private readonly InMemoryStorage _storage;
    
    public TransactionRepo(InMemoryStorage storage)
    {
        _storage = storage;
    }
    
    public Transaction Create(Transaction transaction)
    {
        throw new NotImplementedException("Storage is currently read-only mock.");
    }

    public Transaction GetByGuid(Guid guid)
    {
        foreach (Transaction t in _storage.Transactions)
        {
            if (t.Guid == guid) return t;
        }
        throw new KeyNotFoundException($"Transaction {guid} not found.");
    }

    public IEnumerable<Transaction> GetAllByWallet(Guid walletGuid)
    {
        List<Transaction> result = new List<Transaction>();
        foreach (Transaction t in _storage.Transactions)
        {
            if (t.WalletGuid == walletGuid) result.Add(t);
        }
        return result;
    }

    public IEnumerable<Transaction> GetAll()
    {
        return _storage.Transactions.ToList();
    }

    public Transaction Update(Transaction transaction)
    {
        throw new NotImplementedException("Storage is currently read-only mock.");
    }

    public void Delete(Guid guid)
    {
        throw new NotImplementedException("Storage is currently read-only mock.");
    }
}