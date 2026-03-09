using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Models;

namespace ExpenseManager.BusinessLayer.Interfaces
{
    public interface IWalletService
    {
        WalletResponse Create(CreateWalletRequest request);
        WalletResponse GetByGuid(GetWalletRequest request);
        IEnumerable<WalletResponse> GetAll();
        WalletResponse Update(UpdateWalletRequest request);
        void Delete(DeleteWalletRequest request);
    }
}