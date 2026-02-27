using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.BusinessLayer.Models;

namespace ExpenseManager.DataAccessLayer;

public class TransactionRepo : ITransactionRepo
{
    public Transaction Create(Transaction transaction)
    {
        throw new NotImplementedException("Storage is currently read-only mock.");
    }

    public Transaction GetByGuid(Guid guid)
    {
        Transaction result = null;
        foreach (Transaction t in InMemoryStorage.Transactions)
        {
            if (t.Guid == guid)
            {
                result = t;
                break;
            }
        }
        if (result is null)
        {
            throw new KeyNotFoundException($"Transaction {guid} not found.");
        }
        return result;
    }

    public IEnumerable<Transaction> GetAllByWallet(Guid walletGuid)
    {
        List<Transaction> result = new List<Transaction>();
        foreach (Transaction t in InMemoryStorage.Transactions)
        {
            if (t.WalletGuid == walletGuid)
            {
                result.Add(t);
            }
        }
        return result;
    }

    public IEnumerable<Transaction> GetAll()
    {
        return InMemoryStorage.Transactions;
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