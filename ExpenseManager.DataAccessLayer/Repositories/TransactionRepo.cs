using ExpenseManager.DataAccessLayer.Interfaces;
using ExpenseManager.DataAccessLayer.Models;
using ExpenseManager.DataAccessLayer.Storage;

namespace ExpenseManager.DataAccessLayer.Repositories;

public class TransactionRepo : ITransactionRepo
{
    private readonly InMemoryStorage _storage;

    public TransactionRepo(InMemoryStorage storage)
    {
        _storage = storage;
    }

    public Transaction Create(Transaction transaction)
    {
        _storage.Transactions.Add(new InMemoryStorage.TransactionRecord(
            transaction.Guid,
            transaction.WalletGuid,
            transaction.Amount,
            transaction.Category,
            transaction.Description,
            transaction.Date
        ));
        return transaction;
    }

    public Transaction GetByGuid(Guid guid)
    {
        var record = _storage.Transactions.FirstOrDefault(t => t.Guid == guid);
        if (record is null)
        {
            throw new KeyNotFoundException($"Transaction {guid} not found.");
        }
        return new Transaction(record.Guid, record.WalletGuid, record.Amount, record.Category, record.Description, record.Date);
    }

    public IEnumerable<Transaction> GetAllByWallet(Guid walletGuid)
    {
        return _storage.Transactions
            .Where(t => t.WalletGuid == walletGuid)
            .Select(r => new Transaction(r.Guid, r.WalletGuid, r.Amount, r.Category, r.Description, r.Date))
            .ToList();
    }

    public IEnumerable<Transaction> GetAll()
    {
        return _storage.Transactions
            .Select(r => new Transaction(r.Guid, r.WalletGuid, r.Amount, r.Category, r.Description, r.Date))
            .ToList();
    }

    public Transaction Update(Transaction transaction)
    {
        int index = FindIndex(transaction.Guid);
        _storage.Transactions[index] = new InMemoryStorage.TransactionRecord(
            transaction.Guid,
            transaction.WalletGuid,
            transaction.Amount,
            transaction.Category,
            transaction.Description,
            transaction.Date
        );
        return transaction;
    }

    public void Delete(Guid guid)
    {
        int index = FindIndex(guid);
        _storage.Transactions.RemoveAt(index);
    }

    private int FindIndex(Guid guid)
    {
        for (int i = 0; i < _storage.Transactions.Count; i++)
        {
            if (_storage.Transactions[i].Guid == guid)
            {
                return i;
            }
        }
        throw new KeyNotFoundException($"Transaction {guid} not found.");
    }
}
