using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.BusinessLayer.Mappings;
using ExpenseManager.BusinessLayer.Models;
using ExpenseManager.BusinessLayer.Mappings;

namespace ExpenseManager.BusinessLayer.Services;

public class WalletService
{
    private readonly IWalletRepo _repo;
    
    public WalletService(IWalletRepo repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    public WalletResponse Create(CreateWalletRequest request)
    {
        try
        {
            Wallet toCreate = new Wallet(request.Name, request.Currency);
            return _repo.Create(toCreate).ToDto();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public WalletResponse GetByGuid(GetWalletRequest request)
    {
        try
        {
            return _repo.GetByGuid(request.Guid).ToDto();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IEnumerable<WalletResponse> GetAll()
    {
        try
        {
            List<WalletResponse> result = new List<WalletResponse>();
            foreach (Wallet wallet in _repo.GetAll())
            {
                result.Add(wallet.ToDto());
            }
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public WalletResponse Update(UpdateWalletRequest request)
    {
        try
        {
            Wallet toUpdate = _repo.GetByGuid(request.Guid);
            toUpdate.Name = request.Name;
            toUpdate.Currency = request.Currency;
            return _repo.Update(toUpdate).ToDto();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void Delete(DeleteWalletRequest request)
    {
        try
        {
            _repo.Delete(request.Guid);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}