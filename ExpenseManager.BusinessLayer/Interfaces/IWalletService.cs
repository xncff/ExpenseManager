using ExpenseManager.BusinessLayer.Dtos;

namespace ExpenseManager.BusinessLayer.Interfaces
{
    public interface IWalletService
    {
        Task<WalletResponse> CreateAsync(CreateWalletRequest request);
        Task<WalletResponse> GetByGuidAsync(GetWalletRequest request);
        Task<IEnumerable<WalletResponse>> GetAllAsync();
        Task<decimal> GetTotalAsync(GetWalletTotalRequest request);
        Task<WalletResponse> UpdateAsync(UpdateWalletRequest request);
        Task DeleteAsync(DeleteWalletRequest request);
    }
}
