using ExpenseManager.BusinessLayer.Models;

namespace ExpenseManager.BusinessLayer.Dtos;

public class CreateWalletRequest
{
    public string Name { get; set; }
    public Currency Currency { get; set; }
}

public class GetWalletRequest
{
    public Guid Guid { get; set; }
}

public class UpdateWalletRequest
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public Currency Currency { get; set; }
}

public class DeleteWalletRequest
{
    public Guid Guid { get; set; }
}

public class WalletResponse
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public Currency Currency { get; set; }
    
    public override string ToString()
    {
        return $"{Name}, {Currency}";
    }
}