using ExpenseManager.BusinessLayer.Models;

namespace ExpenseManager.BusinessLayer.Interfaces;

public interface IWalletRepo
{
    public Wallet Create(Wallet wallet);
    public Wallet GetByGuid(Guid guid);
    public  IEnumerable<Wallet> GetAll();
    public Wallet Update(Wallet wallet);
    public void Delete(Guid guid);
}