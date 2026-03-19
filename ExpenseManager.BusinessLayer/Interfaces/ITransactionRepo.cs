using ExpenseManager.BusinessLayer.Models;

namespace ExpenseManager.BusinessLayer.Interfaces;

public interface ITransactionRepo
{
    Transaction Create(Transaction transaction);
    Transaction GetByGuid(Guid guid);
    IEnumerable<Transaction> GetAllByWallet(Guid walletGuid);
    IEnumerable<Transaction> GetAll();
    Transaction Update(Transaction transaction);
    void Delete(Guid guid);
}