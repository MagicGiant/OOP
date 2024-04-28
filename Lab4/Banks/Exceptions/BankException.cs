using Banks.Entities;

namespace Banks.Exceptions;

public class BankException : Exception
{
    public BankException(string message)
        : base(message)
    { }

    public static BankException SetClientExistingException(string id)
    {
        return new BankException($"This client with id \"{id}\" already exist");
    }

    public static BankException RemoveClientException(string id)
    {
        return new BankException($"Can't remove not existing client with id \"{id}\"");
    }

    public static BankException RemoveAccountWithCashException()
    {
        return new BankException("Can't remove account with credit or cash");
    }

    public static BankException AccountException(string passportId)
    {
        return new BankException($"Bank have not account \"{passportId}\"");
    }

    public static BankException FindClientException(string passportId)
    {
        return new BankException($"Bank have not client \"{passportId}\"");
    }

    public static BankException TransferException()
    {
        return new BankException("Transfer Cash can't be low 0");
    }
}