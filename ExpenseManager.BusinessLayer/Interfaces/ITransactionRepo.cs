using ExpenseManager.BusinessLayer.Models;

namespace ExpenseManager.BusinessLayer.Interfaces;

public interface ITransactionRepo
{
    public Transaction Create(Transaction transaction);
    public Transaction GetByGuid(Guid guid);
    public IEnumerable<Transaction> GetAllByWalletGuid(Guid walletGuid);
    public Transaction Update(Transaction transaction);
    public void Delete(Guid guid);
}