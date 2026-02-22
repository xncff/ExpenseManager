using ExpenseManager.BusinessLayer.Models;

namespace ExpenseManager.BusinessLayer.Interfaces;

public interface IWalletRepo
{
    public Wallet Create(Wallet wallet);
    public Wallet GetByGuid(Guid guid);
    public Wallet Update(Wallet wallet);
    public void Delete(Guid guid);
}