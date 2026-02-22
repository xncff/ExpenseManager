using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.BusinessLayer.Models;

namespace ExpenseManager.BusinessLayer.Services;

public class WalletService
{
    private readonly IWalletRepo _repo;
    
    public WalletService(IWalletRepo repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));;
    }

    public Wallet Create(string name, Currency currency)
    {
        try
        {
            Wallet toCreate = new Wallet(name, currency);
            return _repo.Create(toCreate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Wallet GetByGuid(Guid guid)
    {
        try
        {
            return  _repo.GetByGuid(guid);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public Wallet Update(Guid guid, string name, Currency currency)
    {
        try
        {
            Wallet toUpdate = _repo.GetByGuid(guid);
            toUpdate.Name = name;
            toUpdate.Currency = currency;
            return _repo.Update(toUpdate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void Delete(Guid guid)
    {
        try
        {
            _repo.Delete(guid);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}