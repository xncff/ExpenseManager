using ExpenseManager.DataAccessLayer.Models;

namespace ExpenseManager.DataAccessLayer.Interfaces;

public interface IWalletRepo
{
    Wallet Create(Wallet wallet);
    Wallet GetByGuid(Guid guid);
    IEnumerable<Wallet> GetAll();
    decimal GetTotal(Guid guid);
    Wallet Update(Wallet wallet);
    void Delete(Guid guid);
}