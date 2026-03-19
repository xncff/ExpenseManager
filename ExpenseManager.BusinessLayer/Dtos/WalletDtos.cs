using ExpenseManager.BusinessLayer.Models;

namespace ExpenseManager.BusinessLayer.Dtos;

public class CreateWalletRequest
{
    public string Name { get; }
    public Currency Currency { get; }

    public CreateWalletRequest(string name, Currency currency)
    {
        Name = name;
        Currency = currency;
    }
}

public class GetWalletRequest
{
    public Guid Guid { get; }

    public GetWalletRequest(Guid guid)
    {
        Guid = guid;
    }
}

public class GetWalletTotalRequest
{
    public Guid Guid { get; }
    
    public GetWalletTotalRequest(Guid guid)
    {
        Guid = guid;
    }
}

public class UpdateWalletRequest
{
    public Guid Guid { get; }
    public string Name { get; }
    public Currency Currency { get; }

    public UpdateWalletRequest(Guid guid, string name, Currency currency)
    {
        Guid = guid;
        Name = name;
        Currency = currency;
    }
}

public class DeleteWalletRequest
{
    public Guid Guid { get; }

    public DeleteWalletRequest(Guid guid)
    {
        Guid = guid;
    }
}

public class WalletResponse
{
    public Guid Guid { get; }
    public string Name { get; }
    public Currency Currency { get; }

    public WalletResponse(Guid guid, string name, Currency currency)
    {
        Guid = guid;
        Name = name;
        Currency = currency;
    }
    
    public override string ToString()
    {
        return $"{Name}, {Currency}";
    }
}