using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.BusinessLayer.Mappings;
using ExpenseManager.DataAccessLayer.Models;
using ExpenseManager.DataAccessLayer.Interfaces;

namespace ExpenseManager.BusinessLayer.Services;

public class WalletService : IWalletService
{
    private readonly IWalletRepo _repo;

    public WalletService(IWalletRepo repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    public WalletResponse Create(CreateWalletRequest request)
    {
        Wallet toCreate = new Wallet(request.Name, request.Currency);
        return _repo.Create(toCreate).ToDto();
    }

    public WalletResponse GetByGuid(GetWalletRequest request)
    {
        return _repo.GetByGuid(request.Guid).ToDto();
    }

    public IEnumerable<WalletResponse> GetAll()
    {
        return _repo.GetAll().Select(w => w.ToDto()).ToList();
    }

    public decimal GetTotal(GetWalletTotalRequest request)
    {
        return _repo.GetTotal(request.Guid);
    }

    public WalletResponse Update(UpdateWalletRequest request)
    {
        Wallet toUpdate = _repo.GetByGuid(request.Guid);
        toUpdate.Name = request.Name;
        toUpdate.Currency = request.Currency;
        return _repo.Update(toUpdate).ToDto();
    }

    public void Delete(DeleteWalletRequest request)
    {
        _repo.Delete(request.Guid);
    }
}
