using ExpenseManager.BusinessLayer.Models;
using ExpenseManager.BusinessLayer.Dtos;

namespace ExpenseManager.BusinessLayer.Mappings;

public static class WalletMapper
{
    public static WalletResponse ToDto(this Wallet wallet)
    {
        return new WalletResponse
        {
            Guid = wallet.Guid,
            Name = wallet.Name,
            Currency = wallet.Currency
        };
    }
}