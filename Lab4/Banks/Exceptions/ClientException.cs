using Banks.Models;

namespace Banks.Exceptions;

public class ClientException : Exception
{
    public ClientException(string message)
        : base(message)
    { }

    public static ClientNameException CashSetterException()
    {
        return new ClientNameException("It is impossible to assign a negative amount of money");
    }

    public static ClientNameException AccountKeyExistException(string key)
    {
        return new ClientNameException($"This key \"{key}\" already exist");
    }

    public static ClientException AccountKeyException(string key)
    {
        return new ClientException($"Not find account with number \"{key}\"");
    }
}