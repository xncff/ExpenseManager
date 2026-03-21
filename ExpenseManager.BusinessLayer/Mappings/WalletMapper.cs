using ExpenseManager.DataAccessLayer.Models;
using ExpenseManager.BusinessLayer.Dtos;

namespace ExpenseManager.BusinessLayer.Mappings;

public static class WalletMapper
{
    public static WalletResponse ToDto(this Wallet wallet)
    {
        return new WalletResponse(wallet.Guid, wallet.Name, wallet.Currency);
    }
}