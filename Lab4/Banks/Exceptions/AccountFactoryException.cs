using Banks.Models;

namespace Banks.Exceptions;

public class AccountFactoryException : Exception
{
    public AccountFactoryException(string message)
        : base(message)
    { }

    public static AccountFactoryException ExistingNumberException(string number)
    {
        return new AccountFactoryException($"This number \"{number}\" already exist");
    }
}