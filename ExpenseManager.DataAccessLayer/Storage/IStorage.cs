using ExpenseManager.DataAccessLayer.Models;

namespace ExpenseManager.DataAccessLayer.Storage;

public record WalletDbModel(Guid Guid, string Name, Currency Currency);

public record TransactionDbModel(
    Guid Guid,
    Guid WalletGuid,
    decimal Amount,
    TransactionCategory Category,
    string Description,
    DateTime Date
);

public interface IStorage
{
    IAsyncEnumerable<WalletDbModel> GetWalletsAsync();
    Task<WalletDbModel?> GetWalletAsync(Guid walletGuid);
    Task SaveWalletAsync(WalletDbModel wallet);
    Task DeleteWalletAsync(Guid walletGuid);

    Task<IEnumerable<TransactionDbModel>> GetTransactionsByWalletAsync(Guid walletGuid);
    Task<TransactionDbModel?> GetTransactionAsync(Guid transactionGuid);
    Task SaveTransactionAsync(TransactionDbModel transaction);
    Task DeleteTransactionAsync(Guid transactionGuid);
}
