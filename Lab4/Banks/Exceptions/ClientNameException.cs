using Banks.Models;

namespace Banks.Exceptions;

public class ClientNameException : Exception
{
    public ClientNameException(string message)
        : base(message)
    { }

    public static ClientNameException InvalidNameException(string name)
    {
        return new ClientNameException("name");
    }
}