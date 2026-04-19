using ExpenseManager.DataAccessLayer.Models;

namespace ExpenseManager.DataAccessLayer.Interfaces;

public interface ITransactionRepo
{
    Task<IEnumerable<Transaction>> GetAllByWalletAsync(Guid walletGuid);
    Task<Transaction?> GetByGuidAsync(Guid guid);
    Task SaveAsync(Transaction transaction);
    Task DeleteAsync(Guid guid);
}
