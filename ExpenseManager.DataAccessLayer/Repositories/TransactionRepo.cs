using ExpenseManager.DataAccessLayer.Interfaces;
using ExpenseManager.DataAccessLayer.Models;
using ExpenseManager.DataAccessLayer.Storage;

namespace ExpenseManager.DataAccessLayer.Repositories;

public class TransactionRepo : ITransactionRepo
{
    private readonly IStorage _storage;

    public TransactionRepo(IStorage storage)
    {
        _storage = storage;
    }

    public async Task<IEnumerable<Transaction>> GetAllByWalletAsync(Guid walletGuid)
    {
        IEnumerable<TransactionDbModel> records = await _storage.GetTransactionsByWalletAsync(walletGuid);
        return records.Select(r => new Transaction(r.Guid, r.WalletGuid, r.Amount, r.Category, r.Description, r.Date)).ToList();
    }

    public async Task<Transaction?> GetByGuidAsync(Guid guid)
    {
        TransactionDbModel? record = await _storage.GetTransactionAsync(guid);
        return record is null
            ? null
            : new Transaction(record.Guid, record.WalletGuid, record.Amount, record.Category, record.Description, record.Date);
    }

    public Task SaveAsync(Transaction transaction)
    {
        return _storage.SaveTransactionAsync(new TransactionDbModel(
            transaction.Guid,
            transaction.WalletGuid,
            transaction.Amount,
            transaction.Category,
            transaction.Description,
            transaction.Date
        ));
    }

    public Task DeleteAsync(Guid guid)
    {
        return _storage.DeleteTransactionAsync(guid);
    }
}
