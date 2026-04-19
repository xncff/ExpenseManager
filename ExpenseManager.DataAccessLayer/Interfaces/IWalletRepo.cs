using ExpenseManager.DataAccessLayer.Models;

namespace ExpenseManager.DataAccessLayer.Interfaces;

public interface IWalletRepo
{
    Task<IEnumerable<Wallet>> GetAllAsync();
    Task<Wallet?> GetByGuidAsync(Guid guid);
    Task<decimal> GetTotalAsync(Guid guid);
    Task SaveAsync(Wallet wallet);
    Task DeleteAsync(Guid guid);
}
